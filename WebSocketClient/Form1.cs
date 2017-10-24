using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using WebSocketSharp;

namespace HTTPWebSocketClient
{
    


    public partial class Form1 : Form
    {
        private WebSocket client;
        const string host = "wss://localhost:5050";

        private static object consoleLock = new object();
        private const int sendChunkSize = 256;
        private const int receiveChunkSize = 64;
        private const bool verbose = true;
        private static readonly TimeSpan delay = TimeSpan.FromMilliseconds(1000);


        public Form1()
        {
            InitializeComponent();

            client = new WebSocket(host);

            client.OnOpen += (ss, ee) =>
               listViewChat.Items.Add(string.Format("Connected to {0} successfully", host));
            client.OnError += (ss, ee) =>
               listViewChat.Items.Add("Error: " +ee.Message);
            client.OnMessage += (ss, ee) =>
               listViewChat.Items.Add("Echo: " +ee.Data);
            client.OnClose += (ss, ee) =>
               listViewChat.Items.Add(string.Format("Disconnected with { 0}", host));

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client.Connect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var content = textBoxChat.Text;
            if (!string.IsNullOrEmpty(content))
                client.Send(content);
        }
    }
}
