using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Data;
using Northwind.Models;
using Northwind.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {

        private NorthwindDbContext dbContext;
        private IConfiguration configuration;

        public ProductController(NorthwindDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        
        public IActionResult Index()
        {
            int count = configuration.GetValue<int>("MaxProductsPerPage");
            if (count == 0)
            {
                return View(dbContext.Products.Include(product => product.Category).Include(product => product.Supplier).OrderBy(product => product.ProductName));
            }
            else
            {
                return View(dbContext.Products.Include(product => product.Category).Include(product => product.Supplier).OrderBy(product => product.ProductName).Take(count));
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(dbContext.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(dbContext.Suppliers, "SupplierId", "CompanyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductEditModel model)
        {
            
            if (ModelState.IsValid)
            {
                var product = new Product() {
                    CategoryId = model.CategoryId,
                    Discontinued = model.Discontinued,
                    ProductName = model.ProductName,
                    QuantityPerUnit = model.QuantityPerUnit,
                    ReorderLevel = model.ReorderLevel,
                    SupplierId = model.SupplierId,
                    UnitPrice = model.UnitPrice,
                    UnitsInStock = model.UnitsInStock,
                    UnitsOnOrder = model.UnitsOnOrder
                };

                dbContext.Products.Add(product);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["CategoryId"] = new SelectList(dbContext.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(dbContext.Suppliers, "SupplierId", "CompanyName");
            return View(dbContext.Products.FirstOrDefault(product => product.ProductId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductEditModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    ProductId = model.ProductId,
                    CategoryId = model.CategoryId,
                    Discontinued = model.Discontinued,
                    ProductName = model.ProductName,
                    QuantityPerUnit = model.QuantityPerUnit,
                    ReorderLevel = model.ReorderLevel,
                    SupplierId = model.SupplierId,
                    UnitPrice = model.UnitPrice,
                    UnitsInStock = model.UnitsInStock,
                    UnitsOnOrder = model.UnitsOnOrder
                };

                dbContext.Products.Update(product);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
