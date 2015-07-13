using System;

namespace OperatorOverloading.DBL
{
    public interface IParse
    {
        double GetConversion(string fromCurrency, string toCurrency);
    }
}
