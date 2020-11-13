using NUnit.Framework;
using NUnit.Framework.Internal;
using REST_HTTP_based_plain_text_Webservices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace NUnitTestHttpServer
{
    public class ResponseTests
    {
        private const string path1 = "/messages";
        private const string path2 = "/messages/0";
        private const string path3 = "/messages/1";
        private const string path4 = "/messages/1/text";
        private const string path5 = "";
        private const string path6 = "/text";
      
        [Test]
        public void Test1()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path1);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "messages";
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void Test2()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path1);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "messages";
            Assert.AreEqual(ExpectedCommand, resultCommand);
            bool ExpectedCheck = response.getCheck1();
            bool resultCheck = false;
            Assert.AreEqual(ExpectedCheck, resultCheck);
        }
        [Test]
        public void Test3()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path2);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "messages";
            Assert.AreEqual(ExpectedCommand, resultCommand);
            int ExpectedId = response.getId();
            int resultId = 0;
            Assert.AreEqual(ExpectedId, resultId);
        }
        [Test]
        public void Test4()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path3);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "messages";
            Assert.AreEqual(ExpectedCommand, resultCommand);
            int ExpectedId = response.getId();
            int resultId = 1;
            Assert.AreEqual(ExpectedId, resultId);
        }
        [Test]
        public void Test5()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path4);
            bool ExpectedCheck = response.getCheck2();
            bool resultCheck = false;
            Assert.AreEqual(ExpectedCheck, resultCheck);
        }
        [Test]
        public void Test6()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path5);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "";
            Assert.AreEqual(ExpectedCommand, resultCommand);
            bool ExpectedCheck = response.getCheck2();
            bool resultCheck = false;
            Assert.AreEqual(ExpectedCheck, resultCheck);
        }
        [Test]
        public void Test7()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path6);
            string ExpectedCommand = response.getCommand();
            string resultCommand = "text";
            Assert.AreEqual(ExpectedCommand, resultCommand);
          
        }
      /*  [Test]
        public void Test8()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.getCommandandID(path6);
            response.Methodhandler("POST", path6, "", stream);
            bool ExpectedCheck = response.getCheck3();
            bool resultCheck = false;
            Assert.AreEqual(ExpectedCheck, resultCheck);

        }*/
    }
}
