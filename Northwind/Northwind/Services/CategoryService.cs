using System.Collections.Generic;
using System.Linq;
using Northwind.Data;
using Northwind.Models;
using Northwind.Services.Interfaces;

namespace Northwind.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NorthwindDbContext context;

        public CategoryService(NorthwindDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.OrderBy(category => category.CategoryName);
        }

        public Category Get(int id)
        {
            return context.Categories.SingleOrDefault(category => category.CategoryId == id);
        }


        public byte[] GetPicture(int id)
        {
            return context.Categories.SingleOrDefault(category => category.CategoryId == id)?.Picture;
        }

        public void SetPicture(int id, byte[] content)
        {
            var category = Get(id);
            category.Picture = content;
            context.Categories.Update(category);
            context.SaveChanges();
        }
    }
}