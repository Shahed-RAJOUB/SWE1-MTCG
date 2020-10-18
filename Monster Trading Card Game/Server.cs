using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Monster_Trading_Card_Game
{
    public class Server
    {
        public static void Main()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);

            server.Start();

            Console.WriteLine("Server has started on 127.0.0.1:8000.{0}Waiting for a connection...", Environment.NewLine);

            try
            {
                TcpClient client = server.AcceptTcpClient();
                using var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                writer.WriteLine("Welcome to myserver!");
                writer.WriteLine("A client connected.");

                using var reader = new StreamReader(client.GetStream());
                string message;
               // do
               // {
                    message = reader.ReadLine();
                    Console.WriteLine("received: " + message);
                //} while (message != "quit");
            }
            catch (Exception exc) {
                Console.WriteLine("error occurred: " + exc.Message);
            }

        }
    }
}
