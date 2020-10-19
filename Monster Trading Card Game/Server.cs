using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Monster_Trading_Card_Game
{
    public class Server
    {
        public  void Connection()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);

            server.Start();

            Console.WriteLine("Server has started on 127.0.0.1:8000.{0}Waiting for a connection...", Environment.NewLine);

            try
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("A client connected.");

                NetworkStream stream = client.GetStream();

                //while (true){
                //while (!stream.DataAvailable) ;

                byte[] bytes = new byte[client.Available];
                stream.Read(bytes, 0, client.Available);
                string message  = Encoding.UTF8.GetString(bytes);
                string respond;

                if (Regex.IsMatch(message, "^GET"))
                {
                    respond = "=====Handshaking from client/ GET Request=====\n";
                    Console.WriteLine(respond);
                }
                else if (Regex.IsMatch(message, "^POST"))
                {
                    Console.WriteLine(" =====  POST Request =====\n");
                }
                else if (Regex.IsMatch(message, "^PUT"))
                {
                    Console.WriteLine(" =====  PUT Request =====\n");
                }
                else if (Regex.IsMatch(message, "^DEL"))
                {
                    Console.WriteLine(" =====  DEL Request =====\n");
                }else
                {
                    Console.WriteLine(" =====  Using Other Request ! =====\n");
                }


                /* using var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                 writer.WriteLine("Welcome to myserver!");
                 writer.WriteLine("A client connected.");
                 using var reader = new StreamReader(client.GetStream());
                 string message;
                  do{
                     message = reader.ReadLine();
                     Console.WriteLine("received: " + message);
                 } while (message != "quit");*/
                //}
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }

        }
    }
}
