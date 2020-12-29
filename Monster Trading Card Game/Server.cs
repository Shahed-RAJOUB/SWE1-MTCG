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

namespace  Monster_Trading_Card_Game
{
    public class Server
    {
        public static NetworkStream stream;
        EndPointsResponse EndPoints = new EndPointsResponse();

        public void Connection()
        {

            string ip = "127.0.0.1"; // localhost
            int port = 10001;

            var server = new TcpListener(IPAddress.Parse(ip), port);

            server.Start();
            Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);

            while (true)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("A Player is connected.");
                    stream = client.GetStream();
                    Thread ClientThread = new Thread(() => getMessage(client, EndPoints));
                    ClientThread.Start();

                    //ClientThread.Join();

                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                    SendResponse(new Response { status = HttpStatus.Internal_Server_Error });
                }


            }

        }




        public static void getMessage(TcpClient client, EndPointsResponse EndPoints)
        {
            try
            {
                string data;
                byte[] bytes = new byte[client.Available];
                stream.Read(bytes, 0, client.Available);
                data = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(data);
                RequestContent request = new RequestContent(data);
                //request.Requesthandler();
                string Method = request.getMethod();
                string Path = request.getPath();
                string Bodymessage = request.getMsg();
                Response response = EndPoints.Methodhandler(Method, Path, Bodymessage);
                SendResponse(response);
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("error occurred: " + e.Message);
                SendResponse(new Response { status = HttpStatus.Bad_Request });
                client.Close();
            }

        }

        private static void SendResponse(Response response)
        {
            byte[] res = Encoding.ASCII.GetBytes(response.formatted_Response());
            stream.Write(res, 0, res.Length);
            stream.Flush();
        }
    }
}
