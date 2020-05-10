using Northwind.Data;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class UserService
    {
        private readonly NorthwindDbContext context;

        public UserService(NorthwindDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<NorthwindUser> GetAll()
        {
            return context.Users.OrderBy(user => user.NormalizedUserName);
        }
    }
}
