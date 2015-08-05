using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.ErrorSpace
{
    public class DataAccessException : BaseApplicationException
    {
        public DataAccessException()
        {
        }

        public DataAccessException(string message)
            : base(message)
        {
        }

        public DataAccessException(string message, Exception ex)
            : base(message, ex)
        {
        }

        public DataAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public DataAccessException(string code, string message) :
            base( code, message)
        {
        }

        public DataAccessException(string code, string message, Exception ex)
            : base(code, message, ex)
        {
        }

        internal DataAccessException(string code, string messageTemplate, params object[] args) :
            base(code, string.Format(messageTemplate, args))
        {
        }
    }
}
