using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class QueryExecutionController : ControllerBase
    {
        private readonly ILogger<QueryExecutionController> logger;

        public QueryExecutionController(ILogger<QueryExecutionController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public int DeferredExecution()
        {
            #region deferred-execution
            // Sequence operators form first-class queries that
            // are not executed until you enumerate over them.

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var q = from n in numbers
                    select ++i;

            // Note, the local variable 'i' is not incremented
            // until each element is evaluated (as a side-effect):
            foreach (var v in q)
            {
                logger.LogInformation($"v = {v}, i = {i}");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int EagerExecution()
        {
            #region eager-execution
            // Methods like ToList() cause the query to be
            // executed immediately, caching the results.

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var q = (from n in numbers
                     select ++i)
                     .ToList();

            // The local variable i has already been fully
            // incremented before we iterate the results:
            foreach (var v in q)
            {
                logger.LogInformation($"v = {v}, i = {i}");
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ReuseQueryDefinition()
        {
            #region reuse-query
            // Deferred execution lets us define a query once
            // and then reuse it later after data changes.

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var lowNumbers = from n in numbers
                             where n <= 3
                             select n;

            logger.LogInformation("First run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                logger.LogInformation(n.ToString());
            }

            for (int i = 0; i < 10; i++)
            {
                numbers[i] = -numbers[i];
            }

            // During this second run, the same query object,
            // lowNumbers, will be iterating over the new state
            // of numbers[], producing different results:
            logger.LogInformation("Second run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                logger.LogInformation(n.ToString());
            }
            #endregion
            return 0;
        }
    }
}
