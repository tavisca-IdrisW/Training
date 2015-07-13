using System;
using Microsoft.Win32;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    class CreateResponse
    {

        RegistryKey registryKey = Registry.ClassesRoot;
        public Socket ClientSocket = null;
        private Encoding _charEncoder = Encoding.UTF8;
        private string _contentPath;
        public FileHandler FileHandler;

        public CreateResponse(Socket clientSocket, string contentPath)
        {
            _contentPath = contentPath;
            ClientSocket = clientSocket;
            FileHandler = new FileHandler(_contentPath);
        }

        public void RequestUrl(string requestedFile)             //This is my DoGet method
        {
            int dotIndex = requestedFile.LastIndexOf('.') + 1;
            if (dotIndex > 0)
            {
                if (FileHandler.FileExists(requestedFile))    //If yes check existence of the file
                    SendResponse(ClientSocket, FileHandler.ReadFile(requestedFile), "200 Ok", GetTypeOfFile(registryKey, (_contentPath + requestedFile)));
                else
                    SendErrorResponce(ClientSocket);      // We don't support this extension.
            }
            else   //find default file as index .htm of index.html
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


        private void SendResponse(Socket clientSocket, byte[] byteContent, string responseCode, string contentType)
        {
            try
            {
                byte[] byteHeader = CreateHeader(responseCode, byteContent.Length, contentType);
                clientSocket.Send(byteHeader);
                clientSocket.Send(byteContent);
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