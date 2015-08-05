using System;
using System.ServiceModel;


namespace WCFEmployee.Services
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
