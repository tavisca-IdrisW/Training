using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Model;

namespace Tavisca.EmployeeManagement.Interface
{
    public interface IEmployeeManager
    {
        Employee Get(string employeeId);

        List<Employee> GetAll();

        List<Remark> GetRemarksById(string employeeId, string pageNumber);

        int GetRemarkCount(string employeeId);

    }
}
