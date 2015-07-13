using System;
using System.Collections.Generic;
using System.Net;

namespace OperatorOverloading.DBL
{
    class FetchFromWeb : JSONParse
    {
        private string _json;

        public string Json
        {
            get { return _json; }

            private set
            {
                // To check if the recieved string is indeed in JSON format.
                // TODO: Validation for value to be null...
                if (value.Trim().Substring(0, 1).IndexOfAny(new[] { '[', '{' }) != 0)
                {
                    throw new Exception(Messages.NotJson);
                }
                _json = value;
            }
        }

        public FetchFromWeb(string path)
        {
            Json = new WebClient().DownloadString(path);
        }

        public Dictionary<string, string> FetchRate()
        {
            // Additional Code, if any, comes here.
            // This is kind of my whole point of using inheritance here. 
            // This may not be required here. Please Comment. 
            // Thanks -IW
            Dictionary<string, string> items = base.GetItems(Json);

            return items;
        }
    }
}
