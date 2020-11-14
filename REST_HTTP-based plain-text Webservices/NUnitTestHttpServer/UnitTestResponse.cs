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
       


        [Test]
        public void Test1()
        {
            Response response = new Response { status = HttpStatus.Ok , content = " message "};
            string result = response.formatted_Response();
            string Expected = "HTTP/1.1 200 OK\n\r message ";
            Assert.AreEqual(Expected, result);
        }


        [Test]
        public void Test2()
        {
            Response response = new Response { status = HttpStatus.Ok };
            string result = response.formatted_Response();
            string Expected = "HTTP/1.1 200 OK\n\r\n\r";
            Assert.AreEqual(Expected, result);
        }
    }
}
