using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Tavisca.EmployeeManagement.ServiceTests
{
    internal static class Serializer
    {
        internal static string Serialize<T>(T data)
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(ms, data);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }

        internal static T Deserialize<T>(string data)
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                using (StreamWriter writer = new StreamWriter(ms))
                {
                    writer.Write(data);
                    writer.Flush();

                    ms.Position = 0;
                    return (T)serializer.ReadObject(ms);
                }
            }
        }
    }
}
