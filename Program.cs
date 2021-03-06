﻿using System;
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
            //Oefening4();
            //Oefening5();
            //Oefening6();
            //Oefening7();
            //Oefening8();
            //Oefening9();
            Oefening10();
            Console.ReadKey();
        }

        private static void Oefening10()
        {
            bool b_oef10 = Products.ProductList.Any(p => p.ProductName.Contains("Coffee"));
            Console.WriteLine("Bestaat er een product waarvan de Naam 'Coffee' bevat?" + b_oef10);

        }

        private static void Oefening9()
        {
            //Geef het eerste element terug uit products waarvan de Category="Beverages"
            var oef9MethodeNotatie = Products.ProductList.Where(p => p.Category == "Beverages").Take(1);
            var oef9MethodNotatie2 = Products.ProductList.Where(p => p.Category == "Beverages").FirstOrDefault();
            Product eersteProductUitCatBeverages = (from p in Products.ProductList
                                    where p.Category == "Beverages"
                                    select p).FirstOrDefault(); //of .Take(1) 
            Console.WriteLine(eersteProductUitCatBeverages);
        }

        private static void Oefening8()
        {
            var oef8MethodNotatie = Products.ProductList.SkipWhile(p => p.UnitPrice < 20.00M);
            var oef8QueryNotatie = (from p in Products.ProductList select p).SkipWhile(p => p.UnitPrice < 20.00M);
            foreach (var item in oef8MethodNotatie)
            {
                Console.WriteLine(item);
            }
        }

        private static void Oefening7()
        {
            //7. Geef de eerste 5 produkten uit de lijst waarvan het aantal unitsInStock =0
            var oef7MethodNotatieList = Products.ProductList.Where(p => p.UnitsInStock == 0).Take(5).ToList();
            var oef7QueryNotatieList = (from p in Products.ProductList
                                    where p.UnitsInStock == 0
                                    select p).Take(5).ToList();
            if(oef7MethodNotatieList.Count() == 0)
                Console.WriteLine("Alle producten hebben UnitsInStock > 0");
            else
                oef7MethodNotatieList.ForEach(p =>  Console.WriteLine(p));

        }

        private static void Oefening6()
        {
            //6.Geef de lijst van alle productNamen en hun categoryNaam
            var oef6MethodNotatie = Products.ProductList.Select(b => new { ProductNaam = b.ProductName, CategorieNaam = b.Category });
            var oef6QueryNotatieList = (from prod in Products.ProductList
                                   select new
                                   {
                                       ProductNaam = prod.ProductName,
                                       CategorieNaam = prod.Category
                                   }).ToList();
            oef6QueryNotatieList.ForEach(item => Console.WriteLine(item));

        }

        private static void Oefening5()
        {//5. Geef de gemiddelde prijs van de produkten per category
            var oef5MethodSyntax = Products.ProductList.GroupBy(prod => prod.Category).Select(prodGroup =>
                        new {
                            Category = prodGroup.Key,
                            GemPrijs = prodGroup.Average(p => p.UnitPrice)
                        });

            var oef5QuerySyntax = from prod in Products.ProductList
                      group prod by prod.Category into prodGroep
                      select new
                      {
                          Category = prodGroep.Key,
                          GemPrijs = prodGroep.Average(p => p.UnitPrice)
                      };
            //
            foreach (var item in oef5QuerySyntax)
            {
                Console.WriteLine($"Categorie: {item.Category} - Gemiddelde Prijs: " + item.GemPrijs.ToString("F2") );
            }
           
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
            var firstItemMethodNotatie = Products.ProductList.Where(p => p.Category == "Seafood")
                .Select(p => new { productNaam = p.ProductName, ID = p.ProductID })
                .FirstOrDefault();

            var firstItemQueryNotatie = (from p in Products.ProductList
                                   where p.Category == "Seafood"
                                   select new
                                   {
                                       ID = p.ProductID,
                                       ProductNaam = p.ProductName
                                   }).FirstOrDefault();

            Console.WriteLine("Eerste product in de lijst van category 'Seafood': " + firstItemQueryNotatie);
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
