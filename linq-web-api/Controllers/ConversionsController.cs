using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace linq_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConversionsController : ControllerBase
    {
        private readonly ILogger<ConversionsController> logger;

        public ConversionsController(ILogger<ConversionsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public int ConvertToArray()
        {
            #region convert-to-array
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            var sortedDoubles = from d in doubles
                                orderby d descending
                                select d;
            var doublesArray = sortedDoubles.ToArray();

            logger.LogInformation("Every other double from highest to lowest:");
            for (int d = 0; d < doublesArray.Length; d += 2)
            {
                logger.LogInformation(doublesArray[d].ToString());
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ConvertToList()
        {
            #region convert-to-list
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords = from w in words
                              orderby w
                              select w;
            var wordList = sortedWords.ToList();

            logger.LogInformation("The sorted word list:");
            foreach (var w in wordList)
            {
                logger.LogInformation(w);
            }
            #endregion
            return 0;
        }
        [HttpGet]
        public int ConvertToDictionary()
        {
            #region convert-to-dictionary
            var scoreRecords = new[] { new {Name = "Alice", Score = 50},
                                new {Name = "Bob"  , Score = 40},
                                new {Name = "Cathy", Score = 45}
                            };

            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);

            logger.LogInformation("Bob's score: {0}", scoreRecordsDict["Bob"]);
            #endregion
            return 0;
        }
        [HttpGet]
        public int ConvertSelectedItems()
        {
            #region convert-to-type
            object[] numbers = { null, 1.0, "two", 3, "four", 5, "six", 7.0 };

            var doubles = numbers.OfType<double>();

            logger.LogInformation("Numbers stored as doubles:");
            foreach (var d in doubles)
            {
                logger.LogInformation(d.ToString());
            }
            #endregion
            return 0;
        }
    }
}
