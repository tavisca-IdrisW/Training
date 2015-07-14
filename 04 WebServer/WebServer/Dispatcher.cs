using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace WebServer
{
    class Dispatcher
    {
        private Socket _clientSocket = null;
        private static FactoryHandler handlerFactory = new FactoryHandler();
        public Dispatcher(Socket clientSocket)
        {
            _clientSocket = clientSocket;
        }
        public void HandleClient()
        {
            var requestParser = new RequestParser();
            string requestString = DecodeRequest(_clientSocket);
            if (string.IsNullOrWhiteSpace(requestString) == false)
            {
                requestParser.Parser(requestString);
                Console.WriteLine(requestParser.HttpUrl);
                int dotIndex = requestParser.HttpUrl.LastIndexOf('.') + 1;
                if (dotIndex > 0)
                {

                    var requestHandler = handlerFactory.CreateHandler(requestParser.HttpUrl, _clientSocket, ConfigurationManager.AppSettings["Path"]);

                    if (requestParser.HttpMethod.Equals("get", StringComparison.InvariantCultureIgnoreCase))
                    {
                        requestHandler.DoGet(requestParser.HttpUrl);
                    }
                    else
                    {
                        Console.WriteLine("Method Unimplemented");
                        Console.ReadLine();
                    }
                }
                else
                {
                    RequestHandler htmlRequestHandler = new RequestHandler(_clientSocket, ConfigurationManager.AppSettings["Path"]);
                    htmlRequestHandler.DoGet(requestParser.HttpUrl);
                }
            }
            StopClientSocket(_clientSocket);
        }

        public void StopClientSocket(Socket clientSocket)
        {
            if (clientSocket != null)
                clientSocket.Close();
        }

        private string DecodeRequest(Socket clientSocket)
        {
            Encoding _charEncoder = Encoding.UTF8;
            var receivedBufferlen = 0;
            var buffer = new byte[10240];
            try
            {
                receivedBufferlen = clientSocket.Receive(buffer);
            }
            catch (Exception)
            {
                Console.WriteLine("Buffer full...");
                Console.ReadLine();
            }
            return _charEncoder.GetString(buffer, 0, receivedBufferlen);
        }

    }
}