using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServer
{
    internal class Worker
    {
        private IProcesses _request;

        public Worker(IProcesses request)
        {
            if (request == null) throw new ArgumentException();

            this._request = request;
        }

        public void Process(IHandler handler)
        {
            if (handler == null) throw new ArgumentException();

            try
            {
                Response response;
                response = handler.Process((Request)_request);
                response.Status = 200;
                response.ReasonPhrase = "OK";
                response.Send();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
