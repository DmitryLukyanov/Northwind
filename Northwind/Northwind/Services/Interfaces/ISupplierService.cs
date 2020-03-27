using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services.Interfaces
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAll();
    }
}