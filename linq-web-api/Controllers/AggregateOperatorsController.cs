﻿using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AggregateOperatorsController : ControllerBase
    {
        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int CountSyntax()
        {
            #region count-syntax
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };

            int uniqueFactors = factorsOf300.Distinct().Count();

            Console.WriteLine($"There are {uniqueFactors} unique factors of 300.");
            #endregion
            return 0;
        }
        [HttpGet]
        public int CountConditional()
        {
            #region count-conditional
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int oddNumbers = numbers.Count(n => n % 2 == 1);

            Console.WriteLine("There are {0} odd numbers in the list.", oddNumbers);
            #endregion
            return 0;
        }
        [HttpGet]
        public int NestedCount()
        {
            #region nested-count
            List<Customer> customers = GetCustomerList();

            var orderCounts = from c in customers
                              select (c.CustomerID, OrderCount: c.Orders.Count());

            foreach (var customer in orderCounts)
            {
                Console.WriteLine($"ID: {customer.CustomerID}, count: {customer.OrderCount}");
            }

            #endregion
            return 0;
        }
        [HttpGet]
        public int GroupedCount()
        {
            #region grouped-count
            List<Product> products = GetProductList();

            var categoryCounts = from p in products
                                 group p by p.Category into g
                                 select (Category: g.Key, ProductCount: g.Count());

            foreach (var c in categoryCounts)
            {
                Console.WriteLine($"Category: {c.Category}: Product count: {c.ProductCount}");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int SumSyntax()
        {
            #region sum-syntax
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            double numSum = numbers.Sum();

            Console.WriteLine($"The sum of the numbers is {numSum}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int SumProjection()
        {
            #region sum-of-projection
            string[] words = { "cherry", "apple", "blueberry" };

            double totalChars = words.Sum(w => w.Length);

            Console.WriteLine($"There are a total of {totalChars} characters in these words.");
            #endregion
            return 0;
        }
        [HttpGet]
        public int SumGrouped()
        {
            #region grouped-sum
            List<Product> products = GetProductList();

            var categories = from p in products
                             group p by p.Category into g
                             select (Category: g.Key, TotalUnitsInStock: g.Sum(p => p.UnitsInStock));

            foreach (var pair in categories)
            {
                Console.WriteLine($"Category: {pair.Category}, Units in stock: {pair.TotalUnitsInStock}");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int MinSyntax()
        {
            #region min-syntax
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int minNum = numbers.Min();

            Console.WriteLine($"The minimum number is {minNum}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int MinProjection()
        {
            #region min-projection
            string[] words = { "cherry", "apple", "blueberry" };

            int shortestWord = words.Min(w => w.Length);

            Console.WriteLine($"The shortest word is {shortestWord} characters long.");
            #endregion
            return 0;
        }
        [HttpGet]
        public int MinGrouped()
        {
            #region min-grouped
            List<Product> products = GetProductList();

            var categories = from p in products
                             group p by p.Category into g
                             select (Category: g.Key, CheapestPrice: g.Min(p => p.UnitPrice));

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Category}, Lowest price: {c.CheapestPrice}");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int MinEachGroup()
        {
            #region min-each-group
            List<Product> products = GetProductList();

            var categories = from p in products
                             group p by p.Category into g
                             let minPrice = g.Min(p => p.UnitPrice)
                             select (Category: g.Key, CheapestProducts: g.Where(p => p.UnitPrice == minPrice));

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Category}");
                foreach (var p in c.CheapestProducts)
                {
                    Console.WriteLine($"\tProduct: {p}");
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int MaxSyntax()
        {
            #region max-syntax
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int maxNum = numbers.Max();

            Console.WriteLine($"The maximum number is {maxNum}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int MaxProjection()
        {
            #region max-projection
            string[] words = { "cherry", "apple", "blueberry" };

            int longestLength = words.Max(w => w.Length);

            Console.WriteLine($"The longest word is {longestLength} characters long.");
            #endregion
            return 0;
        }
        [HttpGet]
        public int MaxGrouped()
        {
            #region max-grouped
            List<Product> products = GetProductList();

            var categories = from p in products
                             group p by p.Category into g
                             select (Category: g.Key, MostExpensivePrice: g.Max(p => p.UnitPrice));

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Category} Most expensive product: {c.MostExpensivePrice}");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int MaxEachGroup()
        {
            #region max-each-group
            List<Product> products = GetProductList();

            var categories = from p in products
                             group p by p.Category into g
                             let maxPrice = g.Max(p => p.UnitPrice)
                             select (Category: g.Key, MostExpensiveProducts: g.Where(p => p.UnitPrice == maxPrice));

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Category}");
                foreach (var p in c.MostExpensiveProducts)
                {
                    Console.WriteLine($"\t{p}");
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int AverageSyntax()
        {
            #region average-syntax
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            double averageNum = numbers.Average();

            Console.WriteLine($"The average number is {averageNum}.");
            #endregion
            return 0;
        }
        [HttpGet]
        public int AverageProjection()
        {
            #region average-projection
            string[] words = { "cherry", "apple", "blueberry" };

            double averageLength = words.Average(w => w.Length);

            Console.WriteLine($"The average word length is {averageLength} characters.");
            #endregion
            return 0;
        }
        [HttpGet]
        public int AverageGrouped()
        {
            #region average-grouped
            List<Product> products = GetProductList();

            var categories = from p in products
                             group p by p.Category into g
                             select (Category: g.Key, AveragePrice: g.Average(p => p.UnitPrice));

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Category}, Average price: {c.AveragePrice}");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int AggregateSyntax()
        {
            #region aggregate-syntax
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

            Console.WriteLine($"Total product of all numbers: {product}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int SeededAggregate()
        {
            #region aggregate-seeded
            double startBalance = 100.0;

            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };

            double endBalance =
                attemptedWithdrawals.Aggregate(startBalance,
                    (balance, nextWithdrawal) =>
                        nextWithdrawal <= balance ? balance - nextWithdrawal : balance);

            Console.WriteLine($"Ending balance: {endBalance}");
            #endregion
            return 0;
        }
    }
}
