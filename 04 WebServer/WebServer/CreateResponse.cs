using System;
using Microsoft.Win32;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    class Response
    {

        RegistryKey registryKey = Registry.ClassesRoot;
        public Socket ClientSocket = null;
        private Encoding _charEncoder = Encoding.UTF8;
        public FileHandler FileHandler;

        private string _filePath { get; set; }

        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                _filePath = value;
            }
        }

        public Response(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }

        public void RequestUrl(string requestedFile)
        {
            int dotIndex = requestedFile.LastIndexOf('.') + 1;
            if (dotIndex > 0)
            {
                if (FileHandler.FileExists(requestedFile))
                    SendResponse(ClientSocket, FileHandler.ReadFile(requestedFile), "200 Ok", GetTypeOfFile(registryKey, (FilePath + requestedFile)));
                else
                    SendErrorResponce(ClientSocket);
            }
            else
            {
                if (FileHandler.FileExists("\\index.htm"))
                    SendResponse(ClientSocket, FileHandler.ReadFile("\\index.htm"), "200 Ok", "text/html");
                else if (FileHandler.FileExists("\\index.html"))
                    SendResponse(ClientSocket, FileHandler.ReadFile("\\index.html"), "200 Ok", "text/html");
                else
                    SendErrorResponce(ClientSocket);
            }
        }

        private string GetTypeOfFile(RegistryKey registryKey, string fileName)
        {
            RegistryKey fileClass = registryKey.OpenSubKey(Path.GetExtension(fileName));
            string type = "";
            try
            {
                type = fileClass.GetValue("Content Type").ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return type;
        }


        private void SendErrorResponce(Socket clientSocket)
        {
            byte[] emptyByteArray = new byte[0];
            SendResponse(clientSocket, emptyByteArray, "404 Not Found", "text/html");
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

    }
}