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
            //Oefening1();
            //Oefening2();
            // Oefening3();
            Oefening4();
            Console.ReadKey();
        }

        private static void Oefening4()
        { 
            //4. Geef voor elke category de produkten die de laagste prijs hebben in deze category
            var oef4QuerySyntax = from prod in Products.ProductList
                                  group prod by prod.Category into prodGroep
                                  let minPrijs = (from p in prodGroep select p.UnitPrice).Min() //prodGroep.Min(p => p.UnitPrice) //tijdelijk variable minPrijs
                                  select new
                                  {
                                      Categorie = prodGroep.Key,
                                      ProductenAanMinimPrijs = from p in prodGroep where p.UnitPrice==minPrijs select p //prodGroep.Where(p => p.UnitPrice == minPrijs)
                                  };
            foreach(var item in oef4QuerySyntax)
            {
                Console.WriteLine("Categorie " + item.Categorie + ":" );
                foreach (var product in item.ProductenAanMinimPrijs)
                {
                    Console.WriteLine($"\tProduct: " + product.ProductName);
                }
            }
        }

        private static void Oefening3()
        {
            var oef3QueryNotatie = from prod in Products.ProductList
                                   group prod by prod.Category into prodGroup
                                   select new
                                   {
                                       Category = prodGroup.Key,
                                       AantalProducten = prodGroup.Count()
                                   };
            var oef3MethodNotatie = Products.ProductList.GroupBy(prod => prod.Category).Select(prodGroup => 
                new {
                    Category= prodGroup.Key,
                    AantalProducten = prodGroup.Count()
                });
           
            foreach (var item in oef3MethodNotatie)
            {
                 Console.WriteLine("Categorie:" + item.Category + "- Aantal Producten:" + item.AantalProducten);
                //Console.WriteLine(item);
            }
        }

        private static void Oefening2()
        {
            var oef2MethodNotatie = Products.ProductList.Where(p => p.Category == "Seafood")
                .Select(p => new { productNaam = p.ProductName, ID = p.ProductID })
                .FirstOrDefault();
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
