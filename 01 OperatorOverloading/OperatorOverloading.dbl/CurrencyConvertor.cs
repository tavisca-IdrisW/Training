using System;
using System.Collections.Generic;

namespace OperatorOverloading.DBL
{
    public class CurrencyConvertor : ICurrencyConvertor
    {
        public double GetConversion(string fromCurrency, string toCurrency)
        {
            if (string.IsNullOrWhiteSpace(toCurrency) || string.IsNullOrWhiteSpace(fromCurrency))
            {
                throw new Exception(Messages.NullInputs);
            }

            double rate = 0;

            string searchString = fromCurrency.ToUpper() + toCurrency.ToUpper();
            // We will have keys of length 6 that will be containing teh conversion, 
            // otherwise, just return not found. 
            if (searchString.Length != 6)
            {
                throw new Exception(Messages.NoResults);
            }

            Dictionary<string, string> items = new ACurrencyConvertor().GetRate();

            foreach (var item in items)
            {
                if (item.Key.Contains(searchString))
                {
                    if (double.TryParse(item.Value, out rate))
                    {
                        if (double.IsPositiveInfinity(rate))
                        {
                            throw new Exception(Messages.InvalidResult);
                        }

                        return rate;
                    }

                    else
                    {
                        throw new Exception(Messages.TypeMismatch);
                    }
                }
            }

            throw new Exception(Messages.NoResults);
        }
    }
}
