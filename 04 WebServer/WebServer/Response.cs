using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public class Response
    {

        public Response(IProcesses request)
        {
            if (request == null) throw new ArgumentException();

            this.Body = new byte[] { };
            Request = (Request)request;
        }

        private string _contentType = "text/html";

        public Request Request { get; private set; }
        public int Status { get; set; }
        public string ReasonPhrase { get; set; }
        public string ContentType
        {
            get { return _contentType; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                _contentType = value;
            }
        }

        public string Date { get { return DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss 'GMT'"); } }
        public int ContentLength { get { return this.Body != null ? this.Body.Length : 0; } }
        public byte[] Body { get; set; }
        public void Send()
        {
            try
            {
                byte[] bytes = GetBytes(BuildResponse());
                var socket = Request.ClientSocket;
                socket.Send(bytes);
                socket.Close();
                socket.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private string BuildResponse()
        {
            StringBuilder httpResponse = new StringBuilder();

            httpResponse.Append(this.Status).Append(" ");
            httpResponse.Append(this.ReasonPhrase).Append(" ");
            httpResponse.Append("\r\n");

            httpResponse.Append(this.Date);
            httpResponse.Append("\r\n");
            httpResponse.Append("Content-Type:").Append(this.ContentType);
            httpResponse.Append("\r\n").Append("\r\n");
            return httpResponse.ToString();
        }
        private byte[] GetBytes(string response)
        {
            var bytes = Encoding.UTF8.GetBytes(response);
            var stream = new MemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            if (this.Body != null)
                stream.Write(this.Body, 0, this.Body.Length);
            return stream.ToArray();
        }
    }
}