using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Monster_Trading_Card_Game
{
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        

    }
}
/*public void DiserializeMsg(string Msg)
{
    try
    {
        var jInpunt = JsonConvert.DeserializeObject<dynamic>(Msg);
    }
    catch (Exception exc)
    {
        Console.WriteLine("This Exception is caught:" + exc);
    }
}*/