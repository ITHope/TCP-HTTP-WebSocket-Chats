using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServerMulti
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("123.100.0.1");
            int port = 8080;

            ChatServer server = new ChatServer(ipAddress.ToString(), port);
            server.Start();
        }
    }
}
