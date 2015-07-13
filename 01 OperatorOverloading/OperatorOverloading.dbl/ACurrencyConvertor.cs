using System;
using System.Collections.Generic;
using System.Configuration;

namespace OperatorOverloading.DBL
{
    /// <summary>
    /// -------------COMMENT. NOT A SUMMARY-------------------------
    /// This is an Adapter to the CurrencyConvertor class.
    /// Here teh logic for the Fetching of JSON data is to be done. 
    /// The logic, here, would be to check where the JSON data is 
    /// being fetched from (i.e.: Web / File / DB etc.) 
    /// and what additional operations(if any) are to be done.
    /// For now this location will be decided by app.config.
    /// </summary>
    public class ACurrencyConvertor
    {
        public Dictionary<string, string> GetRate()
        {
            Dictionary<string, string> items;
            string path = ConfigurationManager.AppSettings["path"] + ConfigurationManager.AppSettings["access_key"];

            if (new Uri(path).IsFile)
            {
                var fileObj = new FetchFromFile(path);
                items = fileObj.FetchRate();
            }

            else
            {
                var webObj = new FetchFromWeb(path);
                items = webObj.FetchRate();
            }
            return items;
        }
    }
}
