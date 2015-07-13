using System;
using System.Collections.Generic;

namespace OperatorOverloading.DBL
{
    public abstract class JSONParse
    {
        /// <summary>
        /// Returns the Exchange Rate after parsing JSON.  
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="searchString"></param>
        /// <returns>ExchangeRate</returns>


        public Dictionary<string, string> GetItems(string jsonString)
        {
            if (String.IsNullOrWhiteSpace(jsonString))
            {
                throw new Exception(Messages.InvalidResponse);
            }

            Dictionary<string, string> items = new Dictionary<string, string>();
            var keyArray = jsonString.Split('{', '}', '[', ']');
            foreach (string str in keyArray)
            {
                var keysArray = str.Split(',');
                foreach (string mykey in keysArray)
                {
                    var jsonKeys = mykey.Split(':');

                    //2 as it should have key and value pair only. We continue and check more.
                    if (jsonKeys.Length != 2)
                    {
                        continue;
                    }
                    items.Add(jsonKeys[0], jsonKeys[1]);
                }
            }
            return items;
        }
    }
}
