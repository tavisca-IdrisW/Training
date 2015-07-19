using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Tavisca.Employee.BusinessLogic
{
    public interface IEmployeeHandler
    {
        string GenerateId();
    }
}
