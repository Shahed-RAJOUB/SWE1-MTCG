
using NUnit.Framework;
using NUnit.Framework.Internal;
using REST_HTTP_based_plain_text_Webservices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace NUnitTestHttpServer
{

    // testing the contructor class
    public class RequestsTests
    {
        private const string Expected_error = "Server recived an empty path!";
        private const string Input_Postman_String = @"POST /messages HTTP/1.1
Content-Type: application/json
User-Agent: PostmanRuntime/7.26.8
Accept: \*/\*
Postman-Token: 012f42e0-324f-4277-8b42-a71f46e66335
Host: localhost:8000
Accept-Encoding: gzip, deflate, br
Connection: keep-alive
Content-Length: 5

test1";
        private const string Browser_String = @"GET /messages HTTP/1.1
Host: localhost:8000
Connection: keep-alive
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.193 Safari/537.36
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9
Sec-Fetch-Site: none
Sec-Fetch-Mode: navigate
Sec-Fetch-User: ?1
Sec-Fetch-Dest: document
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9,de-AT;q=0.8,de;q=0.7,ar-AE;q=0.6,ar;q=0.5,ru;q=0.4
Cookie: Phpstorm-7f9060c7=9490fcdc-2e6b-4332-a777-49822bc307c2; Idea-9518ebca=1639b979-bc03-4c26-b340-6378fb67e57b; Pycharm-e3e9aa86=0d78eba3-6cd6-47a7-82db-e05c8fb1c64b; Idea-3edfb054=b536f8cb-adf2-4242-beb2-ac7a7a42bf3e
";
        [Test]
        public void Test1()
        {
            
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                RequestContent content = new RequestContent("");
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected_error, result);
            }
        }
        [Test]
        public void Test2()
        {
                RequestContent content = new RequestContent(Input_Postman_String);
                 string Expected = content.getMethod();
                 string result = "POST";
                Assert.AreEqual(Expected, result);
            
        }
        [Test]
        public void Test3()
        {
            RequestContent content = new RequestContent(Input_Postman_String);
            string Expected = content.getPath();
            string result = "/messages";
            Assert.AreEqual(Expected, result);

        }

        [Test]
        public void Test4()
        {
            RequestContent content = new RequestContent(Input_Postman_String);
            string Expected = content.getHttpVersion();
            string result = "HTTP/1.1";
            Assert.AreEqual(Expected, result);

        }

        [Test]
        public void Test5()
        {
            RequestContent content = new RequestContent(Input_Postman_String);
            List<string> Expected = content.getHeader();
            List<string> result = new List<string>() { "\nContent-Type", "application/json", "\nUser-Agent",
                "PostmanRuntime/7.26.8", "\nAccept", "\\*/\\*", "\nPostman-Token", "012f42e0-324f-4277-8b42-a71f46e66335", 
                "\nHost", "localhost:8000", "\nAccept-Encoding", "gzip, deflate, br", "\nConnection", "keep-alive",
                "\nContent-Length", "5", "\n", "\ntest1" };

            Assert.AreEqual(Expected, result);

        }
        public void Test6()
        {
            RequestContent content = new RequestContent(Browser_String);
            string Expected = content.getMethod();
            string result = "GET";
            Assert.AreEqual(Expected, result);

        }
        [Test]
        public void Test7()
        {
            RequestContent content = new RequestContent(Browser_String);
            string Expected = content.getPath();
            string result = "/messages";
            Assert.AreEqual(Expected, result);

        }

        [Test]
        public void Test8()
        {
            RequestContent content = new RequestContent(Browser_String);
            string Expected = content.getHttpVersion();
            string result = "HTTP/1.1";
            Assert.AreEqual(Expected, result);

        }

        [Test]
        public void Test9()
        {
            RequestContent content = new RequestContent(Browser_String);
            List<string> Expected = content.getHeader();
            List<string> result = new List<string>() { "\nHost", "localhost:8000" , "\nConnection" , "keep-alive" ,"\nUpgrade-Insecure-Requests" ,
                "1" , "\nUser-Agent" , "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.193 Safari/537.36" ,
                "\nAccept" , "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                "\nSec-Fetch-Site" , "none" ,"\nSec-Fetch-Mode" , "navigate" , "\nSec-Fetch-User", "?1","\nSec-Fetch-Dest" , "document" ,
                 "\nAccept-Encoding", "gzip, deflate, br" , "\nAccept-Language" , "en-US,en;q=0.9,de-AT;q=0.8,de;q=0.7,ar-AE;q=0.6,ar;q=0.5,ru;q=0.4" ,
                "\nCookie", "Phpstorm-7f9060c7=9490fcdc-2e6b-4332-a777-49822bc307c2; Idea-9518ebca=1639b979-bc03-4c26-b340-6378fb67e57b; Pycharm-e3e9aa86=0d78eba3-6cd6-47a7-82db-e05c8fb1c64b; Idea-3edfb054=b536f8cb-adf2-4242-beb2-ac7a7a42bf3e",
                "\n"};

            Assert.AreEqual(Expected, result);

        }

    }
}
