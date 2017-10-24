using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections;

namespace HTTPChats
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 8888);
            server.Start();
            Console.ReadKey();
        }

        
    }
}
