using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace WebServer
{
    class Request : IProcesses
    {
        private Dictionary<string, string> _headers { get; set; }
        RegistryKey registryKey = Registry.ClassesRoot;
        public Socket ClientSocket = null;
        private Encoding _charEncoder = Encoding.UTF8;
        private string _filePath = ConfigurationManager.AppSettings["path"];
        public FileHandler FHandle;
        public Uri Uri { get; private set; }

        public Request(Socket clientSocket)
        {
            ClientSocket = clientSocket;
            FHandle = new FileHandler(_filePath);
            var request = Encoding.UTF8.GetString(ReadBytes());
            
            if (string.IsNullOrEmpty(request))
            {
                return;
            }
            _headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            string[] lineSplit = request.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string[] requestLine = lineSplit[0].Split(' ');
        }

        private byte[] ReadBytes()
        {
            var bucket = new byte[1024];
            using (var buffer = new MemoryStream())
            {
                while (true)
                {
                    var bytesRead = ClientSocket.Receive(bucket);
                    if (bytesRead > 0)
                        buffer.Write(bucket, 0, bytesRead);

                    if (ClientSocket.Available == 0)
                        break;
                }
                return buffer.ToArray();
            }
        }

        public void DoGet(string requestedFile)
        {
            if (FHandle.FileExists(requestedFile))
            {
                SendResponse(ClientSocket, FHandle.ReadFile(requestedFile), "200 Ok", GetFileType(registryKey, (_filePath + requestedFile)));
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


        private void SendErrorResponce(Socket clientSocket)
        {
            byte[] noData = new byte[0];
            SendResponse(clientSocket, noData, "404 Not Found", "text/html");
        }


        private void SendResponse(Socket socket, byte[] body, string respCode, string contentType)
        {
            try
            {
                byte[] header = CreateHeader(respCode, body.Length, contentType);
                socket.Send(header);
                socket.Send(body);
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
