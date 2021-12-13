using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class QuantifiersController : ControllerBase
    {
        private readonly ILogger<QuantifiersController> logger;

        public QuantifiersController(ILogger<QuantifiersController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int AnyMatchingElements()
        {
            #region any-matches
            string[] words = { "believe", "relief", "receipt", "field" };

            bool iAfterE = words.Any(w => w.Contains("ei"));

            logger.LogInformation($"There is a word that contains in the list that contains 'ei': {iAfterE}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int GroupedAnyMatchedElements()
        {
            #region any-grouped
            List<Product> products = GetProductList();
            var productGroups = from p in products
                                group p by p.Category into g
                                where g.Any(p => p.UnitsInStock == 0)
                                select (Category: g.Key, Products: g);

            foreach (var group in productGroups)
            {
                logger.LogInformation(group.Category);
                foreach (var product in group.Products)
                {
                    logger.LogInformation($"\t{product}");
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int AllMatchedElements()
        {
            #region all-match
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };

            bool onlyOdd = numbers.All(n => n % 2 == 1);

            logger.LogInformation($"The list contains only odd numbers: {onlyOdd}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int GroupedAllMatchedElements()
        {
            #region all-grouped
            List<Product> products = GetProductList();

            var productGroups = from p in products
                                group p by p.Category into g
                                where g.All(p => p.UnitsInStock > 0)
                                select (Category: g.Key, Products: g);

            foreach (var group in productGroups)
            {
                logger.LogInformation(group.Category);
                foreach (var product in group.Products)
                {
                    logger.LogInformation($"\t{product}");
                }
            }
            #endregion
            return 0;
        }
    }
}
