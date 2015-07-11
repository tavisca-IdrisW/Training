using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoServer
{
    public class Request
    {
        public string Type { get; set; }
        public string URL { get; set; }
        public string Host { get; set; }

        private Request(string type, string url, string host)
        {

        }

        public static Request FetchRequest(string request)
        {
            if (String.IsNullOrWhiteSpace(request))
            {
                throw new Exception("EmptyRequest!!"); 
            }

            return new Request("", "", "");
        }
    }
}
