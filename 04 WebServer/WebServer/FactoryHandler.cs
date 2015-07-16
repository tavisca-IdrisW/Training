using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace WebServer
{
    class FactoryHandler
    {
        private string _extentions { get; set; }

        public string Extentions
        {
            get 
            { 
                return _extentions; 
            }
            set 
            {
                _extentions = value;
            }
        }
        public FactoryHandler()
        {
<<<<<<< HEAD
            Extentions = ".html, .htm, .css, .js, .txt"; 
=======
            Extentions = ""; 
>>>>>>> 5af2b98d8de060aa9c31655d426b48e7d0b3043a
        }

        public IProcesses CreateHandler(string url, Socket clientSocket, string path)
        {
            IProcesses requesterProcess = null;
            string urlExtention = GetExtensionFromUrl(url);

            if (Extentions.Contains(urlExtention))
            {
<<<<<<< HEAD
                requesterProcess = new RequestHandler(clientSocket, path);
=======
                requesterProcess = new Request(clientSocket);
>>>>>>> 5af2b98d8de060aa9c31655d426b48e7d0b3043a
            }
            else
            {
                requesterProcess = new ErrorHandler(clientSocket, 415);
            }
            return requesterProcess;
        }

        private string GetExtensionFromUrl(string url)
        {
            return url.Substring(url.LastIndexOf('.'));
        }
    }
}