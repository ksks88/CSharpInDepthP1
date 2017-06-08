using System.Collections.Generic;

namespace CSharpInDepthP1
{
    public class Product
    {
        public string Name { get; private set; }
        public decimal? Price { get; set; }

        public Product(string name, decimal? price = null)
        {
            Name = name;
            Price = price;
        }

        Product()
        {
        }

        public static List<Product> GetSampleList()
        {
            return new List<Product>()
            {
                new Product() { Name = "SalihP", Price = 22m },
                new Product(name: "SabanP", price: 13m),
                new Product() { Name = "SinanP", Price = 12m },
                new Product(name: "SabanP", price: 155m),
                new Product("SejoK")
            };
        }

        public override string ToString()
        {
            return string.Format("Name of product: {0}, Price of product: {1}", Name, Price);
        }
    }
}