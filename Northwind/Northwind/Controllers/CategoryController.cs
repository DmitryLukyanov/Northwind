using System;
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
        private const int imageIncorrectBytes = 78;
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
            // correct image as first 78 bytes of original images are garbage
            var correctedPicture = new byte[fileBytes.Length - imageIncorrectBytes];
            Array.ConstrainedCopy(fileBytes, imageIncorrectBytes, correctedPicture, 0, fileBytes.Length - imageIncorrectBytes);

            return File(correctedPicture, "image/bmp");
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
                var originalImage = stream.ToArray();
                //put 78 bytes of garbage to start of the array to support both old and new images
                var correctedImage = new byte[originalImage.Length + imageIncorrectBytes];
                originalImage.CopyTo(correctedImage, imageIncorrectBytes);
                service.SetPicture(id, correctedImage);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}