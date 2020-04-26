using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Models;

namespace Northwind.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> Take(int count);
        IEnumerable<Product> GetAll();
        void Create(Product product);
        void Delete(int id);
        void Edit(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Product Get(int id);
    }
}