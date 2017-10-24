using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClientMulti
{
    class Program
    {
        static BinaryReader reader;
        static BinaryWriter writer;


        private static void Connection()
        {
            while (true)
            {
                try
                {
                    TcpClient client = new TcpClient();
                    client.Connect("123.100.0.1", 9050);

                    StringBuilder response = new StringBuilder();
                    NetworkStream stream = client.GetStream();

                    reader = new BinaryReader(stream);
                    writer = new BinaryWriter(stream);

                    Thread thread = new Thread(Waiting);
                    thread.Start();

                    while (true)
                    {
                        string data = Console.ReadLine();
                        Console.Write("You:" + data + "\n");
                        writer.Write(data);
                    }
                }
                catch
                {
                    Console.WriteLine("Cannot connect to server.");
                }
            }
        }

        private static void Waiting()
        {
            try
            {
                while (true)
                {
                    string data = reader.ReadString();
                    if (data.Length != 0)
                    {
                        Console.WriteLine("Server:" + data);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Connection lost!");
            }
        }

        static void Main(string[] args)
        {
            Connection();
        }
    }
}
