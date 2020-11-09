using System.Net.Http;
using System.Threading;
using NUnit.Framework;
using REST_HTTP_based_plain_text_Webservices;

namespace NUnitTestHttpServer
{
    public class ServerTests
    {
        private ServerTests test;
        private Server testServer;

        [SetUp]
        public void Setup()
        {
            test = new ServerTests();
        }

        [Test]
        public void Test1()
        {

            //var mockedHttpClient = new Mock<HttpClient>();
             testServer = new Server();
            Thread thread1 = new Thread(testServer.Connection);
            thread1.Start();
            var test = new HttpClient();
            var response = test.PostAsync("http://localhost:8000/messages", new StringContent("content"));
            //Console.WriteLine(response.Result.ToString());
            thread1.Join();
           // test.Connection();
           // Assert.Pass();
        }
    }
}