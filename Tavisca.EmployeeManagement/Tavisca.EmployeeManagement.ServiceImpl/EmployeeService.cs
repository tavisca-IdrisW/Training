using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.DataContract;
using Tavisca.EmployeeManagement.EnterpriseLibrary;
using Tavisca.EmployeeManagement.Interface;
using Tavisca.EmployeeManagement.ServiceContract;
using Tavisca.EmployeeManagement.Translator;

namespace Tavisca.EmployeeManagement.ServiceImpl
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IEmployeeManager manager)
        {
            _manager = manager;
        }

        IEmployeeManager _manager;

        public DataContract.EmployeeResponse Get(string employeeId)
        {
            EmployeeResponse employeeResponse = new EmployeeResponse();
            try
            {
                var result = _manager.Get(employeeId);
                    if (result == null)
                    {
                        employeeResponse.Employee = null;
                        employeeResponse.Status.StatusCode = "500";
                        employeeResponse.Status.Message = "Internal Server Error.Could not fetch the employee record.";
                        return employeeResponse;
                    }

                employeeResponse.Employee = result.ToDataContract();
                return employeeResponse;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex);
                if (rethrow) throw;
                employeeResponse.Employee = null;
                employeeResponse.Status.StatusCode = "500";
                employeeResponse.Status.Message = "Internal Server Error.Could not fetch the employee record.";
                return employeeResponse;
            }
        }

        public EmployeeListResponse GetAll()
        {
            EmployeeListResponse employeeListResponse = new EmployeeListResponse();
            try
            {
                var result = _manager.GetAll();
                if (result == null)
                {
                    employeeListResponse.EmployeeList = null;
                    employeeListResponse.Status.StatusCode = "500";
                    employeeListResponse.Status.Message = "Internal Server Error.Could not fetch the employee record.";
                    return employeeListResponse;
                }
                employeeListResponse.EmployeeList= result.Select(employee => employee.ToDataContract()).ToList();
                return employeeListResponse;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex);
                if (rethrow) throw;
                employeeListResponse.EmployeeList = null;
                employeeListResponse.Status.StatusCode = "500";
                employeeListResponse.Status.Message = "Internal Server Error.Could not fetch the employee record.";
                return employeeListResponse;
            }

        }


        public PaginatedRemarks GetRemarksById(string employeeId, string pageNumber)
        {
            PaginatedRemarks pagenatedRemarks = new PaginatedRemarks();
            try
            {
                var result = _manager.GetRemarksById(employeeId,pageNumber);
                int count = _manager.GetRemarkCount(employeeId);
                if (result == null||count==-1)
                {
                    pagenatedRemarks.Remarks = null;
                    pagenatedRemarks.TotalCount = 0;
                    pagenatedRemarks.Status.StatusCode = "500";
                    pagenatedRemarks.Status.Message = "Internal Server Error.Could not fetch the remark records.";
                    return pagenatedRemarks;
                }
                pagenatedRemarks.Remarks = result.Select(remark => remark.ToDataContract()).ToList();
                pagenatedRemarks.TotalCount = count;
                return pagenatedRemarks;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex);
                if (rethrow) throw;
                pagenatedRemarks.Remarks = null;
                pagenatedRemarks.TotalCount = 0;
                pagenatedRemarks.Status.StatusCode = "500";
                pagenatedRemarks.Status.Message = "Internal Server Error.Could not fetch the remark records.";
                return pagenatedRemarks;
            }
        }
    }
}
