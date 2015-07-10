using System;

namespace OperatorOverloading.dbl
{
    public class CurrencyConvertor : IParse
    {
        public CurrencyConvertor()
        {
        }

        public double Conversion(string fromCurrency, string toCurrency, string path)
        {
            // Not checking for NULL on fromCurrency as the check is already done.
            // We get fromCurrency from an already existing Money object so the currecny 
            // is already checked while object creation.
            // Please comment if the case is still possible. -IW

            if (string.IsNullOrWhiteSpace(toCurrency))
            {
                throw new Exception(Messages.NullInputs);
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new Exception(Messages.NullPath);
            }

            double rate = 0;
            // Should add code for any existing type to any existing type.
            string searchString = fromCurrency.ToUpper() + toCurrency.ToUpper();
            // Checkling if data is to be read from FILE or URL and call the Class appropriately. 
            if (new Uri(path).IsFile)
            {
                var fetcherObj = new FetchFromFile(path);
                rate = fetcherObj.FetchRate(searchString);
            }
            else
            {
                var fetcherObj = new FetchFromWeb(path);
                rate = fetcherObj.FetchRate(searchString);
            }
            if (rate < 0)
            {
                throw new Exception(Messages.NoResults);
            }
            return rate;
        }
    }
}
