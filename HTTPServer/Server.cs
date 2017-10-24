using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.IO;

namespace HTTPChats
{
    class Server
    {
        TcpListener server = null;
        Dictionary<TcpClient, int> clients = null;
        int count = 0;

        public Server(string local, int port)
        {
            WebSocket sd = WebSocket.CreateServerBuffer(234);
            server = new TcpListener(IPAddress.Parse(local), port);
            clients = new Dictionary<TcpClient, int>();
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine("Waiting for a clients...");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client, ++count);
                new BinaryWriter(client.GetStream()).Write("You've connected to server!");
                Console.WriteLine($"Client {clients[client]} have connected!");
                ThreadPool.QueueUserWorkItem(DoWork, client);
            }
        }

        private void DoWork(object obj)
        {
            try
            {
                while (true)
                {

                    string data = ReadData((obj as TcpClient).GetStream());
                    Console.WriteLine($"Received from client {clients[obj as TcpClient]}: {data}");


                    foreach (TcpClient client in clients.Keys)
                    {
                        WriteData(client.GetStream(), data);
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

        private string ReadData(Stream stream)
        {
            byte[] buffer = new byte[1024 * 16];
            stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        private void WriteData(Stream stream, string data)
        {
            string Status = $"HTTP/1.1 200 OK\n";
            string ContentType = $"Content-Type: text/plain\n";
            string ContentLength = $"Content-Length: {data.Length}\n";
            string response = Status + ContentType + ContentLength + "\n" + data;
            stream.Write(Encoding.ASCII.GetBytes(response), 0, response.Length);
            //context.OutputStream.Flush();
        }
    }
}
