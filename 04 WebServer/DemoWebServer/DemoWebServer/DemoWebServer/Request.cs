using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebServer
{
    public class Request
    {
        public string Type { get; set; }
        public string URL { get; set; }
        public string Host { get; set; }

        private Request(string type, string url, string host)
        {
            Type = type;
            this.URL = url;
            Host = host;
        }

        public static Request FetchRequest(string request)
        {
            if (String.IsNullOrEmpty(request))
                //return null;
                throw new Exception("RequestCannotBeEmpty!!!");

            string[] tokens = request.Split(' ');
            string type = tokens[0];
            string url = tokens[1];
            string host = tokens[4];


            return new Request(type, url, host);
        }
    }
}
