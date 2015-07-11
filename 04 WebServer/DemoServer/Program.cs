using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Server on port 3030");
            HTTPServer server = new HTTPServer(3030);
            server.Start();
        }
    }
}
