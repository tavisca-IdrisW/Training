using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.ServiceTests
{
    internal static class DataAdapter
    {
        internal static Employee GetEmployee(TestContext testContext)
        {
            var employee = new Employee();
            employee.Title = testContext.DataRow["Title"].ToString();
            employee.FirstName = testContext.DataRow["FirstName"].ToString();
            employee.LastName = testContext.DataRow["LastName"].ToString();
            employee.Email = testContext.DataRow["Email"].ToString();
            employee.Phone = testContext.DataRow["Phone"].ToString();
            employee.JoiningDate = DateTime.Parse(testContext.DataRow["JoiningDate"].ToString());

            return employee;
        }

        internal static Remark GetRemark(TestContext testContext)
        {
            var remark = new Remark();
            remark.Text = testContext.DataRow["Text"].ToString();
            return remark;
        }
    }
}
