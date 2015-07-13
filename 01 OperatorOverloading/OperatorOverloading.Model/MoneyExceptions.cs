using System;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Runtime.Serialization;

namespace OperatorOverloading.Model
{
    [Serializable]
    public class InvalidCurrencyException : Exception
    {
        public InvalidCurrencyException()
        {
        }
        public InvalidCurrencyException(string message)
            : base(message)
        {
        }
        public InvalidCurrencyException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected InvalidCurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

}
