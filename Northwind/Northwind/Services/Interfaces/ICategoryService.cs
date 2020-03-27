using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category Get(int id);
        byte[] GetPicture(int id);
        void SetPicture(int id, byte[] content);
    }
}