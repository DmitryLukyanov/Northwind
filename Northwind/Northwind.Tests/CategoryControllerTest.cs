using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Controllers;
using Northwind.Models;

namespace Northwind.Tests
{
    [TestClass]
    public class CategoryControllerTest : BaseTest
    {
        [TestMethod]
        public void IndexReturnsCategories()
        {
            var categoryServiceMock = GetCategoryServiceMock();
            var controller = new CategoryController(categoryServiceMock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult) result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IEnumerable<Category>));
            var viewDataModel = (IEnumerable<Category>) viewResult.ViewData.Model;
            Assert.AreEqual(categoriesCount, viewDataModel.Count());
        }
    }
}