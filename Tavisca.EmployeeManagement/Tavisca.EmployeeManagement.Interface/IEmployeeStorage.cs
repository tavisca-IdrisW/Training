using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Model;

namespace Tavisca.EmployeeManagement.Interface
{
    public interface IEmployeeStorage
    {
        Employee Save(Employee employee);

        Employee Get(string employeeId);

        List<Employee> GetAll();

        Remark SaveRemark(string employeeId,Remark remark);

        Employee Authenticate(string emailId, string password);

        int ChangePassword(string emailId,string oldPassword,string newPassword);

        List<Remark> GetRemarksById(string employeeId, string pageNumber);
     
        int GetRemarkCount(string employeeId);
    }
}
