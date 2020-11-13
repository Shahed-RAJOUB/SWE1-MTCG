using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Moq;

namespace REST_HTTP_based_plain_text_Webservices
{
    public class RequestContent
    {
        private string Method{ get; }
        private string Path{ get; }
        private string HttpVersion{ get; }
        private string Bodymessage{ get; }
        private List<string> KeysandValues = new List<string>();// Header
        
        // i need to instance the object here one time outside the function to save everything in on Responce object

        EndPointsResponse EndPoints = new EndPointsResponse(); 

        public RequestContent(string data)
        {
            if (data!="")
            {
                string[] delimiterChars = { " /", " H", ": ", "\r" };
                string[] words = data.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                Method = words[0];
                Path = "/" + words[1];
                HttpVersion = "H"+ words[2];
                int i = 3;
                while (i < words.Length)
                {
                    KeysandValues.Add(words[i]);
                    i++;
                }
                Bodymessage = words[words.Length - 1];
            }
            else { Console.WriteLine("Server recived an empty path!"); }
        }
        public string getMethod() { return Method; }
        public string getPath() { return Path; }
        public string getHttpVersion() { return HttpVersion; }
        public List<string> getHeader() { return KeysandValues; }
        
        public void Requesthandler(NetworkStream stream)
        {
          
            Console.WriteLine("==============>>>> HTTP-REQUEST KEY and Values <<<<==============\n");
            Console.WriteLine("METHOD : " + Method + "\n");
            Console.WriteLine("PATH : " + Path + "\n");
            Console.WriteLine("HTTP_VERSION : " + HttpVersion + "\n");
            Console.WriteLine("MESSAGE_BODY : " + Bodymessage + "\n");
            Console.WriteLine("HEADER_KEYS AND VALUES :===>>>> \n");
            for( int i = 0; i < (KeysandValues.Count)-2; i++)
            {
                Console.WriteLine(KeysandValues[i] + " : " + KeysandValues[++i]);
                
            }
            Console.WriteLine("==============>>>> Endpoints Response messages according to the Path <<<<==============\n");
           
            EndPoints.Methodhandler(Method, Path , Bodymessage , stream);

        }
    }
}
