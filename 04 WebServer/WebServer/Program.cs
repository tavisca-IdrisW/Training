using System;

namespace WebServer
{
    public class Program
    {
       public static void Main(string[] args)
        {
            try
            {
                Listener serverListener = new Listener(3030);
                Console.Write("Listening for request at port 3030 ");
                serverListener.Run();
                Console.ReadKey();
                serverListener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}