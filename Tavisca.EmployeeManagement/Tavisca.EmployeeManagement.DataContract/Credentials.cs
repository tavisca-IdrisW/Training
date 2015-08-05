using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Tavisca.EmployeeManagement.DataContract
{
    [DataContract]
    public class Credentials
    {
        [DataMember]
        public string EmailId { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
