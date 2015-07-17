using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCFEmployee.Services
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
