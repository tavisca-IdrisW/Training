using System;
using System.Collections.Generic;

namespace OperatorOverloading.DBL
{
    public class CurrencyConvertor : ICurrencyConvertor
    {
        public CurrencyConvertor()
        {
        }

        public double GetConversion(string fromCurrency, string toCurrency)
        {
            // Not checking for NULL on fromCurrency as the check is already done.
            // We get fromCurrency from an already existing Money object so the currecny 
            // is already checked while object creation.
            // Please comment if the case is still possible. -IW

            if (string.IsNullOrWhiteSpace(toCurrency))
            {
                throw new Exception(Messages.NullInputs);
            }

            double rate = 0;
            // Should add code for any existing type to any existing type.

            string searchString = fromCurrency.ToUpper() + toCurrency.ToUpper();
            // We will have keys of length 6 that will be containing teh conversion, 
            // otherwise, just return not found. 
            if (searchString.Length != 6)
            {
                throw new Exception(Messages.NoResults);
            }

            Dictionary<string, string> exchangeRates = new ACurrencyConvertor().GetRate();

            foreach (var item in exchangeRates)
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
