using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.DataContract;

namespace Tavisca.EmployeeManagement.ServiceContract
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [WebGet(UriTemplate = "employee/{employeeId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        EmployeeResponse Get(string employeeId);

        [WebGet(UriTemplate = "employee", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        EmployeeListResponse GetAll();

        [WebGet(UriTemplate = "remarks/{employeeId}/{pageNumber}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PaginatedRemarks GetRemarksById(string employeeId,string pageNumber);
    }
}
