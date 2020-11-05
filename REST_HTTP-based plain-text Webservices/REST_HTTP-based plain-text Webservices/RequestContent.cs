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
        private string Header;
        private Dictionary<string, string> KeyValues = new Dictionary<string, string>();

        public RequestContent() { }//default empty constructor
        public RequestContent(string data)
        {

        }

        internal void Requesthandler()
        {
            throw new NotImplementedException();
        }
    }
}
