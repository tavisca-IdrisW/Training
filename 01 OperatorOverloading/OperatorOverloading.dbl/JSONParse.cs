using System;
using System.Net;

namespace OperatorOverloading.dbl
{
    public class JSONParse : IParse
    {
        public double Conversion(string fromCurrency, string toCurrency)
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
            var json = new WebClient().DownloadString("http://www.apilayer.net/api/live?access_key=a8f70a4d56dd71ef3d37065d7e3f3045&format=1");
            string searchString = fromCurrency.ToUpper() + toCurrency.ToUpper();
            rate = FetchResult(json, searchString);
            if (rate < 0)
            {
                throw new Exception(Messages.NoResults);
            }
            return rate;
        }
        /// <summary>
        /// TODO: Try teh file approach. Also read up on Tasks in C#. - IW
        /// </summary>
        /// <param name="jsonString">Teh Response recieved from Server.</param>
        /// <param name="searchString">Conversion rates to be searched.</param>
        /// <returns>rates</returns>
        public double FetchResult(string jsonString, string searchString)
        {
            if (String.IsNullOrWhiteSpace(jsonString))
            {
                throw new Exception(Messages.InvalidResponse);
            }

            if (String.IsNullOrWhiteSpace(searchString))
            {
                throw new Exception(Messages.NullInputs);
            }

            if (searchString.Length != 6)
            {
                throw new Exception(Messages.NoResults);
            }
            double rate = 0;
            var keyArray = jsonString.Split('{', '}', ',');
            foreach (string str in keyArray)
            {
                if (str.Contains(searchString))
                {
                    var resultArray = str.Split(':');
                    if (double.TryParse(resultArray[1], out rate))
                    {
                        if (rate < 0 || double.IsPositiveInfinity(rate))
                        {
                            throw new Exception(Messages.InvalidRate);
                        }
                        return rate;
                    }
                    else
                    {
                        throw new Exception(Messages.TypeMismatch);
                    }
                }
            }
            return -1;
        }
    }
}
