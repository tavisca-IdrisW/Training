using System;

namespace OperatorOverloading.dbl
{
    interface IParse
    {
        double Conversion(string fromCurrency, string toCurrency, string path);
    }
}
