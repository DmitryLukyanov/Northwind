using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Interfaces;
using Northwind.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.Controllers
{
    public class CategoryController : Controller
    {

        private ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        
        public IActionResult Index()
        {
            
            return View(service.GetAll());
        }
    }
}
