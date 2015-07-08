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
