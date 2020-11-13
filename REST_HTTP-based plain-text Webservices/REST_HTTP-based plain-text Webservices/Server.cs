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
    public class Server
    {
        public static NetworkStream stream;

        public void Connection()
        {

            string ip = "127.0.0.1"; // localhost
            int port = 8000;
            
            var server = new TcpListener(IPAddress.Parse(ip), port);
            
            server.Start();
            Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);

            while (true)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("A client connected.");
                     stream = client.GetStream();
                    Thread ClientThread = new Thread(() => getMessage(client));
                    ClientThread.Start();

                    //ClientThread.Join();

                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                    byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 500 Internal Server Error\n\r\n\r");
                    stream.Write(response, 0, response.Length);
                    stream.Flush();
                }


            }

        }

     


        public static void getMessage(TcpClient client)
        {
            try
            {
                string data;
                byte[] bytes = new byte[client.Available];
               stream.Read(bytes, 0, client.Available);
                data = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(data);
                RequestContent request = new RequestContent(data);
                request.Requesthandler(stream);
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("error occurred: " + e.Message);
                byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 400 Bad Request\n\r\n\r");
                stream.Write(response, 0, response.Length);
                stream.Flush();


            }

        }
    }
}
