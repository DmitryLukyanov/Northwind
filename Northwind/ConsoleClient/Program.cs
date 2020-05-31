using Newtonsoft.Json;
using Northwind.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {

        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Northwind database. Press C to get categories, press P to get products.");

            var key = Console.ReadKey();

            client.BaseAddress = new Uri("https://localhost:44354/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (key.KeyChar.ToString().ToLower().Equals("c"))
            {
                var categories = GetCategories();
                foreach (var category in categories.Result)
                {
                    Console.WriteLine($"{category.CategoryName} : {category.Description}");
                }
            }

            if (key.KeyChar.ToString().ToLower().Equals("p"))
            {
                var products = GetProducts();
                foreach (var product in products.Result)
                {
                    Console.WriteLine($"{product.ProductName}");
                }
            }

            Console.ReadLine();
        }

        private static async Task<IEnumerable<Category>> GetCategories()
        {

            IEnumerable<Category> categories = null;
            HttpResponseMessage response = await client.GetAsync("api/categories");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(result);
            }
            return categories;

        }

        private static async Task<IEnumerable<Product>> GetProducts()
        {

            IEnumerable<Product> products = null;
            HttpResponseMessage response = await client.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(result);
            }
            return products;

        }
    }
}
