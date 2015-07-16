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

            Response response;
            try
            {
                response = handler.Process(this._request);
                response.Status = Constants.STATUS_CODE_200;
                response.ReasonPhrase = "OK";
            }

            response.Send();
        }
    }
}
