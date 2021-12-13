using linq_web_api.DataSources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GroupingsController : ControllerBase
    {
        private readonly ILogger<GroupingsController> logger;

        public GroupingsController(ILogger<GroupingsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<Product> GetProductList() => Products.ProductList;
        [HttpGet]
        public List<Customer> GetCustomerList() => Customers.CustomerList;

        [HttpGet]
        public int GroupingSyntax()
        {
            #region groupby-syntax
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var numberGroups = from n in numbers
                               group n by n % 5 into g
                               select (Remainder: g.Key, Numbers: g);

            foreach (var g in numberGroups)
            {
                logger.LogInformation($"Numbers with a remainder of {g.Remainder} when divided by 5:");
                foreach (var n in g.Numbers)
                {
                    logger.LogInformation(n.ToString());
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int GroupByProperty()
        {
            #region groupby-property
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };

            var wordGroups = from w in words
                             group w by w[0] into g
                             select (FirstLetter: g.Key, Words: g);

            foreach (var g in wordGroups)
            {
                logger.LogInformation("Words that start with the letter '{0}':", g.FirstLetter);
                foreach (var w in g.Words)
                {
                    logger.LogInformation(w);
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int GroupByCategory()
        {
            #region groupby-category
            List<Product> products = GetProductList();

            var orderGroups = from p in products
                              group p by p.Category into g
                              select (Category: g.Key, Products: g);

            foreach (var orderGroup in orderGroups)
            {
                logger.LogInformation($"Products in {orderGroup.Category} category:");
                foreach (var product in orderGroup.Products)
                {
                    logger.LogInformation($"\t{product}");
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int NestedGrouBy()
        {
            #region nested-groupby
            List<Customer> customers = GetCustomerList();

            var customerOrderGroups = from c in customers
                                      select
                                      (
                                      c.CompanyName,
                                      YearGroups: from o in c.Orders
                                                  group o by o.OrderDate.Year into yg
                                                  select
                                                  (
                                                  Year: yg.Key,
                                                  MonthGroups: from o in yg
                                                               group o by o.OrderDate.Month into mg
                                                               select (Month: mg.Key, Orders: mg)
                                                  )
                                      );

            foreach (var ordersByCustomer in customerOrderGroups)
            {
                logger.LogInformation($"Customer Name: {ordersByCustomer.CompanyName}");
                foreach (var ordersByYear in ordersByCustomer.YearGroups)
                {
                    logger.LogInformation($"\tYear: {ordersByYear.Year}");
                    foreach (var ordersByMonth in ordersByYear.MonthGroups)
                    {
                        logger.LogInformation($"\t\tMonth: {ordersByMonth.Month}");
                        foreach (var order in ordersByMonth.Orders)
                        {
                            logger.LogInformation($"\t\t\tOrder: {order}");
                        }
                    }
                }
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int GroupByCustomComparer()
        {
            #region groupby-custom-comparer
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };

            var orderGroups = anagrams.GroupBy(w => w.Trim(), new AnagramEqualityComparer());

            foreach (var set in orderGroups)
            {
                // The key would be the first item in the set
                foreach (var word in set)
                {
                    logger.LogInformation(word);
                }
                logger.LogInformation("...");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int NestedGroupByCustom()
        {
            #region nested-groupby-custom
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };

            var orderGroups = anagrams.GroupBy(
                        w => w.Trim(),
                        a => a.ToUpper(),
                        new AnagramEqualityComparer()
                        );
            foreach (var set in orderGroups)
            {
                logger.LogInformation(set.Key);
                foreach (var word in set)
                {
                    logger.LogInformation($"\t{word}");
                }
            }

            #endregion
            return 0;
        }
    }

    #region anagram-comparer
    public class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => getCanonicalString(x) == getCanonicalString(y);

        public int GetHashCode(string obj) => getCanonicalString(obj).GetHashCode();

        private string getCanonicalString(string word)
        {
            char[] wordChars = word.ToCharArray();
            Array.Sort(wordChars);
            return new string(wordChars);
        }
    }
    #endregion
}
