using System;
using System.Runtime.Serialization;


namespace OperatorOverloading.Model
{
    public class Money
    {
        private double _amount;
        private String _currency;

        public double Amount
        {
            get { return _amount; }
            private set
            {
                if (value < 0)
                {
                    throw new InvalidCurrencyException(Messages.AmountNegative);
                }
                if (double.IsPositiveInfinity(value))
                {
                    throw new InvalidCurrencyException(Messages.AmountTooLarge);                    
                }
                _amount = value;
            }
        }

        public string Currency
        {
            get { return _currency; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidCurrencyException(Messages.InvalidCurrency);
                }
                _currency = value;
            }
        }

        public Money(double amt, string cur)
        {
            Amount = amt;
            Currency = cur;
        }

        public Money(string inputAmount)
        {
            string[] amountArr;
            double amt;

            amountArr = inputAmount.Split(' ');
            Currency = amountArr[1];

            if (double.TryParse(amountArr[0], out amt))
            {
                Amount = amt;
            }
            else
            {
                throw new InvalidCurrencyException(Messages.InvalidAmount);
            }
        }

        public static Money operator +(Money obj1, Money obj2)
        {
            if (obj1._currency.Equals(obj2._currency, StringComparison.CurrentCultureIgnoreCase))
            {
                double totalAmount = obj1._amount + obj2._amount;

                if (double.IsPositiveInfinity(totalAmount))
                {
                    throw new InvalidCurrencyException(Messages.AmountTooLarge);
                }
                return new Money(totalAmount, obj1._currency);
            }
            else
            {
                throw new InvalidCurrencyException(Messages.MismatchedCurrency);
            }
        }

        public override string ToString()
        {
            return Amount + " " + Currency;
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
