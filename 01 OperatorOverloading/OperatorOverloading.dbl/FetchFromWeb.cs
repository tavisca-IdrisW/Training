using System;
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

        public double FetchRate(string searchString)
        {
            // This is method is defined to provede access to only those classes that
            // that need or are getting the json. Otherwise there are chances taht the json can get changed somehow.
            // This may not be required here. Please Comment. 
            // Thanks -IW

            return base.FetchRate(Json, searchString);
        }
    }
}
