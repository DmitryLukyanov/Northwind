using Microsoft.EntityFrameworkCore;
using Northwind.Models;
using Northwind.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class ProductService : IProductService
    {

        private Northwind.Data.NorthwindDbContext context;

        public ProductService(Northwind.Data.NorthwindDbContext context)
        {
            this.context = context;
        }

        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Edit(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        public Product Get(int id)
        {
            return context.Products.FirstOrDefault(product => product.ProductId == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.Include(product => product.Category).Include(product => product.Supplier).OrderBy(product => product.ProductName);
        }

        public IEnumerable<Product> Take(int count)
        {
            return context.Products.Include(product => product.Category).Include(product => product.Supplier).OrderBy(product => product.ProductName).Take(count);
        }
    }
}
