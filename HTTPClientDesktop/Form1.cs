using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatWinForms
{
    public partial class Form1 : Form
    {
        HttpClient client;
        string clientName;
        Thread thread;
        string strAddr;

        public Form1()
        {
            InitializeComponent();
            
        }

        private string GetAddress(string strAddr, string pt)
        {
            string url = strAddr;
            string port = pt;
            string prefix = String.Format("http://{0}:{1}/", url, port);

            return prefix;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {

                strAddr = GetAddress("localhost", "8888");
                client = new HttpClient();
                thread = new Thread(Listen);
                thread.Start();

                lbChat.Items.Add("Connection: Connected!");
            }
            catch
            {
                lbChat.Items.Add("Connection failed!");
            }
        }

        private async void Listen()
        {
            while(true)
            {
                try
                {
                    string resp = await client.GetStringAsync(strAddr + "?check=" + clientName);
                    string[] parametrs = resp.Split('=');
                    if (parametrs[0] == "message")
                    {
                        if (lbChat.InvokeRequired)
                        {
                            lbChat.BeginInvoke(new Action(delegate
                            {
                                lbChat.Items.Add("Server:" + parametrs[1]);
                            }));
                        }
                    }
                    else if (parametrs[0] == "name")
                        clientName = parametrs[0];
                }
                catch
                {
                    lbChat.Items.Add("Connection: Failed!");
                }
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await client.PostAsync(strAddr, new StringContent("message=" + tbChatText.Text));
                string content = await response.Content.ReadAsStringAsync();
                if (content == "received")
                    lbChat.Items.Add("Client: " + tbChatText.Text);
            }
            catch
            {
                lbChat.Items.Add("Cannot connect!");
            }
            
        }
        
    }
}
