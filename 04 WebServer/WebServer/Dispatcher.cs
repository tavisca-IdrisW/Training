using System;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.Text;

namespace WebServer
{
    class Dispatcher
    {
        private Socket _clientSocket = null; // use a queue later on

        public Dispatcher(Socket clientSocket)
        {
            _clientSocket = clientSocket;
        }
        public void HandleClient()
        {
            var requestParser = new RequestParser();
            string requestString = DecodeRequest(_clientSocket);
            requestParser.Parser(requestString);

            Console.WriteLine(requestParser.HttpUrl);

            if (requestParser.HttpMethod.Equals("get", StringComparison.InvariantCultureIgnoreCase))
            {
                //TO DO: Select appropriate class for serving the received GET request.....
                var createResponse = new CreateResponse(_clientSocket, ConfigurationManager.AppSettings["Path"]);
                createResponse.RequestUrl(requestParser.HttpUrl);
            }
            else
            {
                Console.WriteLine("unimplemented methode");
                Console.ReadLine();
            }
            StopClientSocket(_clientSocket);  //closes the connection
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
                //Console.WriteLine("buffer full");
                Console.ReadLine();
            }
            return _charEncoder.GetString(buffer, 0, receivedBufferlen);
        }



    }
}
