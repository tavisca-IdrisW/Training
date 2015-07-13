using System;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    class ErrorHandler : IProcesses
    {
        RegistryKey registryKey = Registry.ClassesRoot;
        private Socket _clientSocket = null;
        private Encoding _charEncoder = Encoding.UTF8;

        public ErrorHandler(Socket clientSocket)
        {
            _clientSocket = clientSocket;
        }

        private void SendResponse(Socket clientSocket, byte[] body, string responseCode, string contentType)
        {
            try
            {
                byte[] byteHeader = CreateHeader(responseCode, body.Length, contentType);
                clientSocket.Send(byteHeader);
                clientSocket.Send(body);
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private byte[] CreateHeader(string responseCode, int contentLength, string contentType)
        {
            return _charEncoder.GetBytes("HTTP/1.1 " + responseCode + "\r\n"
                                  + "Server: Simple Web Server\r\n"
                                  + "Content-Length: " + contentLength + "\r\n"
                                  + "Connection: close\r\n"
                                  + "Content-Type: " + contentType + "\r\n\r\n");
        }

        public void DoGet(string uri)
        {
            byte[] noData = new byte[0];
            SendResponse(_clientSocket, noData, "500 Internal server error", "text/html");
        }

        public void DoPost()
        {
            throw new Exception(" DoPost Not Implemented for Error.");
        }
    }
}