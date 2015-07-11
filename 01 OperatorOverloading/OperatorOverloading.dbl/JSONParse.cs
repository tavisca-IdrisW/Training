using System;

namespace OperatorOverloading.DBL
{
    public abstract class JSONParse
    {
        /// <summary>
        /// Returns the Exchange Rate after parsing JSON.  
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="searchString"></param>
        /// <returns>ExchangeRate</returns>

        //TODO: Try teh file approach. Also read up on Tasks in C#. - IW
        //UPDATE: Don't try anyhthing new. Just do as you are told. - IW

        public double FetchRate(string jsonString, string searchString)
        {
            if (String.IsNullOrWhiteSpace(jsonString))
            {
                throw new Exception(Messages.InvalidResponse);
            }

            if (String.IsNullOrWhiteSpace(searchString))
            {
                throw new Exception(Messages.NullInputs);
            }

            // We will have keys of length 6 that will be containing teh conversion, 
            // otherwise, just return not found. 
            if (searchString.Length != 6)
            {
                throw new Exception(Messages.NoResults);
            }
            double rate = 0;
            var keyArray = jsonString.Split('{', '}', '[', ']');
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
