using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Filters;
using Northwind.Services.Interfaces;

namespace Northwind.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }


        public IActionResult Index()
        {
            return View(service.GetAll());
        }

        public IActionResult Image(int id)
        {
            var fileBytes = service.GetPicture(id);
            if (fileBytes == null)
            {
                return NotFound();
            }

            return File(fileBytes, "image/png");
        }

        [TypeFilter(typeof(LogActionFilter), Arguments = new object[] {true})]
        [HttpGet]
        public IActionResult UploadImage(int id)
        {
            return View(service.Get(id));
        }

        [HttpPost]
        public IActionResult UploadImage(int id, List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return RedirectToAction(nameof(UploadImage));
            }

            var file = files[0];
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                service.SetPicture(id, stream.ToArray());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}