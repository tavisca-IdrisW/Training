using System;
using System.IO;

namespace OperatorOverloading.dbl
{
    class FetchFromFile : JSONParse
    {
        private string _json;

        public string Json
        {
            get { return _json; }

            private set
            {
                // To check if the recieved string is indeed in JSON format.
                if (value.Trim().Substring(0, 1).IndexOfAny(new[] { '[', '{' }) != 0)
                {
                    throw new Exception(Messages.NotJson);
                }
                _json = value;
            }
        }

        public FetchFromFile(string path)
        {
            Json = File.ReadAllText(path);
        }

        /// <summary>
        /// Returns the Exchange Rate after parsing JSON.  
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="searchString"></param>
        /// <returns>ExchangeRate</returns>
        public double FetchRate(string searchString)
        {
            return FetchResult(Json, searchString);
        }

        public override double FetchResult(string JSON, string searchString)
        {
            // Additional Code, if any, comes here.
            // This is kind of my whole point of using inheritance here. 
            // This may not be required here. Please Comment. 
            // Thanks -IW
            double rate = base.FetchResult(JSON, searchString);
            return rate;
        }
    }
}
