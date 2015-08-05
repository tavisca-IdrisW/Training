using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Configuration;

namespace Tavisca.EmployeeManagement.ServiceTests
{
    [TestClass]
    public class EmployeeManagementTestFixture
    {
        public TestContext TestContext { get; set; }

        private string EmployeeServiceUrl = ConfigurationManager.AppSettings["employeeserviceurl"];
        private string EmployeeManagementServiceUrl = ConfigurationManager.AppSettings["employeemanagementserviceurl"];

        [TestMethod]
        [DataSource("Employee-Create")]
        public void CreateEmployeeTest()
        {
            if (!IsEnabled(TestContext)) return;
            // Crate employee 
            var client = new HttpClient();
            var createdEmployee = client.UploadData<Employee, Employee>(EmployeeManagementServiceUrl + "employee", DataAdapter.GetEmployee(TestContext));
            Assert.IsNotNull(createdEmployee);
            Assert.IsTrue(string.IsNullOrWhiteSpace(createdEmployee.Id) == false);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void CreateInvalidEmployeeTest()
        {
            var employee = new Employee();
            var client = new HttpClient();
            var createdEmployee = client.UploadData<Employee, Employee>(EmployeeManagementServiceUrl + "employee", employee);
        }

        [TestMethod]
        public void AddRemarkToEmployeeTest()
        {
            // add remark to employee 
            var employee = new Employee() { FirstName = "Jane", LastName = "Doe", Email = "jane@doe.com", JoiningDate = DateTime.UtcNow }; 
            var client = new HttpClient();
            var createdEmployee = client.UploadData<Employee, Employee>(EmployeeManagementServiceUrl + "employee", employee);
            Assert.IsNotNull(createdEmployee);
            Assert.IsTrue(string.IsNullOrWhiteSpace(createdEmployee.Id) == false);

            var remark = new Remark() { Text = "Remark1" , CreateTimeStamp = DateTime.UtcNow};
            var createdRemark = client.UploadData<Remark, Remark>(EmployeeManagementServiceUrl + "employee/"+ createdEmployee.Id + "/remark", remark);
            Assert.IsNotNull(createdRemark);
            Assert.IsTrue(createdRemark.CreateTimeStamp > DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void AddRemarkToNonExistingEmployeeTest()
        {
            var client = new HttpClient();
            var remark = new Remark() { Text = "Remark1" , CreateTimeStamp = DateTime.UtcNow};
            var createdRemark = client.UploadData<Remark, Remark>(EmployeeManagementServiceUrl + "employee/testid/remark", remark);
            
        }

        [TestMethod]
        public void GetEmployeeByIdTest()
        {
            // add remark to employee 
            var employee = new Employee() { FirstName = "Jane", LastName = "Doe", Email = "jane@doe.com", JoiningDate = DateTime.UtcNow };
            var client = new HttpClient();
            var createdEmployee = client.UploadData<Employee, Employee>(EmployeeManagementServiceUrl + "employee", employee);
            Assert.IsNotNull(createdEmployee);
            Assert.IsTrue(string.IsNullOrWhiteSpace(createdEmployee.Id) == false);

            var getEmployee = client.GetData<Employee>(EmployeeServiceUrl + "employee/" + createdEmployee.Id);
            Assert.IsNotNull(getEmployee);
            Assert.IsTrue(string.IsNullOrWhiteSpace(getEmployee.Id) == false);
            Assert.IsTrue(createdEmployee.Id == getEmployee.Id);
        }

        private static bool IsEnabled(TestContext context)
        {
            var isEnabled = context.DataRow["Enabled"].ToString();
            if (string.IsNullOrWhiteSpace(isEnabled)) return false;
            return "|true|y|yes|1|on|t|".IndexOf(isEnabled, StringComparison.InvariantCultureIgnoreCase) != -1;
        }
    }
}
