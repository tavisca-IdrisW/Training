using System;
using System.Text;

namespace WebServer
{
    class RequestParser
    {
        private Encoding _charEncoder = Encoding.UTF8;
        public string HttpMethod;
        public string HttpUrl;
        public string HttpProtocolVersion;


        public void Parser(string requestString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(requestString))
                {
                    return;
                }
                string[] tokens = requestString.Split(' ');

                tokens[1] = tokens[1].Replace("/", "\\");
                HttpMethod = tokens[0].ToUpper();
                HttpUrl = tokens[1];
                HttpProtocolVersion = tokens[2];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                Console.WriteLine("Bad Request");
            }
        }
    }
}
