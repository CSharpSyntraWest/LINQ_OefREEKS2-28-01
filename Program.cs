using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_OefREEKS2_27_01
{
 
    class Program
    {
 
        static void Main(string[] args)
        {
            //GroupJoinQuerySyntax();
           // GroupJoinMethodSyntax();
            //foreach (var p in Products.ProductList)
            //{
            //    Console.WriteLine(p);
            //}

            //foreach (var c in Customers.CustomerList)
            //{
            //    Console.WriteLine("Customer: " + c);
            //    Console.WriteLine("Orders:");
            //    foreach (var order in c.Orders)
            //    {
            //        Console.WriteLine("\t" + order);
            //    }
            //}
            Oefening1();
            Console.ReadKey();
        }

        private static void Oefening1()
        {
            var query = (from prod in Products.ProductList
                         select prod.Category).Distinct();

            foreach (string prodnaam in query)
                Console.WriteLine(prodnaam);
        }

        public static int GroupJoinMethodSyntax()
        {
            #region group-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };
            List<Product> products = Products.ProductList;
            var q = categories.GroupJoin(products,  //inner sequence
                                cat => cat, //outerKeySelector 
                                prod => prod.Category,     //innerKeySelector
                                (cat, prodGroep) => new // resultSelector 
                                {
                                    Products = prodGroep,
                                    Naam = cat
                                });

            foreach (var item in q)
            {
                Console.WriteLine(item.Naam);

                foreach (var prod in item.Products)
                    Console.WriteLine("\t" + prod.ProductName);
            }


            #endregion
            return 0;
        }
        public static int GroupJoinQuerySyntax()
        {
            #region group-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = Products.ProductList;

            var q = from c in categories
                    join p in products on c equals p.Category into prodGroep
                    select (Category: c, Products: prodGroep);

            foreach (var v in q)
            {
                Console.WriteLine(v.Category + ":");
                foreach (var p in v.Products)
                {
                    Console.WriteLine("\t" + p.ProductName);
                }
            }
            #endregion
            return 0;
        }
    }
}
