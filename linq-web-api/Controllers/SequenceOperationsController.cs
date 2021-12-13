using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SequenceOperationsController : ControllerBase
    {
        private readonly ILogger<SequenceOperationsController> logger;

        public SequenceOperationsController(ILogger<SequenceOperationsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int EqualSequence()
        {
            #region equal-sequence
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "cherry", "apple", "blueberry" };

            bool match = wordsA.SequenceEqual(wordsB);

            logger.LogInformation($"The sequences match: {match}");
            #endregion
            return 0;
        }
        [HttpGet]
        // Combine in Markdown.s
        public int Linq97()
        {
            #region not-equal-sequence
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "apple", "blueberry", "cherry" };

            bool match = wordsA.SequenceEqual(wordsB);

            logger.LogInformation($"The sequences match: {match}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int ConcatSeries()
        {
            #region concat-series
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var allNumbers = numbersA.Concat(numbersB);

            logger.LogInformation("All numbers from both arrays:");
            foreach (var n in allNumbers)
            {
                logger.LogInformation(n.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ConcatProjection()
        {
            #region concat-projections
            List<Customer> customers = GetCustomerList();
            List<Product> products = GetProductList();

            var customerNames = from c in customers
                                select c.CompanyName;
            var productNames = from p in products
                               select p.ProductName;

            var allNames = customerNames.Concat(productNames);

            logger.LogInformation("Customer and product names:");
            foreach (var n in allNames)
            {
                logger.LogInformation(n);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int DotProduct()
        {
            #region dot-product
            int[] vectorA = { 0, 2, 4, 5, 6 };
            int[] vectorB = { 1, 3, 5, 7, 8 };

            int dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();

            logger.LogInformation($"Dot product: {dotProduct}");
            #endregion
            return 0;
        }
    }
}
