using Northwind.Models;
using System;
using System.Collections.Generic;

namespace Northwind.Models
{
    public partial class CustomerDemographic
    {
        public CustomerDemographic()
        {
            CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
        }

        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }

        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}
