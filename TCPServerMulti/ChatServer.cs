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
    class ChatServer
    {
        TcpListener server = null;
        Dictionary<TcpClient, int> clients = null;
        int count = 0;

        public ChatServer(string local, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(local);

            server = new TcpListener(ipAddress, port);
            clients = new Dictionary<TcpClient, int>();
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine("Connection: Waiting for a clients");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client, ++count);
                new BinaryWriter(client.GetStream()).Write("Connection: Connected to server!");

                Console.WriteLine($"Client {clients[client]} connected!");
                ThreadPool.QueueUserWorkItem(DoWork, client);
            }
        }

        private void DoWork(object obj)
        {
            try
            {
                while (true)
                {

                    string data = new BinaryReader((obj as TcpClient).GetStream()).ReadString();
                    Console.WriteLine($"Received from client {clients[obj as TcpClient]}: {data}");
                    
                    foreach (TcpClient client in clients.Keys)
                    {
                        new BinaryWriter(client.GetStream()).Write(data);
                        Console.WriteLine($"Sent to client {clients[client]}: {data}");
                    }

                }
            }
            catch
            {
                (obj as TcpClient).Close();
                Console.WriteLine($"Client {clients[(obj as TcpClient)]} have disconnected!");
                clients.Remove(obj as TcpClient);
            }
        }
    }
}
