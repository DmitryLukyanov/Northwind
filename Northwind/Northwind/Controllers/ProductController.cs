using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Models;
using Northwind.Services.Interfaces;
using Northwind.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {

        private ICategoryService categoryService;
        private ISupplierService supplierService;
        private IProductService productService;
        private IConfiguration configuration;

        public ProductController(IConfiguration configuration, ICategoryService categoryService, IProductService productService, ISupplierService supplierService)
        {
            this.categoryService = categoryService;
            this.supplierService = supplierService;
            this.productService = productService;
            this.configuration = configuration;
        }

        
        public IActionResult Index()
        {
            int count = configuration.GetValue<int>("MaxProductsPerPage");
            if (count == 0)
            {
                return View(productService.GetAll());
            }
            else
            {
                return View(productService.Take(count));
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(supplierService.GetAll(), "SupplierId", "CompanyName");
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

                productService.Create(product);

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
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(supplierService.GetAll(), "SupplierId", "CompanyName");
            return View(productService.Get(id));
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

                productService.Edit(product);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
