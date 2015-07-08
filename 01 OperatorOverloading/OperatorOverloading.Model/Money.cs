using System;

namespace OperatorOverloading.Model
{
    public class Money
    {
        private double _amount;
        private string _currency;

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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidCurrencyException(Messages.InvalidCurrency);
                }
                _currency = value;
            }
        }

        public Money(double amount, string cur)
        {
            Amount = amount;
            Currency = cur;
        }

        public Money(string inputAmount)
        {

            if (string.IsNullOrWhiteSpace(inputAmount))
            {
                throw new InvalidCurrencyException(Messages.NullValue);
            }

            var amountArr = inputAmount.Split(' ');
            double amount;

            if (amountArr.Length != 2)
            {
                throw new InvalidCurrencyException(Messages.InvalidInput);
            }

            Currency = amountArr[1];

            if (double.TryParse(amountArr[0], out amount))
            {
                Amount = amount;
            }
            else
            {
                throw new InvalidCurrencyException(Messages.InvalidAmount);
            }
        }

        public static Money operator +(Money obj1, Money obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                throw new InvalidCurrencyException(Messages.NullValue);
            }
            if (obj1._currency.Equals(obj2._currency, StringComparison.CurrentCultureIgnoreCase))
            {
                double totalAmount = obj1.Amount + obj2.Amount;
                return new Money(totalAmount, obj1.Currency);
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
}
