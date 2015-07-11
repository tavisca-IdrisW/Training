using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace DemoWebServer
{
    public class HTTPServer
    {
        public const string Version = "HTTP/1.1";
        public const string Name = "Demo HTTP Server";

        private bool _isRunning = false;
        private TcpListener _listener;

        public HTTPServer(int portNum)
        {
            _listener = new TcpListener(IPAddress.Any, portNum);
        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
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
                message += reader.ReadLine() + "\n";
            }

            Debug.WriteLine("Request: \n" + message);

            Request req = Request.FetchRequest(message);
            Response res = Response.From(req);
        }
    }

}
