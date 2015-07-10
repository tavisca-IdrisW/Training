using System;

namespace OperatorOverloading.dbl
{
    public class JSONParse
    {
        /// <summary>
        /// Returns the Exchange Rate after parsing JSON.  
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="searchString"></param>
        /// <returns>ExchangeRate</returns>

        //TODO: Try teh file approach. Also read up on Tasks in C#. - IW

        public virtual double FetchResult(string jsonString, string searchString)
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
            var keyArray = jsonString.Split('{', '}', ',', '[', ']');
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
