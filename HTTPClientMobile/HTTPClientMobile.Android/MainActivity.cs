using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HTTPClientMobile.Droid
{
	[Activity (Label = "HTTPClientMobile.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        HttpClient client;
        string clientName;
        Thread thread;
        string strAddr;

        //TcpClient client;
        //BinaryReader reader;
        //BinaryWriter writer;

        Button btnSend;
        Button btnConnect;
        TextView listChat;
        EditText inputText;
        EditText inputIp;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            btnSend = FindViewById<Button>(Resource.Id.btnSend);
            btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            listChat = FindViewById<TextView>(Resource.Id.listChat);
            inputText = FindViewById<EditText>(Resource.Id.inputText);
            inputIp = FindViewById<EditText>(Resource.Id.ipInput);

            btnSend.Click += SendText;
            btnConnect.Click += ConnectToServer;
        }

        private string GetAddress(string strAddr, string pt)
        {
            string url = strAddr;
            string port = pt;
            string prefix = String.Format("http://{0}:{1}/", url, port);

            return prefix;
        }

        private void ConnectToServer(object sender, EventArgs e)
        {
            try
            {

                strAddr = GetAddress("localhost", "8888");
                client = new HttpClient();
                thread = new Thread(Listen);
                thread.Start();

                listChat.Text += "Connection: Connected!";
            }
            catch
            {
                listChat.Text += "Connection failed!";
            }

            
        }

        private void SendText(object sender, EventArgs e)
        {
            try
            {
                var response = await client.PostAsync(strAddr, new StringContent("message=" + tbChatText.Text));
                string content = await response.Content.ReadAsStringAsync();
                if (content == "received")
                    listChat.Text += "Client: " + tbChatText.Text;
            }
            catch
            {
                listChat.Text += "Cannot connect!";
            }

        }


        private async void Listen()
        {
            while (true)
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

        protected override void OnDestroy()
        {
            client.Close();
            base.OnDestroy();
        }
    }
}


