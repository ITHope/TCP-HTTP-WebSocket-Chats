using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace HTTPChats
{
    class ServerHTTPListener
    {
        public ServerHTTPListener(string strAddr, string pt)
        {
            try
            {
                string url = strAddr;
                string port = pt;
                string prefix = String.Format("http://{0}:{1}/", url, port);

                HttpListener listener = new HttpListener();
                listener.Prefixes.Add(prefix);
                listener.Start();

                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
                Console.WriteLine("Ожидание... запрос будет обработан асинхронно.");
                result.AsyncWaitHandle.WaitOne();

                Console.WriteLine("Запрос обрабатывается асинхронно");
                listener.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        public static void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            Console.WriteLine("Вывод сообщения на экран");
            string responseString = "Echo: ";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            Console.Read();
            output.Close();

        }
    }
}
