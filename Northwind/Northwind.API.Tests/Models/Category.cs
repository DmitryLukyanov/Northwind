// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Northwind.API.Tests.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Category
    {
        /// <summary>
        /// Initializes a new instance of the Category class.
        /// </summary>
        public Category()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Category class.
        /// </summary>
        public Category(int? categoryId = default(int?), string categoryName = default(string), string description = default(string), byte[] picture = default(byte[]), IList<Product> products = default(IList<Product>))
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description;
            Picture = picture;
            Products = products;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CategoryId")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CategoryName")]
        public string CategoryName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Picture")]
        public byte[] Picture { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Products")]
        public IList<Product> Products { get; set; }

    }
}