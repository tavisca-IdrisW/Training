using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
namespace Tavisca.Employee.DAL
{
    public class EmployeeFileSystem
    {
        public void SaveEmployee(string empData, string id)
        {
            if (File.Exists(@"C:\Employees\ID.txt"))
            {
                File.AppendAllText(@"C:\Employees\ID.txt", id + Environment.NewLine);
            }
            else
            {
                File.WriteAllText(@"C:\Employees\ID.txt", id + Environment.NewLine);
            }

            File.WriteAllText(@"C:\EmployeeDetails\" + id + ".txt", empData);
        }
        public string GetById(String id)
        {
            var employeeData = File.ReadAllText(@"C:\EmployeeDetails\" + id + ".txt");
            return employeeData;
        }

        public List<string> GetIdList()
        {
            string line = "";
            List<string> empIds = new List<string>();
            var file = new StreamReader(@"C:\Employees\ID.txt");

            while ((line = file.ReadLine()) != null)
            {
                empIds.Add(line);
            }

            return empIds;

        }
    }
}