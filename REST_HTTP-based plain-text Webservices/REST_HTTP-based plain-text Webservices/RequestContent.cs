using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace REST_HTTP_based_plain_text_Webservices
{
   public class RequestContent
    {
        private string Method;
        private string Path;
        private string HttpVersion;
        private string Bodymessage;
        List<string> KeysandValues = new List<string>(); // Header
        EndPointsResponse endPoints = new EndPointsResponse(); // i need to instance the object here one time outside the function to save everything in on Responce object


        public RequestContent(string data)
        {
            string [] delimiterChars = { " /", " H", ": ", "\r"  };
            string[] words = data.Split(delimiterChars , System.StringSplitOptions.RemoveEmptyEntries);
            Method = words[0]; 
            Path = words[1];
            HttpVersion = words[2];
            int i = 3;
          while( i < words.Length)
            {              
                KeysandValues.Add(words[i]);
                i++;
            }
           Bodymessage = words[words.Length - 1];
        }

        public void Requesthandler(NetworkStream stream)
        {
          
            Console.WriteLine("==============>>>> HTTP-REQUEST KEY and Values <<<<==============\n");
            Console.WriteLine("METHOD : " + Method + "\n");
            Path = "/" + Path;
            Console.WriteLine("PATH : " + Path + "\n");
            HttpVersion = "H" + HttpVersion;
            Console.WriteLine("HTTP_VERSION : " + HttpVersion + "\n");
            Console.WriteLine("MESSAGE_BODY : " + Bodymessage + "\n");
            Console.WriteLine("HEADER_KEYS AND VALUES :===>>>> \n");
            for( int i = 0; i < (KeysandValues.Count)-2; i++)
            {
                Console.WriteLine(KeysandValues[i] + " : " + KeysandValues[++i]);
                
            }
            Console.WriteLine("==============>>>> Endpoints Response messages according to the Path <<<<==============\n");
           
            endPoints.Methodhandler(Method, Path , Bodymessage , stream);

        }
    }
}
