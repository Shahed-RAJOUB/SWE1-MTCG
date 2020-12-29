using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;


namespace Monster_Trading_Card_Game
{
    public class RequestContent
    {
        private string Method{ get; }
        private string Path{ get; }
        private string HttpVersion{ get; }
        public string Auth { get; private set; }
        private string Bodymessage{ get; }
        private List<string> KeysandValues = new List<string>();// Header
        
        // i need to instance the object here one time outside the function to save everything in on Responce object

        

        public RequestContent(string data)
        {
            if (data!="")
            {
                string[] delimiterChars = { " /", " H", ": ", "\r" };
                string[] words = data.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                Method = words[0];
                Path = "/" + words[1];
                HttpVersion = "H"+ words[2];
                Auth = words[4];
                int i = 3;
                while (i < words.Length)
                {
                    KeysandValues.Add(words[i]);
                    i++;
                }
                int j = 0;
                string[] jsonS = data.Split("\r\n", System.StringSplitOptions.RemoveEmptyEntries);
                for (i = 0; i < jsonS.Length; i++)
                {
                    if (jsonS[i] == "{")
                    {
                        j = i;
                        Bodymessage = "";
                    }

                }
                for (i = j; i < jsonS.Length; i++)
                {
                    Bodymessage = Bodymessage + jsonS[i];

                }
            }
            else { Console.WriteLine("Server recived an empty path!"); }
        }
        public string getMethod() { return Method; }
        public string getPath() { return Path; }
        public string getHttpVersion() { return HttpVersion; }
        public string getMsg() { return Bodymessage; }
        public List<string> getHeader() { return KeysandValues; }
        
        public void Requesthandler()
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
           
            

        }
    }
}
