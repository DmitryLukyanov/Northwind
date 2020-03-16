﻿using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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