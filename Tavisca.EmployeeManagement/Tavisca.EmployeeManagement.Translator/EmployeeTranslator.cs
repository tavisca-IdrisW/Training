using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.Translator
{
    public static class EmployeeTranslator
    {
        public static Model.Employee ToDomainModel(this DataContract.Employee employee)
        {
            if (employee == null) return null;
            return new Model.Employee()
            {
                Id = employee.Id,
                Title = employee.Title,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                Phone = employee.Phone,
                Remarks = employee.Remarks == null ? null : employee.Remarks.Select(remark => remark.ToDomainModel()).ToList()
               
            };
        }

        public static DataContract.Employee ToDataContract(this Model.Employee employee)
        {
            if (employee == null) return null;
            return new DataContract.Employee()
            {
                Id = employee.Id,
                Title = employee.Title,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                Phone = employee.Phone,
                Remarks = employee.Remarks == null ? null : employee.Remarks.Select(remark => remark.ToDataContract()).ToList()
            };
        }
    }
}
