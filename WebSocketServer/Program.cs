using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;


namespace HTTPWebSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.Start("http://localhost:5050/");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
