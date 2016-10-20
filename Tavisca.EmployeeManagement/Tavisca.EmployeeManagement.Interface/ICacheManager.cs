using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.Interface
{
    public interface ICacheManager
    {
        void Flush(string managerName);

        object Get(string key, string managerName);

        void Add(string key, object value, string managerName);

        void Remove(string key, string managerName);
    }
}
