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


        private TcpListener _listener;
        private bool _isRunning = false;

        public Listener(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public void Run()
        {
            _isRunning = true;
            _listener.Start();
            while (_isRunning)
            {
                Socket socket = _listener.AcceptSocket();
                RequestQueue.Enqueue(socket);
            }

        }

        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();            
        }
    }
}
