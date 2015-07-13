using System;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Listener serverListener = new Listener(8080);
                serverListener.Start();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
