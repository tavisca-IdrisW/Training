using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tavisca.Employee.DAL;

namespace Tavisca.Employee.BusinessLogic
{
    public class Remarks : IRemarkHandler
    {
        public string UtcTime { get; set; }
        public string Comment { get; set; }

        public Remarks(string comment)
        {
            Comment = comment;
        }

        public string GetUtcTime()
        {
            DateTime time = DateTime.UtcNow;
            return time.ToString();
        }

        public static void InsertRemarkIntoFile(Remarks remark, string id)
        {

            var dataJson = JsonConvert.SerializeObject(remark);
            EmployeeFileSystem file = new EmployeeFileSystem();
            Employee employee = JsonConvert.DeserializeObject<Employee>(file.GetById(id));
            employee.EmpRemarks = new Remarks(dataJson);
            dataJson = JsonConvert.SerializeObject(employee);
            file.SaveEmployee(dataJson, id);
        }

        public static Employee GetEmployeeById(string id)
        {
            EmployeeFileSystem file = new EmployeeFileSystem();

            Employee employee = JsonConvert.DeserializeObject<Employee>(file.GetById(id));

            return employee;
        }
    }
}