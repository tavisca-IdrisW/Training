using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.FileStorage
{
    internal static class Configurations
    {
        internal static string StoragePath
        {
            get
            {
                var path = ConfigurationManager.AppSettings["employee_storage_path"];
                return string.IsNullOrWhiteSpace(path) ? "." : path;
            }
        }
    }
}
