using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tavisca.Employee.DAL;

namespace Tavisca.Employee.BusinessLogic
{
    public class Employee : IEmployeeHandler
    {
        public string EmpId { get; set; }

        public string EmpDesig { get; set; }

        public string EmpFirst { get; set; }

        public string EmpLast { get; set; }

        public string EmpEmail { get; set; }

        public Remarks EmpRemarks { get; set; }

        public Employee(string desg, string firstName, string lastName, string email)
        {
            EmpId = "";
            EmpDesig = desg;
            EmpFirst = firstName;
            EmpLast = lastName;
            EmpEmail = email;
            EmpRemarks = new Remarks(" ");
        }

        public string GenerateId()
        {
            string id = string.Format(@"{0}", Guid.NewGuid());
            return id;
        }

        public static void SaveToFile(Employee employee)
        {
            var dataJson = JsonConvert.SerializeObject(employee);

            EmployeeFileSystem file = new EmployeeFileSystem();

            file.SaveEmployee(dataJson, employee.EmpId);
        }

        public static Employee GetEmployeeById(string id)
        {
            EmployeeFileSystem file = new EmployeeFileSystem();

            Employee employee = JsonConvert.DeserializeObject<Employee>(file.GetById(id));

            return employee;
        }

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            EmployeeFileSystem file = new EmployeeFileSystem();

            List<string> empId = file.GetIdList();

            for (int index = 0; index < empId.Count - 1; index++)
            {
                employeeList.Add(GetEmployeeById(empId[index]));
            }
            
            return employeeList;
        }

    }
}