using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace DemoServer
{
    public class HTTPServer
    {
        private bool _isRunning = false;
        private TcpListener _listener;

        public HTTPServer(int portNum)
        {
            _listener = new TcpListener(IPAddress.Any, portNum);
        }

        public void Run()
        {
            _isRunning = true;
            _listener.Start();

            while (_isRunning)
            {
                Console.WriteLine("Waiting for connection....");

                TcpClient client = _listener.AcceptTcpClient();

                Console.WriteLine("Client Connected!!!");

                HandleClient(client);
                client.Close();
            }

            _isRunning = false;
            _listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            string message = "";

            while (reader.Peek() != -1)
            {
                message += Console.ReadLine() + "\n";
            }

            Console.WriteLine("Request: \n" + message);
        }
    }
}
