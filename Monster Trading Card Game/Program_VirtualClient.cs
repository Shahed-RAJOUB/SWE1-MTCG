using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Monster_Trading_Card_Game
{
    class Program_VirtualClient
    {
        static async Task Main(string[] args)
        {
          await Task.Run(() =>
            {
                Server myServer = new Server();
                myServer.Connection();

            });
        }
    }
}