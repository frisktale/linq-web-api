using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SetOperationsController : ControllerBase
    {
        private readonly ILogger<SetOperationsController> logger;

        public SetOperationsController(ILogger<SetOperationsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int DistinctSyntax()
        {
            #region distinct-syntax
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };

            var uniqueFactors = factorsOf300.Distinct();

            logger.LogInformation("Prime factors of 300:");
            foreach (var f in uniqueFactors)
            {
                logger.LogInformation(f.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int DistinctPropertyValues()
        {
            #region distinct-property-values
            List<Product> products = GetProductList();

            var categoryNames = (from p in products
                                 select p.Category)
                                 .Distinct();

            logger.LogInformation("Category names:");
            foreach (var n in categoryNames)
            {
                logger.LogInformation(n);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int UnionSyntax()
        {
            #region union-syntax
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var uniqueNumbers = numbersA.Union(numbersB);

            logger.LogInformation("Unique numbers from both arrays:");
            foreach (var n in uniqueNumbers)
            {
                logger.LogInformation(n.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int UnionOfQueryResults()
        {
            #region union-query-results
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();

            var productFirstChars = from p in products
                                    select p.ProductName[0];
            var customerFirstChars = from c in customers
                                     select c.CompanyName[0];

            var uniqueFirstChars = productFirstChars.Union(customerFirstChars);

            logger.LogInformation("Unique first letters from Product names and Customer names:");
            foreach (var ch in uniqueFirstChars)
            {
                logger.LogInformation(ch.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int IntersectSynxtax()
        {
            #region intersect-syntax
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var commonNumbers = numbersA.Intersect(numbersB);

            logger.LogInformation("Common numbers shared by both arrays:");
            foreach (var n in commonNumbers)
            {
                logger.LogInformation(n.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int IntersectQueryResults()
        {
            #region intersect-different-queries
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();

            var productFirstChars = from p in products
                                    select p.ProductName[0];
            var customerFirstChars = from c in customers
                                     select c.CompanyName[0];

            var commonFirstChars = productFirstChars.Intersect(customerFirstChars);

            logger.LogInformation("Common first letters from Product names and Customer names:");
            foreach (var ch in commonFirstChars)
            {
                logger.LogInformation(ch.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int DifferenceOfSets()
        {
            #region except-syntax
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            IEnumerable<int> aOnlyNumbers = numbersA.Except(numbersB);

            logger.LogInformation("Numbers in first array but not second array:");
            foreach (var n in aOnlyNumbers)
            {
                logger.LogInformation(n.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int DifferenceOfQueries()
        {
            #region difference-of-queries
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();

            var productFirstChars = from p in products
                                    select p.ProductName[0];
            var customerFirstChars = from c in customers
                                     select c.CompanyName[0];

            var productOnlyFirstChars = productFirstChars.Except(customerFirstChars);

            logger.LogInformation("First letters from Product names, but not from Customer names:");
            foreach (var ch in productOnlyFirstChars)
            {
                logger.LogInformation(ch.ToString());
            }
            #endregion
            return 1;
        }
    }
}
