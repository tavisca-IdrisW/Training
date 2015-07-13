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
            Extentions = ".html, .htm, .css, .js, .txt"; 
        }

        public IProcesses CreateHandler(string url, Socket clientSocket, string path)
        {
            IProcesses requesterProcess = null;
            string urlExtention = GetExtensionFromUrl(url);

            if (Extentions.Contains(urlExtention))
            {
                requesterProcess = new RequestHandler(clientSocket, path);
            }
            else
            {
                requesterProcess = new ErrorHandler(clientSocket);
            }
            return requesterProcess;
        }

        private string GetExtensionFromUrl(string url)
        {
            return url.Substring(url.LastIndexOf('.'));
        }
    }
}