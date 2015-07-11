using System;
using System.Net.Sockets;

namespace DemoWebServer
{
    class Response
    {
        private byte[] _data;
        private Response(byte[] _data)
        {
 
        }

        public static Response From(Request req)
        {
            return new Response();
        }

        public void Post(NetworkStream stream)
        {
            stream.Write(_data, 0);
        }
    }
}
