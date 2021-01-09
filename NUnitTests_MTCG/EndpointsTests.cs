using NUnit.Framework;
using Monster_Trading_Card_Game;
using NUnit.Framework.Internal;

namespace NUnitTest_MTCG
{
    public class EndpointsTests
    {
        private const string path1 = "/messages";
        private const string path2 = "/users/shahd/test";
        private const string path3 = "/cards/shahd/test/Body";
        private const string path7 = "/stats/1/text/body";
        private const string path8 = "/packages/1/text/body";
        private const string path4 = "/messages/1/text";
        private const string path5 = "";
        private const string path6 = "/text";
        private const string method1 = "GET";
        private const string method2 = "POST";
        private const string method5 = "COPY";
        private const string m1 = "";
        private const string Au1 = "Bearer admin-mtcgToken";
        private const int p = 2;

        [Test]
        public void TsetGetMsg()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandInfo(path1);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "messages";
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }

        [Test]
        public void TestGetlongMsgEmpty()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandInfo(path4);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "";
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void TestEmptyMsg()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandInfo(path5);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "";
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void TestGetshortMsg()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandInfo(path6);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "text";
            Assert.AreEqual(ExpectedCommand, resultCommand);

        }
        [Test]
        public void TestGetwithLongMsg()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method1, path2, m1 , Au1 , p);
            Response resultCommand = new Response { status = HttpStatus.Not_Found };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void TestPostwithLongMsg()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method2, path3, m1 , Au1, p);
            Response resultCommand = new Response { status = HttpStatus.Not_Found }; ;
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void TestCopyMsg()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method5, path3, m1, Au1, p);
            Response resultCommand = new Response { status = HttpStatus.Not_Found }; ;
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void TestPostStatLong()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method2, path7, m1, Au1, p);
            Response resultCommand = new Response { status = HttpStatus.Not_Found}; ;
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void TestPostPackageLong()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method2, path8, m1, Au1, p);
            Response resultCommand = new Response { status = HttpStatus.Not_Found }; ;
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }



    }
}
