using System;
using OperatorOverloading.Model;

namespace OperatorOverloading.Host
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter Amount 1 (<Amt> CType): ");
                Money amount1 = new Money(Console.ReadLine());

                Console.Write("Enter Amount 2 (<Amt> CType): ");
                Money amount2 = new Money(Console.ReadLine());

                Console.Write("The Total Amount is: ");
                Console.Write(amount1 + amount2);

                Console.Write("Enter Amount 3 (<Amt> CType): ");
                Money amount3 = new Money(Console.ReadLine());
                Console.Write("Enter Currency to Convert to: ");
                string convertTo = Console.ReadLine();
                Money convertedAmount = amount3.Convert(convertTo);

                Console.WriteLine("The Converted Amount is: " + convertedAmount);

            }

            catch (Exception e)
            {
                Console.WriteLine("Exception Occured.");
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
