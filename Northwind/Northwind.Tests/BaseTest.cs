using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Moq;
using Northwind.Models;
using Northwind.Services.Interfaces;

namespace Northwind.Tests
{
    public abstract class BaseTest
    {
        protected readonly int categoriesCount;

        protected readonly int productsCount;
        protected readonly int suppliersCount;

        private readonly List<Category> testCategories = new List<Category>
        {
            new Category {CategoryName = "Food", CategoryId = 1},
            new Category {CategoryName = "Drinks", CategoryId = 2}
        };

        private readonly List<Product> testProducts = new List<Product>
        {
            new Product {ProductName = "Pepsi", CategoryId = 1, SupplierId = 1, ProductId = 1},
            new Product {ProductName = "Pizza", CategoryId = 2, SupplierId = 2, ProductId = 2},
            new Product {ProductName = "Soup", CategoryId = 1, SupplierId = 1, ProductId = 3}
        };

        private readonly List<Supplier> testSuppliers = new List<Supplier>
        {
            new Supplier {CompanyName = "McDonalds", SupplierId = 1},
            new Supplier {CompanyName = "PizzaHut", SupplierId = 2}
        };

        protected BaseTest()
        {
            productsCount = testProducts.Count;
            categoriesCount = testCategories.Count;
            suppliersCount = testSuppliers.Count;
        }

        protected Mock<ICategoryService> GetCategoryServiceMock()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(categoryService => categoryService.GetAll())
                .Returns(GetTestCategories());
            return categoryServiceMock;
        }

        protected Mock<IProductService> GetProductServiceMock()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(productService => productService.GetAll())
                .Returns(GetTestProducts());
            productServiceMock.Setup(productService => productService.Take(It.IsAny<int>()))
                .Returns((int count) => TakeTestProducts(count));
            productServiceMock.Setup(productService => productService.Create(It.IsAny<Product>())).Verifiable();
            productServiceMock.Setup(productService => productService.Edit(It.IsAny<Product>())).Verifiable();
            productServiceMock.Setup(productService => productService.Get(It.IsAny<int>()))
                .Returns((int id) => GetProduct(id)).Verifiable();
            return productServiceMock;
        }

        protected Mock<IConfiguration> GetConfigurationMock(int maxProductsPerPage = 0)
        {
            var configurationMock = new Mock<IConfiguration>();

            var configurationSectionMock = new Mock<IConfigurationSection>();
            configurationSectionMock.Setup(cs => cs.Value)
                .Returns(maxProductsPerPage.ToString());
            configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "MaxProductsPerPage")))
                .Returns(configurationSectionMock.Object);
            return configurationMock;
        }

        protected Mock<ISupplierService> GetSupplierMock()
        {
            var supplierServiceMock = new Mock<ISupplierService>();
            supplierServiceMock.Setup(supplierService => supplierService.GetAll())
                .Returns(GetTestSuppliers);
            return supplierServiceMock;
        }

        private IEnumerable<Category> GetTestCategories()
        {
            return testCategories;
        }

        private IEnumerable<Product> GetTestProducts()
        {
            return testProducts;
        }

        private IEnumerable<Product> TakeTestProducts(int count)
        {
            return testProducts.Take(count);
        }

        private IEnumerable<Supplier> GetTestSuppliers()
        {
            return testSuppliers;
        }

        private Product GetProduct(int id)
        {
            return testProducts.SingleOrDefault(product => product.ProductId == id);
        }
    }
}