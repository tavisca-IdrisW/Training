using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using EntLib = Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.EnterpriseLibrary
{
    public class ExceptionPolicy 
    {
        public static bool HandleException(string policy, Exception ex)
        {
            return EntLib.ExceptionPolicy.HandleException(ex, policy);
        }

        public static bool HandleException(string policy, Exception ex, out Exception newEx)
        {
            return EntLib.ExceptionPolicy.HandleException(ex, policy, out newEx);
        }
    }
}
