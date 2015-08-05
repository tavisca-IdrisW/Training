using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Tavisca.EmployeeManagement.DataContract
{
    [DataContract]
    public class PaginatedRemarks:Result
    {
        [DataMember]
        public List<Remark> Remarks { get; set;}
         [DataMember]
        public int TotalCount { get; set; }
    }
}
