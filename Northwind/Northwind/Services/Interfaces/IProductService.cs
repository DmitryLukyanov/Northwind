using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> Take(int count);
        IEnumerable<Product> GetAll();
        void Create(Product product);
        void Edit(Product product);

        Product Get(int id);
    }
}