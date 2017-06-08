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
            if (!x.Price.HasValue || !y.Price.HasValue)
                return -1;
            else
                return x.Price.Value.CompareTo(y.Price.Value);
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

            Predicate<Product> test10 = delegate (Product x) { return x.Price>10m; };
            List<Product> matches = prodList.FindAll(test10);
            Action<Product> print = WriteLine;//Console.WriteLine;
            matches.ForEach(print);
            Console.WriteLine("===================================================\n");

            Predicate<Product> testNull = delegate (Product x) { return x.Price == null; };
            matches = prodList.FindAll(testNull);
            matches.ForEach(print);
            Console.WriteLine("===================================================\n");

            Predicate<Product> test100 = delegate (Product x) { return x.Price > 100m; };
            prodList.FindAll(test100).ForEach(x => WriteLine(x));
            Console.WriteLine("===================================================\n");

            Console.Read();
        }
        static void WriteLine(Product p)
        {
            Console.WriteLine(p);
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
