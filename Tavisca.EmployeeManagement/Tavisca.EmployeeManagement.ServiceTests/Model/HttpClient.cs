using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.ServiceTests
{
    public class HttpClient
    {
        public T GetData<T>(string url, string contentType = null)
        {
            var client = new WebClient();
            client.Headers.Add("Content-Type", contentType ?? "application/json");
            var response = client.DownloadString(url);
            return Serializer.Deserialize<T>(response);
        }

        public T2 UploadData<T1,T2>(string url, T1 data, string contentType = "application/json", string method = "POST")
        {
            var client = new WebClient();
            client.Headers.Add("Content-Type", contentType);
            var dataToBeUploaded = Serializer.Serialize<T1>(data);
            var response = client.UploadString(url, method, dataToBeUploaded);
            return Serializer.Deserialize<T2>(response);
        }
    }
}
