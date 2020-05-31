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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return categoryService.GetAll();
        }

        [HttpGet ("{id}")]
        public byte[] GetPicture(int id)
        {
            return categoryService.GetPicture(id);
        }

        [HttpPut("{id}")]
        public void SetPicture(int id, byte[] bytes)
        {
            categoryService.SetPicture(id, bytes);
        }

        
    }
}
