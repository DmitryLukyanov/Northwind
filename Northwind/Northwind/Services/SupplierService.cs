using System.Collections.Generic;
using System.Linq;
using Northwind.Data;
using Northwind.Models;
using Northwind.Services.Interfaces;

namespace Northwind.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly NorthwindDbContext context;

        public SupplierService(NorthwindDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return context.Suppliers.OrderBy(supplier => supplier.CompanyName);
        }
    }
}