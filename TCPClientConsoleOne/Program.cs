using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static void TCPConnection()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 8080;

                TcpClient client = new TcpClient();
                client.Connect(ipAddress, port);

                StringBuilder message = new StringBuilder();
                NetworkStream stream = client.GetStream();

                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);

                while (true)
                {
                    Console.WriteLine("Server:" + reader.ReadString());

                    Console.Write("Client:");

                    writer.Write(Console.ReadLine());

                    Console.WriteLine("Server:" + reader.ReadString());
                }
            }
            catch
            {
                Console.WriteLine("Connection: Cannot connect to server.");
            }
        }


        static void Main(string[] args)
        {
            while (true)
            {
                TCPConnection();
            }
        }
    }
}
