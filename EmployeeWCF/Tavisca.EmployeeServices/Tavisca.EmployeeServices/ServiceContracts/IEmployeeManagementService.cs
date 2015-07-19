using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Tavisca.EmployeeServices
{
    public interface IEmployeeManagementService
    {
        [OperationContract(Name = "SearchById")]
        Employee Get(int Id);

        [OperationContract]
        [FaultContract(typeof(CustomTypeException))]
        List<Employee> GetEmployees();
    }
}
