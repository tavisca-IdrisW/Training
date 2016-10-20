using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Interface;
using Tavisca.EmployeeManagement.Model;

namespace Tavisca.EmployeeManagement.BusinessLogic
{
    public class EmployeeManagementManager : IEmployeeManagementManager
    {
        public EmployeeManagementManager(IEmployeeStorage storage)
        {
            _storage = storage;
        }

        IEmployeeStorage _storage;

        public Employee Create(Employee employee)
        {
            employee.JoiningDate = DateTime.UtcNow;
            employee.Validate();
            return _storage.Save(employee);
        }

        public Remark AddRemark(string employeeId, Remark remark)
        {
        ////    remark.Validate();
        //    //var employee = _storage.Get(employeeId);
        //    //if(employee.Remarks == null) employee.Remarks = new List<Remark>();
        //    //remark.CreateTimeStamp = DateTime.UtcNow;
        //    //employee.Remarks.Add(remark);
        //    //_storage.Save(employee);
        //    var employee = _storage.Get(employeeId);
        //    if (employee == null) 
        //    {
        //        return null;
        //    } 

        //    remark.CreateTimeStamp = DateTime.UtcNow;
        //    _storage.SaveRemark(employeeId,remark);
        //    return remark;


            remark.Validate();

            remark.CreateTimeStamp = DateTime.UtcNow;

            _storage.SaveRemark(employeeId, remark);
            return remark;
        }

        public Employee CheckCredentials(string emailId, string password)
        {
            var employee = _storage.Authenticate(emailId,password);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }


        public int ModifyCredentials(string emailId, string oldPassword, string newPassword)
        {
            int success = _storage.ChangePassword(emailId, oldPassword,newPassword);
            return success;
        }
    }
}
