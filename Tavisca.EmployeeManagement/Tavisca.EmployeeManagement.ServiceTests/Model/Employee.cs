using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.ServiceTests
{
    [DataContract]
    internal class Employee
    {
        internal Employee()
        {
            JoiningDate = DateTime.UtcNow;
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }
        
        [DataMember]
        public DateTime JoiningDate { get; set; }

        [DataMember]
        public List<Remark> Remarks { get; set; }
    }
}
