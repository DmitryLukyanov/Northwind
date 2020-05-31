using System.Collections.Generic;
using System.ComponentModel;

namespace Northwind.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }

        [DisplayName("Name")] public string CategoryName { get; set; }

        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}