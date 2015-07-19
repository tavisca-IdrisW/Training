using System;
using System.ServiceModel;


namespace Tavisca.EmployeeServices
{
    [ServiceContract]
    public interface ICreateEmployeeService
    {
        [OperationContract]
        [FaultContract(typeof(CustomTypeException))]
        Employee Create(Employee employee);

        [OperationContract]
        [FaultContract(typeof(CustomTypeException))]
        Remark AddRemark(string employeeId, Remark remark);
    }
}
