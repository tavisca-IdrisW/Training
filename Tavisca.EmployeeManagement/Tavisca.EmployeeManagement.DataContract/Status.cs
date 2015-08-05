using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.DataContract
{
    [DataContract]
    public class ResponseSatus
    {
        [DataMember]
        public String StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        public ResponseSatus() { }
        public ResponseSatus(string statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }
    }
}
