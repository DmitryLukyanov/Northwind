using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Northwind.Models;
using Northwind.Services.Interfaces;
using Northwind.ViewModels;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IConfiguration configuration;
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;

        public ProductController(IConfiguration configuration, ICategoryService categoryService,
            IProductService productService, ISupplierService supplierService)
        {
            this.categoryService = categoryService;
            this.supplierService = supplierService;
            this.productService = productService;
            this.configuration = configuration;
        }


        public IActionResult Index()
        {
            var count = configuration.GetValue<int>("MaxProductsPerPage");
            if (count == 0)
            {
                return View(productService.GetAll());
            }

            return View(productService.Take(count));
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
                var product = new Product
                {
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

            return View();
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
                var product = new Product
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

            return View();
        }
    }
}