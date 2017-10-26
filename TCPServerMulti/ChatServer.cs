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
        TcpListener tcpListener = null;
        Dictionary<TcpClient, int> clients = null;
        int count = 0;

        public ChatServer(string local, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(local);
            tcpListener = new TcpListener(ipAddress, port);
            clients = new Dictionary<TcpClient, int>();
        }

        protected internal void AddConnection(TcpClient client)
        {
            if (clients == null)
            {
                clients = new Dictionary<TcpClient, int>();
            }
            clients.Add(client, ++count);
        }

        protected internal void RemoveConnection(TcpClient clientToRemove)
        {
            if(clients.Keys.Contains(clientToRemove))
            {
                clients.Remove(clientToRemove);
            }
        }

        public void Start()
        {
            try
            {
                tcpListener.Start();
                Console.WriteLine("Connection: Listening...");
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    clients.Add(tcpClient, ++count);
                    new BinaryWriter(tcpClient.GetStream()).Write("Connection: Connected to server!");
                    Console.WriteLine($"Client {clients[tcpClient]} connected!");
                    ThreadPool.QueueUserWorkItem(Listen, tcpClient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Disconnect();
            }
        }

        private void Listen(object obj)
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
                Console.WriteLine($"Connection: Client {clients[(obj as TcpClient)]} disconnected!");
                clients.Remove(obj as TcpClient);
            }
        }

        protected internal void Disconnect()
        {
            tcpListener.Stop();
            foreach(TcpClient client in clients.Keys)
            {
                client.Close();
            }
            Environment.Exit(0);
        }
    }
}
