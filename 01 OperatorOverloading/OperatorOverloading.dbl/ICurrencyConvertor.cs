using System;

namespace OperatorOverloading.DBL
{
    public interface ICurrencyConvertor
    {
        double GetConversion(string fromCurrency, string toCurrency);
    }
}
