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
                Money Amount1 = new Money(Console.ReadLine());
                Console.Write("Enter Amount 2 (<Amt> CType): ");
                Money Amount2 = new Money(Console.ReadLine());

                Console.Write("The Total Amount is: ");
                Console.Write(Amount1 + Amount2);

                Console.ReadLine();
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception Occured.");
                Console.WriteLine(e.Message);
            }
            
                //Finally Added to atleast display the Exception.
            finally 
            {
                Console.ReadLine();
            }
        }
    }
}
