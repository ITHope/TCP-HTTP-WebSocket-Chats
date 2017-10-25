using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatWinForms
{
    public partial class Form1 : Form
    {
        TcpClient client;
        BinaryReader reader;
        BinaryWriter writer;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            try
            {
                client.Connect("127.0.0.1", 8080);

                NetworkStream stream = client.GetStream();
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);
                Thread thread = new Thread(Waiting);
                thread.Start();

            }
            catch
            {
                lbChat.Items.Add("Connection failed!");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (writer != null)
                {
                    writer.Write(tbChatText.Text);
                    lbChat.Items.Add("Client: " + tbChatText.Text);
                }
            }
            catch
            {
                lbChat.Items.Add("Connection lost!");
            }
        }

        private void Waiting()
        {
            try
            {
                while (true)
                {
                    string data = reader.ReadString();
                    if (data.Length != 0)
                    {
                        if (lbChat.InvokeRequired)
                        {
                            lbChat.BeginInvoke(new Action(delegate
                            {
                                lbChat.Items.Add("Server:" + data);
                            }));
                        }
                    }
                }
            }
            catch
            {
                if (lbChat.InvokeRequired)
                {
                    lbChat.BeginInvoke(new Action(delegate
                    {
                        lbChat.Items.Add("Connection lost!");
                    }));
                }
            }
        }
    }
}
