using System;
using System.Runtime.Serialization;


namespace OperatorOverloading.Model
{
    public class Money
    {
        private double _amount { get; set; }
        private String _currency { get; set; }

        public Money()
        { }

        public Money(double amt, string cur)
        {
            _amount = amt;
            _currency = cur;
        }

        public Money(string inputAmount)
        {
            string[] amountArr;
            double amt;

            if (String.IsNullOrEmpty(inputAmount))
            {
                throw new InvalidCurrencyException("InvalidCurrency: Input Currency Cannot be NULL.");
            }
            amountArr = inputAmount.Split(' ');
            _currency = amountArr[1];

            if (double.TryParse(amountArr[0], out amt))
            {
                if (amt > 0 && amt < double.MaxValue)
                {
                    _amount = amt;
                }
                else
                {
                    throw new InvalidCurrencyException("InvalidCurrency: Please Enter Proper Currency Values.");
                }
            }
            else
            {
                throw new InvalidCurrencyException("InvalidCurrency: Please Enter Proper Currency Values.");
            }


        }

        public static Money operator +(Money obj1, Money obj2)
        {
            if (obj1._currency.Equals(obj2._currency, StringComparison.CurrentCultureIgnoreCase))
            {
                Double TotalAmount = obj1._amount + obj2._amount;

                if(TotalAmount > double.MaxValue)
                {
                    throw new InvalidCurrencyException("InvalidCurrency: Result is too large.");
                }
                return new Money(TotalAmount, obj1._currency);
            }
            else
            {
                throw new CurrencyMismatchException("CurrencyTypeMismatch: Please Enter Currency of the Same Type!!");
            }
        }

        public override string ToString()
        {
            return _amount + " " + _currency;
        }
    }


    public class CurrencyMismatchException : Exception, ISerializable
    {
        public CurrencyMismatchException()
        {
        }
        public CurrencyMismatchException(string message)
            : base(message)
        {
        }
        public CurrencyMismatchException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CurrencyMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public class InvalidCurrencyException : Exception, ISerializable
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
