using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace HTTPClientConsole
{
    class Program
    {

        static void SendData(string address, string datagramm)
        {
            try
            {
                string url = address;
                HttpWebRequest reqw = (HttpWebRequest)WebRequest.Create(url);
                reqw.Method = "POST";
                byte[] data = Encoding.ASCII.GetBytes(datagramm);
                reqw.GetRequestStream().Write(data, 0, data.Length);
                reqw.GetRequestStream().Close();
                Console.WriteLine("Сообщение отправлено...");
                HttpWebResponse resp = (HttpWebResponse)reqw.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251));
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();

            }
            catch (Exception ex)
            {
                Console.Write("Ошибка: " + ex.Message);
            }
        }


        static void Main(string[] args)
        {
            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();
            SendData("http://123.0.0.1:80/", message);
            Console.Read();
        }
    }
}
