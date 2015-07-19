using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tavisca.EmployeeServices
{

    [DataContract]
    public class Employee
    {
        [DataMember]
        public int EmpId { get; set; }
        [DataMember]
        public string EmpFirst { get; set; }
        [DataMember]
        public string EmpLast { get; set; }
        [DataMember]
        public string EmpEmail { get; set; }
        [DataMember]
        public string EmpDesig { get; set; }
        [DataMember]
        public Remark EmpRemarks { get; set; }
    }


    [DataContract]
    public class Remark
    {
        [DataMember]
        public DateTime SubTime { get; set; }
        [DataMember]
        public string Comments { get; set; }

    }
}
