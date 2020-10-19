using NUnit.Framework;
using Monster_Trading_Card_Game;
using Moq;
using System.Net.Http;
using System.Threading;
using System;

namespace NUnitTests_MTCG
{
    public class Server_test
    {
     

        [Test]
        public void Server_Test_GetRequest ()
        {
            //var mockedHttpClient = new Mock<HttpClient>();
            var Program = new Server();
            Thread thread1 = new Thread(Program.Connection);
            thread1.Start();
            var test = new HttpClient();
            var response = test.PostAsync("http://localhost:8000/hi", new StringContent("content"));
            //Console.WriteLine(response.Result.ToString());
            thread1.Join();
        }

    }
}