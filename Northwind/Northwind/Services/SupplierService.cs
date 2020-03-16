using Northwind.Models;
using Northwind.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class SupplierService : ISupplierService
    {

        private Northwind.Data.NorthwindDbContext context;

        public SupplierService(Northwind.Data.NorthwindDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return context.Suppliers.OrderBy(supplier => supplier.CompanyName);
        }
    }
}
