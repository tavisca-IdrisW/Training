using System;

namespace OperatorOverloadingModel
{
    public class Money
    {
        private Double Amount;
        private String CurrencyType;

        /**
         * CONSTRUCTOR: This will accept the Amount as a string Value 
         * (i.e.: of the format <Amt> <CurType>).
         * 
         * This string is first split to seperate teh Amount from the Currency Type.
         * 
         * Check if we accept the currency type. 
         * If yes, then we will proceed else throw an ``InvalidCurrencyException``.
         * 
         * Similarly, we check for the input Amount (here we check if the amount is parsable 
         * as a Double or not).
         * 
         * 
         * For now only USD, INR and YEN have been handled. 
         * 
         * TODO: Find a better solution than using arrays. 
         * Reduce varaible count.(I know it can be done!!) -IW
         */
        public Money(string InputAmount)
        {
            string[] AmountArr;

            /**
             * TODO: Find a better way to do this conversion.
             * Assuming All Amount to be entered with teh Type Prefixed.
             * -IW
             */

            // Convert The string to amount value and type.
            AmountArr = InputAmount.Split(' ');

            string AcceptedCurTypes = "USD INR YEN";

            if (AcceptedCurTypes.Contains(AmountArr[1]))
            {
                CurrencyType = AmountArr[1];
            }

            else 
            {
                throw new InvalidCurrencyException();
            }

            if (!Double.TryParse(AmountArr[0], out Amount))
            {
                throw new InvalidCurrencyException();
            }
        }

        /**
         * OVERLOADING OPERATOR:
         * 
         * Here, teh + operator is overloaded to Add two Money-type Objects and return result.
         * 
         * Here, a check has been put in place to compare the Two currency values. 
         * If the values do not match then we will throw a ``CurrencyTypeMismatchException``.
         */
        public static Money operator +(Money Obj1, Money Obj2)
        {
            if (Obj1.CurrencyType.Equals(Obj2.CurrencyType))
            {
                Double TotalAmount = Obj1.Amount + Obj2.Amount;
                String TotalValue = TotalAmount + " " + Obj1.CurrencyType;
                return new Money(TotalValue);
            }
            else
            {
                throw new CurrencyTypeMismatchException(); 
            }
        }

        /**
         * OVERLOADING ``ToString()`` method.
         * 
         * Here, the ToString Method is overloaded to display the output in
         * string format. This can be passed as an input to Console.Write()
         * or any such logger.
         * 
         */
        public override string ToString()
        {
            return Amount + " " + CurrencyType;
        }
    }

    /**
     * EXCEPTION CLASSES.
     * 
     * 
     * TODO: Try To pass Message with Exception Specific Information.
     */
    public class CurrencyTypeMismatchException : Exception
    {
        //string ErrorType;

        public CurrencyTypeMismatchException()
        {
            //ErrorType = "CurrencyTypeMismatch: Please Enter Currency of the Same Type!!";
        }
    }

    public class InvalidCurrencyException : Exception
    {
        //string ErrorType;

        public InvalidCurrencyException()
        {
            //ErrorType = "InvalidCurrency: Please Enter Currency of Type- <Amt> (USD, INR or YEN)!!";
        }
    }
}
