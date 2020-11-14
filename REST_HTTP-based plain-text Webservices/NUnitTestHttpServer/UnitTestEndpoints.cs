using NUnit.Framework;
using NUnit.Framework.Internal;
using REST_HTTP_based_plain_text_Webservices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace NUnitTestHttpServer
{
    public class EndpointsTests
    {
        private const string path1 = "/messages";
        private const string path2 = "/messages/0";
        private const string path3 = "/messages/1";
        private const string path4 = "/messages/1/text";
        private const string path5 = "";
        private const string path6 = "/text";

        private const string method1 = "GET";
        private const string method2 = "POST";
        private const string method3 = "DELETE";
        private const string method4 = "PUT";
        private const string method5 = "COPY";

        private const string m1 = "";
        private const string m2 = "test1";
        private const string m3 = "test2";

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
            bool resultCheck = true;
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
        [Test]
        public void Test8()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method1, path1, m1);
            Response resultCommand = new Response { status = HttpStatus.No_Content };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void Test9()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method2, path1, m1);
            Response resultCommand = new Response { status = HttpStatus.Ok, content = m1 + " is added." };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void Test10()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.Methodhandler(method2, path1, m2);
            Response ExpectedCommand = response.Methodhandler(method1, path2, m1);
            Response resultCommand = new Response { status = HttpStatus.Ok, content = m2 + " is saved at this id." };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void Test11()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.Methodhandler(method2, path1, m2);
            response.Methodhandler(method2, path1, m3);
            Response ExpectedCommand = response.Methodhandler(method1, path3, m1);
            Response resultCommand = new Response { status = HttpStatus.Ok, content = m3 + " is saved at this id." };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]
        public void Test12()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.Methodhandler(method2, path1, m2);
            response.Methodhandler(method2, path1, m3);
            Response ExpectedCommand = response.Methodhandler(method1, path1, m1);
            string joined = string.Join("\n", m2 , m3);
            Response resultCommand = new Response { status = HttpStatus.Ok, content = " You have these Messages:\n" + joined };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]

        public void Test13()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.Methodhandler(method2, path1, m2);
            Response ExpectedCommand = response.Methodhandler(method4, path2, m3);
            Response resultCommand = new Response { status = HttpStatus.Ok, content = m3 + " is updated at this id." };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]

        public void Test14()
        {
            EndPointsResponse response = new EndPointsResponse();
            response.Methodhandler(method2, path1, m2);
            Response ExpectedCommand = response.Methodhandler(method3, path2, m1);
            Response resultCommand = new Response { status = HttpStatus.Ok, content = " Message deleted at this id." };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }
        [Test]

        public void Test15()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method5, path1, m1);
            Response resultCommand =  new Response { status = HttpStatus.Method_Not_Allowed };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }

        [Test]

        public void Test16()
        {
            EndPointsResponse response = new EndPointsResponse();
            Response ExpectedCommand = response.Methodhandler(method1, path6, m1);
            Response resultCommand = new Response { status = HttpStatus.Not_Found };
            Assert.AreEqual(ExpectedCommand, resultCommand);
        }


    }
}
