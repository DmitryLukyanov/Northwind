using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Controllers;
using Northwind.Models;
using Northwind.ViewModels;

namespace Northwind.Tests
{
    [TestClass]
    public class ProductControllerTest : BaseTest
    {
        [TestMethod]
        public void IndexReturnsProducts()
        {
            var controller = Setup();

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult) result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IEnumerable<Product>));
            var viewDataModel = (IEnumerable<Product>) viewResult.ViewData.Model;
            Assert.AreEqual(productsCount, viewDataModel.Count());
        }

        [TestMethod]
        public void IndexReturnsPagedProducts()
        {
            var productsPerPage = 1;
            var controller = Setup(productsPerPage);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult) result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IEnumerable<Product>));
            var viewDataModel = (IEnumerable<Product>) viewResult.ViewData.Model;
            Assert.AreEqual(productsPerPage, viewDataModel.Count());
        }

        [TestMethod]
        public void CreateReturnsWellFormedModel()
        {
            var controller = Setup();

            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult) result;
            Assert.IsInstanceOfType(viewResult.ViewData["CategoryId"], typeof(SelectList));
            var categories = (SelectList) viewResult.ViewData["CategoryId"];
            Assert.AreEqual(categoriesCount, categories.Count());
            Assert.IsInstanceOfType(viewResult.ViewData["SupplierId"], typeof(SelectList));
            var suppliers = (SelectList) viewResult.ViewData["SupplierId"];
            Assert.AreEqual(suppliersCount, suppliers.Count());
        }

        [TestMethod]
        public void CreateProcessesPostRequest()
        {
            var controller = Setup();
            var productEditModel = new ProductEditModel {ProductId = 1, ProductName = "Beer"};

            var result = controller.Create(productEditModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void EditReturnsWellFormedModel()
        {
            var controller = Setup();

            var result = controller.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult) result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Product));
            Assert.IsInstanceOfType(viewResult.ViewData["CategoryId"], typeof(SelectList));
            var categories = (SelectList) viewResult.ViewData["CategoryId"];
            Assert.AreEqual(categoriesCount, categories.Count());
            Assert.IsInstanceOfType(viewResult.ViewData["SupplierId"], typeof(SelectList));
            var suppliers = (SelectList) viewResult.ViewData["SupplierId"];
            Assert.AreEqual(suppliersCount, suppliers.Count());
        }

        [TestMethod]
        public void EditProcessesPostRequest()
        {
            var controller = Setup();
            var productEditModel = new ProductEditModel {ProductId = 1, ProductName = "Beer"};

            var result = controller.Create(productEditModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        private ProductController Setup(int maxProductsPerPage = 0)
        {
            var productServiceMock = GetProductServiceMock();
            var categoryServiceMock = GetCategoryServiceMock();
            var configurationMock = GetConfigurationMock(maxProductsPerPage);
            var supplierMock = GetSupplierMock();
            return new ProductController(configurationMock.Object, categoryServiceMock.Object,
                productServiceMock.Object, supplierMock.Object);
        }
    }
}