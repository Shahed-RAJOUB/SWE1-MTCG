using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace REST_HTTP_based_plain_text_Webservices
{
    public class Program
    {


        public static void Main()
        {
            var server = new Server();
            server.Connection();
        }
    }
}