﻿using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
    }
}
