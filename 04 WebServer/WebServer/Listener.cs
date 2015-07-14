using System;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;


namespace WebServer
{
    class Listener
    {

        private TcpListener _listener {get; set;}
        private bool _isRunning = false;

        public Listener(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public void Run()
        {
            _isRunning = true;
            _listener.Start();
            Console.WriteLine(" ... and we're up ...");
            while (_isRunning)
            {
                if (_listener.Pending())
                {

                    Socket clientSocket = _listener.AcceptSocket();
                    Dispatcher dispatcher = new Dispatcher(clientSocket);
                    Thread dispatcherThread = new Thread(new ThreadStart(dispatcher.HandleClient));
                    dispatcherThread.Start();
                }
            }
            _isRunning = false;
            _listener.Stop();
        }

    }
}
