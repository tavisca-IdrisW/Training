using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.ErrorSpace
{
    public class BaseApplicationException : Exception
    {
        public BaseApplicationException()
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public BaseApplicationException(string message)
            : base(message)
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public BaseApplicationException(string message, Exception ex)
            : base(message, ex)
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public BaseApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public BaseApplicationException(string code, string message)
            : base(message)
        {
            this.Timestamp = DateTime.UtcNow;
            this.Code = code;
        }

        public BaseApplicationException(string code, string message, Exception ex)
            : base(message, ex)
        {
            this.Timestamp = DateTime.UtcNow;
            this.Code = code;
        }

        public DateTime Timestamp { get; private set; }

        public virtual string Code { get; set; }
    }
}
