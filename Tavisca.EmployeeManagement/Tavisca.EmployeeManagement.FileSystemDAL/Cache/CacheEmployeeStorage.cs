using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Interface;
using Newtonsoft.Json;
using Tavisca.EmployeeManagement.ErrorSpace;
using System.Data.SqlClient;
using Tavisca.EmployeeManagement.EnterpriseLibrary;

namespace Tavisca.EmployeeManagement.FileStorage
{
    public class CacheEmployeeStorage : IEmployeeStorage
    {
        public CacheEmployeeStorage(ICacheManager manager)
        {
            _innerStorage = new EmployeeStorage();
            _cacheManager = manager;
        }

        IEmployeeStorage _innerStorage;
        ICacheManager _cacheManager;

        readonly string KEYFORMAT = "emp.{0}";
        readonly string CACHEMANAGER = "employee";

        public Model.Employee Save(Model.Employee employee)
        {
            var result = _innerStorage.Save(employee);
         //   _cacheManager.Add(string.Format(KEYFORMAT, result.Id), result, CACHEMANAGER);
            return result;
        }

        public Model.Employee Get(string employeeId)
        {
            Model.Employee result;
         //   result = _cacheManager.Get(string.Format(KEYFORMAT, employeeId), CACHEMANAGER) as Model.Employee;
           // if (result == null)
            {
                result = _innerStorage.Get(employeeId);
            //    _cacheManager.Add(string.Format(KEYFORMAT, employeeId), result, CACHEMANAGER);
            }
            return result;
        }

        public List<Model.Employee> GetAll()
        {
            return _innerStorage.GetAll();
        }

        // <<<<<<<<<<<<Dummy code>>>>>>>>>>>>>
        public Model.Remark SaveRemark(string employeeId, Model.Remark remark)
        {
            var result = _innerStorage.SaveRemark(employeeId,remark);
         //   _cacheManager.Add(string.Format(KEYFORMAT, result.Text), result, CACHEMANAGER);
            return result;
        }


        public Model.Employee Authenticate(string emailId, string password)
        {
            var result = _innerStorage.Authenticate(emailId, password);
            return result; 
        }


        public int ChangePassword(string emailId, string oldPassword, string newPassword)
        {
            int result = _innerStorage.ChangePassword(emailId, oldPassword,newPassword);
            return result;
        }


        public List<Model.Remark> GetRemarksById(string employeeId, string pageNumber)
        {
            var result = _innerStorage.GetRemarksById(employeeId, pageNumber);
            return result;
        }


        public int GetRemarkCount(string employeeId)
        {
            var result = _innerStorage.GetRemarkCount(employeeId);
            return result;
        }
    }
}
