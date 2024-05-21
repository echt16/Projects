using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Project_Server_
{
    internal class Program
    {
        static int port = 8085;

        static IPAddress ip1;
        static int port1 = 8081;
        static IPAddress ip2;
        static int port2 = 8082;

        static bool p1 = false;
        static bool p2 = false;

        static bool isGame = false;

        static void Main(string[] args)
        {
            ThreadStart start = new ThreadStart(RecieveData);
            Thread thread = new Thread(start);
            thread.Start();
        }

        static void RecieveData()
        {
            while (!isGame)
            {
                UdpClient client = new UdpClient(port);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> texts = text.Split('|').ToList();
                Console.WriteLine(text);
                if (texts[0] == "p1=true")
                {
                    if (p1 == false)
                    {
                        p1 = true;
                        ip1 = IPAddress.Parse(texts[1]);
                    }
                }
                if (texts[0] == "p2=true")
                {
                    if (p2 == false)
                    {
                        p2 = true;
                        ip2 = IPAddress.Parse(texts[1]);
                    }
                }

                if (p1 && p2)
                {
                    isGame = true;
                    SendData(RandomDeck());
                }

                client.Close();
            }

            while (true)
            {
                UdpClient client = new UdpClient(port);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> texts = text.Split('|').ToList();

                SendData(text);

                client.Close();
            }
        }

        static void SendData(string text)
        {
            byte[] bytes1 = Encoding.Unicode.GetBytes(text);
            byte[] bytes2 = bytes1.ToArray();

            Thread.Sleep(5); 

            UdpClient client1 = new UdpClient();
            IPEndPoint ipEnd1 = new IPEndPoint(ip1, port1);
            UdpClient client2 = new UdpClient();
            IPEndPoint ipEnd2 = new IPEndPoint(ip2, port2);

            client1.Send(bytes1, bytes1.Length, ipEnd1);
            Thread.Sleep(5);
            client2.Send(bytes2, bytes2.Length, ipEnd2);

            client2.Close();
            client1.Close();

            Console.WriteLine(text);
        }

        static string RandomDeck ()
        {
            string result = "deck";

            List<string> cards = new List<string>();
            for (int i = 1; i <=4; i++)
            {
                for (int j = 6; j <= 14; j++)
                {
                    cards.Add(j.ToString() + "&" + i.ToString());
                }
            }

            Random rand = new Random();

            for (int i = 0; i < 36; i++)
            {
                int r = rand.Next(cards.Count);
                result += "|" + cards[r];
                cards.RemoveAt(r);
            }

            return result;
        }
    }
}
