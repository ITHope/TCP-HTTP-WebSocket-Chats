using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chats
{
    class Program
    {
        static void TCPConnection(TcpListener listener)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection: Client connected.");

                NetworkStream stream = client.GetStream();

                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);

                while (true)
                {
                    string data = reader.ReadString();

                    Console.WriteLine($"Client: {data}");

                    writer.Write(data);

                    Console.WriteLine($"Server: {data}");
                }
            }
            catch
            {
                Console.WriteLine("Connection: Client was disconnected.");
            }
        }


        static void Main(string[] args)
        {

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8080;

            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();

            Console.WriteLine("Connection: Waiting for a client");

            while (true)
            {
                TCPConnection(listener);
            }

        }
    }
}
