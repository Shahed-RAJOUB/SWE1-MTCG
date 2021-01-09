using NUnit.Framework;
using Monster_Trading_Card_Game;
using NUnit.Framework.Internal;


namespace NUnitTest_MTCG
{
    public class ResponseTests
    {



        [Test]
        public void TestResponseWithMassage()
        {
            Response response = new Response { status = HttpStatus.Ok, content = " message " };
            string result = response.formatted_Response();
            string Expected = "HTTP/1.1 200 OK\n\r message ";
            Assert.AreEqual(Expected, result);
        }


        [Test]
        public void TestResponseWithoutMassage()
        {
            Response response = new Response { status = HttpStatus.Ok };
            string result = response.formatted_Response();
            string Expected = "HTTP/1.1 200 OK\n\r\n\r";
            Assert.AreEqual(Expected, result);
        }
    }
}