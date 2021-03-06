﻿using System;
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
                throw new InvalidCurrencyException(Messages.MismatchedCurrency);
            }
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
            double exchangeRate = new CurrencyConvertor().GetConversion(Currency, convertTo);

            return new Money(exchangeRate * Amount, convertTo);
        }
    }
}
