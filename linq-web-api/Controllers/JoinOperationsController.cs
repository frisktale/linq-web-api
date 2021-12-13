using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JoinOperationsController : ControllerBase
    {
        private readonly ILogger<JoinOperationsController> logger;

        public JoinOperationsController(ILogger<JoinOperationsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        [HttpGet]
        public int CrossJoinQuery()
        {
            #region cross-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category
                    select (Category: c, p.ProductName);

            foreach (var v in q)
            {
                logger.LogInformation(v.ProductName + ": " + v.Category);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int GroupJoinQquery()
        {
            #region group-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    select (Category: c, Products: ps);

            foreach (var v in q)
            {
                logger.LogInformation(v.Category + ":");
                foreach (var p in v.Products)
                {
                    logger.LogInformation("   " + p.ProductName);
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int CrossGroupJoin()
        {
            #region cross-group-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    from p in ps
                    select (Category: c, p.ProductName);

            foreach (var v in q)
            {
                logger.LogInformation(v.ProductName + ": " + v.Category);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int LeftOuterJoin()
        {
            #region left-outer-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    from p in ps.DefaultIfEmpty()
                    select (Category: c, ProductName: p == null ? "(No products)" : p.ProductName);

            foreach (var v in q)
            {
                logger.LogInformation($"{v.ProductName}: {v.Category}");
            }
            #endregion
            return 0;
        }
    }
}


