using System;
using System.Net;
using OperatorOverloading.DBL;

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

        /// <summary>
        /// Accepts Amount in double and a string for Currency Type.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        public Money(double amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// Accepts a single input string in teh form "Amount Currency"
        /// </summary>
        /// <param name="inputAmount"></param>
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

            if (double.TryParse(amountArr[0], out amount))
            {
                Amount = amount;
            }
            else
            {
                throw new InvalidCurrencyException(Messages.InvalidAmount);
            }

            Currency = amountArr[1];
        }

        public static Money operator +(Money obj1, Money obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                throw new InvalidCurrencyException(Messages.NullValue);
            }

            if (obj1.Currency.Equals(obj2.Currency, StringComparison.CurrentCultureIgnoreCase))
            {
                double totalAmount = obj1.Amount + obj2.Amount;
                return new Money(totalAmount, obj1.Currency);
            }

            else
            {
                // Written better code for this on git. - IW
                throw new InvalidCurrencyException(Messages.MismatchedCurrency);
            }

            //else
            //{
            //    double exchangeRate = ExchangeRate(obj1.Currency, obj2.Currency);
            //    return new Money(obj1.Amount * exchangeRate + obj2.Amount, obj2.Currency);
            //}
        }

        public override string ToString()
        {
            return Amount + " " + Currency.ToUpper();
        }

        /// <summary>
        /// Accepts a string of currency to be converted to.
        /// </summary>
        /// <param name="convertTo"></param>
        /// <returns></returns>
        public Money Convert(string convertTo)
        {
            var currencyConvertor = Activator.CreateInstance("Asseb", "CurrencyConvertor") as IParse;
            double exchangeRate = currencyConvertor.GetConversion("", "");

            //double exchangeRate = ExchangeRate(Currency, convertTo);
            return new Money(exchangeRate * Amount, convertTo);
        }

        //private static double ExchangeRate(string convertFrom, string convertTo)
        //{
        //    return FetchResult("USD", convertTo) / FetchResult("USD", convertFrom);
        //}

        //private Money Convert(string convertFrom, string convertTo)
        //{
        //    double result = FetchResult(convertFrom, convertTo);
        //    return new Money(result * Amount, convertTo);
        //}

        //private static double FetchResult(string convertFrom, string convertTo)
        //{
        //    return new CurrencyConvertor().GetConversion(convertFrom, convertTo, "http://www.apilayer.net/api/live?access_key=a8f70a4d56dd71ef3d37065d7e3f3045&format=1");
        //}
    }
}
