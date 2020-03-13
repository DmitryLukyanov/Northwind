using Northwind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.ViewModels
{
    public class ProductEditModel
    {
        public int ProductId { get; set; }
        [Required, MinLength(3, ErrorMessage = "Product name should not be less than 3 characters.")]
        [DisplayName("Name")]
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        
        [DisplayName("Quantity per unit")]
        public string QuantityPerUnit { get; set; }
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        [DisplayName("Unit price")]
        public decimal? UnitPrice { get; set; }
        [DisplayName("Units in stock")]
        public short? UnitsInStock { get; set; }
        [Compare("UnitsInStock", ErrorMessage = "Units on order must be less than units in stock.")]
        [DisplayName("Units on order")]
        public short? UnitsOnOrder { get; set; }
        [DisplayName("Reorder Level")]
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
