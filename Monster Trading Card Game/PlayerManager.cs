using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monster_Trading_Card_Game
{
    public class PlayerManager
    {
        DatabaseManager dataBase = new DatabaseManager();
        BattleManager session = new BattleManager();


        public Response ShowProfilebyName(string info)
        {

            dataBase.DbConnection();
            return new Response { status = HttpStatus.Ok, content = dataBase.ShowUser(info) };
        }

        public Response RegisterUser(string msg)
        {
            User user = JsonConvert.DeserializeObject<User>(msg);
            dataBase.DbConnection();
            dataBase.InsertUser(user.Username, user.Password);
            return new Response { status = HttpStatus.Ok, content = " User is registered." };

        }

        public Response LoginUser(string msg)
        {

            User user = JsonConvert.DeserializeObject<User>(msg);
            dataBase.DbConnection();
            if (dataBase.CheckUser(user.Username, user.Password) == true)
            {
                return new Response { status = HttpStatus.Ok, content = " User has logged in." };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not registered." }; }

        }

        public Response AddPackage(string msg)
        {

            List<Card> des = JsonConvert.DeserializeObject<List<Card>>(msg);

            dataBase.DbConnection();
            dataBase.InsertPackage(des);

            return new Response { status = HttpStatus.Ok, content = " Package added." };

        }

        public Response UpdateUser(string msg, string msg1, string Auth)
        {
            User user = JsonConvert.DeserializeObject<User>(msg1);
            dataBase.DbConnection();
            if (Auth == "Bearer " + msg + "-mtcgToken")
            {
                dataBase.updateUser(user.Username, user.Password);
                return new Response { status = HttpStatus.Ok, content = " password is updated." };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }


        }
        int i = 20;
        public Response AcquirePackage(string Auth, string msg)
        {

            dataBase.DbConnection();
            if ((Auth != "") && i != 0)
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                if(dataBase.addpackageToUser(jsonS[1]) == true) { 
                

                i -= 5;
                return new Response { status = HttpStatus.Ok, content = " Stack is updated und you still have " + i + " coins." };
                }else
                { return new Response { status = HttpStatus.Bad_Request, content = " You cannot get any more packages - Nothing in Inventory :(" }; }


            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " You cannot get any more packages - you have  no coins or you need to login" }; }
        }

        public Response ShowtCards(string Auth)
        {
            dataBase.DbConnection();
            if (Auth != "")
                {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                return new Response { status = HttpStatus.Ok, content = dataBase.getcardsStack(jsonS[1]) };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }

        }

        public Response ShowDeck(string Auth)
        {
            dataBase.DbConnection();
            if (Auth != "")
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                return new Response { status = HttpStatus.Ok, content = dataBase.getcardsDeck(jsonS[1]) };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }

        public Response UpdateDeck(string Auth , string msg)
        {
            List<Card> des = JsonConvert.DeserializeObject<List<Card>>(msg);
            dataBase.DbConnection();
            if (Auth != "")
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                if (dataBase.updatecardsDeck(jsonS[1], des) == true)
                {
                    return new Response { status = HttpStatus.Ok, content = " Deck is updated." };
                }
                else { return new Response { status = HttpStatus.Bad_Request, content = " Not enough cards." }; }
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }

        public Response ShowBoard( string Auth)
        {
            dataBase.DbConnection();
            if (Auth != "")
            {
                return new Response { status = HttpStatus.Ok, content = dataBase.getboard() };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }

        public Response ShowStats(string Auth)
        {
            dataBase.DbConnection();
            if (Auth != "")
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                return new Response { status = HttpStatus.Ok, content = dataBase.getstat(jsonS[1]) };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }

       public Response StartBattle(string Auth) 
        {
            session.DbConnection();
            if (Auth != "")
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                return new Response { status = HttpStatus.Ok, content = session.Battlelog(jsonS[1]) };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }

        public Response ShowtradingCards(string auth)
        {
            dataBase.DbConnection();
            if (auth != "")
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                return new Response { status = HttpStatus.Ok, content = dataBase.getcardstrade(jsonS[1]) };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }

        public Response TradeCard(string Auth , string msg)
        {
            Deal deal = JsonConvert.DeserializeObject<Deal>(msg);
            dataBase.DbConnection();
            if (Auth != "")
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                dataBase.createDeal( jsonS[1] , deal.cardId);
                return new Response { status = HttpStatus.Ok, content = " You craeted a trading deal." };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }

        public Response TakeCard(string Auth, string info, string msg)
        {
            Deal deal = JsonConvert.DeserializeObject<Deal>(msg);
            dataBase.DbConnection();
            if (Auth != "") 
            {
                string[] delimiterChars = { " ", "-" };
                string[] jsonS = Auth.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                if (dataBase.buyCard(jsonS[1], deal.cardId, info) == true) { return new Response { status = HttpStatus.Ok, content = " You'v just bought a card!." }; }
                else { return new Response { status = HttpStatus.Bad_Request, content = " you can#t by your own card " }; }
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }
        public Response DeleteCard(string Auth,  string info)
        {
            
            dataBase.DbConnection();
            if (Auth !="")
            {
                dataBase.deletedeal(info);
                return new Response { status = HttpStatus.Ok, content = " You deleted a trading deal." };
            }
            else { return new Response { status = HttpStatus.Bad_Request, content = " User not Autherized." }; }
        }
    }
}