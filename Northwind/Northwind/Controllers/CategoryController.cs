using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Data;
using Northwind.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.Controllers
{
    public class CategoryController : Controller
    {

        private NorthwindDbContext dbContext;

        public CategoryController(NorthwindDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        public IActionResult Index()
        {
            var list = dbContext.Categories.OrderBy(category => category.CategoryName);
            return View(list);
        }
    }
}
