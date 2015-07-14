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
        private int _errorCode { get; set; }

        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }

            set
            {
                _errorCode = value;
            }
        }

        public ErrorHandler(Socket clientSocket, int errorCode)
        {
            _clientSocket = clientSocket;
            ErrorCode = errorCode;
        }

        private void SendResponse(Socket clientSocket, byte[] body, string responseCode, string contentType)
        {
            try
            {
                byte[] header = CreateHeader(responseCode, body.Length, contentType);
                clientSocket.Send(header);
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
            switch (ErrorCode) {
                case 500:
                    SendResponse(_clientSocket, noData, "500 Internal server error", "text/html");
                    break;
                case 415:
                    SendResponse(_clientSocket, noData, "415 Media Unsupported.", "text/html");
                    break;
                default:
                    SendResponse(_clientSocket, noData, "520 Unknown Error.", "text/html");
                    break;
            }
        }

        public void DoPost()
        {
            throw new Exception(" DoPost Not Implemented for Error.");
        }
    }
}