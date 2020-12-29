using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Monster_Trading_Card_Game
{
    public class PlayerManager
    {
        DatabaseManager dataBase = new DatabaseManager();


        public Response ShowProfilebyName(string info)
        {
            dataBase.DbConnection();
            throw new NotImplementedException();
        }

        public Response RegisterUser(string msg)
        {
            User user = JsonConvert.DeserializeObject<User>(msg);
            dataBase.DbConnection();
            dataBase.InsertUser(user.Username, user.Password);
            if (true)
            {
                
                return new Response { status = HttpStatus.Ok, content = " Message deleted at this id." };
            }
            else
            {
                return new Response
                {
                    status = HttpStatus.Method_Not_Allowed
                };
            }
        }

        public Response LoginUser(string msg)
        {
            User user = JsonConvert.DeserializeObject<dynamic>(msg);

            throw new NotImplementedException();
        }

        public Response AddPackage(string msg)
        {
            throw new NotImplementedException();
        }

        public Response UpdateUser(string msg, string msg1)
        {
            User user = JsonConvert.DeserializeObject<dynamic>(msg);
            throw new NotImplementedException();
        }

        public Response AcquirePackage()
        {
            throw new NotImplementedException();
        }

        public Response ShowtradingCards()
        {
            throw new NotImplementedException();
        }

        public Response ShowDeck()
        {
            throw new NotImplementedException();
        }

        public Response UpdateDeck(string msg)
        {
            throw new NotImplementedException();
        }

        public Response ShowBoard()
        {
            throw new NotImplementedException();
        }

        public Response ShowStats()
        {
            throw new NotImplementedException();
        }

        public Response StartBattle()
        {
            throw new NotImplementedException();
        }

        public Response TradeCard(string msg)
        {
            throw new NotImplementedException();
        }

        public Response TakeCard(string info, string msg)
        {
            throw new NotImplementedException();
        }

        public Response DeleteCard(string info)
        {
            throw new NotImplementedException();
        }
    }
}