using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.ServiceContract;
using Tavisca.EmployeeManagement.Interface;
using Tavisca.EmployeeManagement.Translator;
using Tavisca.EmployeeManagement.EnterpriseLibrary;
using Tavisca.EmployeeManagement.DataContract;

namespace Tavisca.EmployeeManagement.ServiceImpl
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        IEmployeeManagementManager _manager;

        public EmployeeManagementService(IEmployeeManagementManager manager)
        {
            _manager = manager;
        }

        public DataContract.EmployeeResponse Create(DataContract.Employee employee)
        {
            try
            {
                EmployeeResponse employeeResponse = new EmployeeResponse();
                var result = _manager.Create(employee.ToDomainModel());
                if (result == null)
                {
                    employeeResponse.Employee = null;
                    employeeResponse.Status.StatusCode = "500";
                    employeeResponse.Status.Message = "Internal Server Error.Could not create the employee.";
                    return employeeResponse; 
                }
                employeeResponse.Employee= result.ToDataContract();
                return employeeResponse;

            }
            catch (Exception ex)
            {
                Exception newEx;
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex, out newEx);
                throw newEx;
            }
        }

        public DataContract.RemarkResponse AddRemark(string employeeId, DataContract.Remark remark)
        {
            try
            {
                 RemarkResponse remarkResponse = new RemarkResponse();
                var result = _manager.AddRemark(employeeId, remark.ToDomainModel());
                if (result == null)
                {
                    remarkResponse.Remark = null;
                    remarkResponse.Status.StatusCode = "500";
                    remarkResponse.Status.Message = "Internal Server Error.Could not add the remark.";
                    return remarkResponse;
                }
                remarkResponse.Remark= result.ToDataContract();
                return remarkResponse;
            }
            catch (Exception ex)
            {
                Exception newEx;
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex, out newEx);
                throw newEx;
            }
        }

        public DataContract.EmployeeResponse CheckCredentials(DataContract.Credentials credentials)
        {
            try
            {
                EmployeeResponse employeeResponse = new EmployeeResponse();
                var result = _manager.CheckCredentials(credentials.EmailId, credentials.Password);
                if (result == null)
                {
                    employeeResponse.Employee = null;
                    employeeResponse.Status.StatusCode = "500";
                    employeeResponse.Status.Message = "Authentication Failed.";
                    return employeeResponse;
                }

                employeeResponse.Employee = result.ToDataContract();
                return employeeResponse;
            }
            catch (Exception ex)
            {
                Exception newEx;
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex, out newEx);
                throw newEx;
            }

        }

        public string ModifyCredentials(UpdatePassword newCredentials)
        {
            int status = _manager.ModifyCredentials(newCredentials.EmailId, newCredentials.OldPassword, newCredentials.NewPassword);
           
            return status.ToString();
        }



       
    }
}
