using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace REST_HTTP_based_plain_text_Webservices
{
    public class Program
    {


        public static void Main()
        {

            string ip = "127.0.0.1"; // localhost
            int port = 8000;
            var server = new TcpListener(IPAddress.Parse(ip), port);

            server.Start();
            Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);

            while (true) {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("A client connected.");
                    NetworkStream stream = client.GetStream();
                    Thread ClientThread = new Thread(() => getMessage(client, stream));
                    ClientThread.Start();

                    //ClientThread.Join()

        }
    }
}