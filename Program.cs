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
        delegate int CompareProducts(Product x, Product y);

        public async void CreateAndInvokeMethod()
        {
            Product bananas = new Product("Bananas", 12m);
            Product apples = new Product("Apples", 7m);

            Func<Product, Product, Task<int>> item = async delegate(Product c, Product z) {
                int randomResult = 4;
                await Task.Delay(2000);
                return randomResult;
            };

            var result  = item.Invoke(bananas, apples);

            CompareProducts comparer = this.Compare;
            Console.WriteLine("Product(price): {0} - {1} vs Product(price): {2} - {3}, sort value: {4}.", bananas.Name, bananas.Price, apples.Name, apples.Price, comparer(bananas, apples));

            Console.WriteLine("Value of x: {0}.", await result);

        }
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
        delegate void PrintLine(Product p);
        delegate int CompareProducts(Product x, Product y);

        public event EventHandler MyEvent
        {
            add
            {
                Console.WriteLine("add operation");
            }

            remove
            {
                Console.WriteLine("remove operation");
            }
        }

        CompareProducts ProductsDelegate { get; set; }
        
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

            Console.WriteLine("=======================DELEGATES===================\n");

            PrintLine p1 = new PrintLine(Program.WriteLine);
            prodList.FindAll(x=>x.Price<20).ForEach(x => p1(x));
            Console.WriteLine("===================================================\n");

            var comparer = new ProductNameComparer();
            CompareProducts cp = new CompareProducts(comparer.Compare);
            var tempList = prodList.Take(2);
            Console.WriteLine("Product(price): {0} - {1} vs Product(price): {2} - {3}, sort value: {4}.", prodList[0].Name, prodList[0].Price, prodList[1].Name, prodList[1].Price, cp(prodList[0], prodList[1]));
            Console.WriteLine("===================================================\n");

            comparer.CreateAndInvokeMethod();
            Console.WriteLine("===================================================\n");

            Console.WriteLine("==============DELEGATES INVOCATION LIST============\n");

            Console.WriteLine("Adding 2 more methods...\n");
            p1 += Console.WriteLine;
            p1 += Console.WriteLine;
            Console.WriteLine("Number of methods assigned to delegate: " + p1.GetInvocationList().Length);
            p1.Invoke(prodList.First(x=>x.Price>100));
            Console.WriteLine("Removing 1 method...\n");
            p1 -= Console.WriteLine;
            Console.WriteLine("Number of methods assigned to delegate: " + p1.GetInvocationList().Length);
            p1.Invoke(prodList.First(x => x.Price > 100));

            Console.WriteLine("===================================================\n");

            Console.WriteLine("=======================EVENTS======================\n");

            Program t = new Program();

            t.MyEvent += new EventHandler(t.DoNothing);
            t.MyEvent -= null;

            Console.Read();
        }

        void DoNothing(object sender, EventArgs e)
        {
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
