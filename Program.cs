using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepthP1
{
    public class ProductNameComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> prodList = Product.GetSampleList();
            prodList.Sort(new ProductNameComparer());
            WriteList(prodList);

            prodList.Sort(delegate (Product x, Product y) { return x.Name.CompareTo(y.Name); });
            WriteList(prodList);
        }

        static void WriteList(List<Product> list)
        {
            foreach (Product item in list)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("===================================================\n");
        }
    }
}
