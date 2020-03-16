using Northwind.Models;
using Northwind.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class CategoryService : ICategoryService
    {

        private Northwind.Data.NorthwindDbContext context;

        public CategoryService(Northwind.Data.NorthwindDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.OrderBy(category => category.CategoryName);
        }
    }
}
