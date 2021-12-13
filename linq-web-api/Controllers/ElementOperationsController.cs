using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ElementOperationsController : ControllerBase
    {
        private readonly ILogger<ElementOperationsController> logger;

        public ElementOperationsController(ILogger<ElementOperationsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int FirstElement()
        {
            #region first-element
            List<Product> products = GetProductList();

            Product product12 = (from p in products
                                 where p.ProductID == 12
                                 select p)
                                 .First();

            logger.LogInformation(product12.ToString());
            #endregion
            return 0;
        }
        [HttpGet]
        public int FirstMatchingElement()
        {
            #region first-matching-element
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            string startsWithO = strings.First(s => s[0] == 'o');

            logger.LogInformation($"A string starting with 'o': {startsWithO}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int MaybeFirstElement()
        {
            #region first-or-default
            int[] numbers = { };

            int firstNumOrDefault = numbers.FirstOrDefault();

            logger.LogInformation(firstNumOrDefault.ToString());
            #endregion
            return 0;
        }
        [HttpGet]
        public int MaybeFirstMatchingElement()
        {
            #region first-matching-or-default
            List<Product> products = GetProductList();

            Product product789 = products.FirstOrDefault(p => p.ProductID == 789);

            logger.LogInformation($"Product 789 exists: {product789 != null}");
            #endregion
            return 0;
        }
        [HttpGet]
        public int ElementAtPosition()
        {
            #region element-at
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int fourthLowNum = (
                from n in numbers
                where n > 5
                select n)
                .ElementAt(1);  // second number is index 1 because sequences use 0-based indexing

            logger.LogInformation($"Second number > 5: {fourthLowNum}");
            #endregion
            return 0;
        }
    }
}
