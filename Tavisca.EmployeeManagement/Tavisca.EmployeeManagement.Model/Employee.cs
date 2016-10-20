using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.Model
{   
    public class Employee
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

       public DateTime JoiningDate  { get; set; }

        public List<Remark> Remarks { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.FirstName))
                throw new Exception("FirstName cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(this.Email))
                throw new Exception("Email cannot be null or empty.");

       //     if (this.JoiningDate == DateTime.MinValue || this.JoiningDate == DateTime.MinValue)
         //       throw new Exception("Invalid joining date provided.");
        }
    }
}
