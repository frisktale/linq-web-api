using System;
using System.Linq;
using System.Collections.Generic;
using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderingsController : ControllerBase
    {
        private readonly ILogger<OrderingsController> logger;

        public OrderingsController(ILogger<OrderingsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int OrderbySyntax()
        {
            #region orderby-syntax
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords = from word in words
                              orderby word
                              select word;

            logger.LogInformation("The sorted list of words:");
            foreach (var w in sortedWords)
            {
                logger.LogInformation(w);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int OrderbyProperty()
        {
            #region orderby-property
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords = from word in words
                              orderby word.Length
                              select word;

            logger.LogInformation("The sorted list of words (by length):");
            foreach (var w in sortedWords)
            {
                logger.LogInformation(w);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int OrderByProducts()
        {
            #region orderby-user-types
            List<Product> products = GetProductList();

            var sortedProducts = from prod in products
                                 orderby prod.ProductName
                                 select prod;

            foreach (var product in sortedProducts)
            {
                logger.LogInformation(product.ToString());
            }
            #endregion
            return 0;
        }

        #region custom-comparer
        // Custom comparer for use with ordering operators
        public class CaseInsensitiveComparer : IComparer<string>
        {
            public int Compare(string x, string y) =>
                string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }
        #endregion
        [HttpGet]
        public int OrderByWithCustomComparer()
        {
            #region orderby-custom-comparer
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());

            foreach (var word in sortedWords)
            {
                logger.LogInformation(word);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int OrderByDescendingSyntax()
        {
            #region orderbydescending-syntax
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            var sortedDoubles = from d in doubles
                                orderby d descending
                                select d;

            logger.LogInformation("The doubles from highest to lowest:");
            foreach (var d in sortedDoubles)
            {
                logger.LogInformation(d.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int OrderProductsDescending()
        {
            #region orderby-descending-type
            List<Product> products = GetProductList();

            var sortedProducts = from prod in products
                                 orderby prod.UnitsInStock descending
                                 select prod;

            foreach (var product in sortedProducts)
            {
                logger.LogInformation(product.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int DescendingCustomComparer()
        {
            #region desc-custom-comparer
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

            foreach (var word in sortedWords)
            {
                logger.LogInformation(word);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ThenBySyntax()
        {
            #region thenby-syntax
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var sortedDigits = from digit in digits
                               orderby digit.Length, digit
                               select digit;

            logger.LogInformation("Sorted digits:");
            foreach (var d in sortedDigits)
            {
                logger.LogInformation(d);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ThenByCustom()
        {
            #region thenby-custom
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words
                .OrderBy(a => a.Length)
                .ThenBy(a => a, new CaseInsensitiveComparer());

            foreach (var word in sortedWords)
            {
                logger.LogInformation(word);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ThenByDifferentOrdering()
        {
            #region thenby-ordering
            List<Product> products = GetProductList();

            var sortedProducts = from prod in products
                                 orderby prod.Category, prod.UnitPrice descending
                                 select prod;

            foreach (var product in sortedProducts)
            {
                logger.LogInformation(product.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int CustomThenByDescending()
        {
            #region thenby-custom-descending
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words
                .OrderBy(a => a.Length)
                .ThenByDescending(a => a, new CaseInsensitiveComparer());

            foreach (var word in sortedWords)
            {
                logger.LogInformation(word);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int OrderingReversal()
        {
            #region reverse
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var reversedIDigits = (
                from digit in digits
                where digit[1] == 'i'
                select digit)
                .Reverse();

            logger.LogInformation("A backwards list of the digits with a second character of 'i':");
            foreach (var d in reversedIDigits)
            {
                logger.LogInformation(d);
            }
            #endregion
            return 0;
        }
    }
}
