using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RestrictionsController : ControllerBase
    {
        private readonly ILogger<RestrictionsController> logger;

        public RestrictionsController(ILogger<RestrictionsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;

        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int LowNumbers()
        {
            #region where-syntax
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums = from num in numbers
                          where num < 5
                          select num;

            logger.LogInformation("Numbers < 5:");
            foreach (var x in lowNums)
            {
                logger.LogInformation(x.ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ProductsOutOfStock()
        {
            #region where-property
            List<Product> products = GetProductList();

            var soldOutProducts = from prod in products
                                  where prod.UnitsInStock == 0
                                  select prod;

            logger.LogInformation("Sold out products:");
            foreach (var product in soldOutProducts)
            {
                logger.LogInformation($"{product.ProductName} is sold out!");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ExpensiveProductsInStock()
        {
            #region where-multiple-properties
            List<Product> products = GetProductList();

            var expensiveInStockProducts = from prod in products
                                           where prod.UnitsInStock > 0 && prod.UnitPrice > 3.00M
                                           select prod;

            logger.LogInformation("In-stock products that cost more than 3.00:");
            foreach (var product in expensiveInStockProducts)
            {
                logger.LogInformation($"{product.ProductName} is in stock and costs more than 3.00.");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int DisplayCustomerOrders()
        {
            #region where-drilldown
            List<Customer> customers = GetCustomerList();

            var waCustomers = from cust in customers
                              where cust.Region == "WA"
                              select cust;

            logger.LogInformation("Customers from Washington and their orders:");
            foreach (var customer in waCustomers)
            {
                logger.LogInformation($"Customer {customer.CustomerID}: {customer.CompanyName}");
                foreach (var order in customer.Orders)
                {
                    logger.LogInformation($"  Order {order.OrderID}: {order.OrderDate}");
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int IndexedWhere()
        {
            #region where-indexed
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var shortDigits = digits.Where((digit, index) => digit.Length < index);

            logger.LogInformation("Short digits:");
            foreach (var d in shortDigits)
            {
                logger.LogInformation($"The word {d} is shorter than its value.");
            }
            #endregion
            return 0;
        }
    }
}
