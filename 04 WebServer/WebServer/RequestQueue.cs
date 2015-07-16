using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace WebServer
{
    class RequestQueue
    {
        private static ConcurrentQueue<Socket> _reqQueue = new ConcurrentQueue<Socket>();

        public static bool TryDequeue(out Socket socket)
        {
            return _reqQueue.TryDequeue(out socket);
        }

        public static void Enqueue(Socket socket)
        {
            _reqQueue.Enqueue(socket);
        }
    }
}
