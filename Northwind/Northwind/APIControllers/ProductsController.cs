using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;
using Northwind.Services.Interfaces;

namespace Northwind.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        
        [HttpGet]
        public Task<IEnumerable<Product>> GetProducts()
        {
            return productService.GetAllAsync();
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            productService.Edit(product);
            return NoContent();
        }

        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            productService.Create(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            productService.Delete(id);
            return NoContent();
        }

    }
}
