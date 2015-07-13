using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace WebServer
{
    class RequestHandler : IProcesses
    {
        RegistryKey registryKey = Registry.ClassesRoot;
        public Socket ClientSocket = null;
        private Encoding _charEncoder = Encoding.UTF8;
        private string _filePath { get; set; }
        public FileHandler FHandle;

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

        public RequestHandler(Socket clientSocket, string path)
        {
            FilePath = path;
            ClientSocket = clientSocket;
            FHandle = new FileHandler(FilePath);
        }
        public void DoGet(string requestedFile)
        {
            if (FHandle.FileExists(requestedFile))
            {
                SendResponse(ClientSocket, FHandle.ReadFile(requestedFile), "200 Ok", GetFileType(registryKey, (FilePath + requestedFile)));
            }
            else
            {
                SendErrorResponce(ClientSocket); 
            }
        }

        public void DoPost()
        {
            throw new Exception("DoPost Not Implemented for Text type.");
        }

        private string GetFileType(RegistryKey registryKey, string fileName)
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
            byte[] noData = new byte[0];
            SendResponse(clientSocket, noData, "404 Not Found", "text/html");
        }


        private void SendResponse(Socket clientSocket, byte[] body, string respCode, string contentType)
        {
            try
            {
                byte[] header = CreateHeader(respCode, body.Length, contentType);
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
