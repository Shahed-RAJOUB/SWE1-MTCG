using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Npgsql;

namespace Monster_Trading_Card_Game
{
   public  class BattleManager : BaseManager
    {
    
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
     
        public string playerInfo(int id)
        {
            string s3 = "SELECT * FROM public.cards where \"deck-id\" = @id;";
            sql = @s3;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);  // Preventing SQL injection
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return DataTableToJSONWithJSONNet(dt);
        }

        public string Battlelog(string v)
        {
            BattleSession ses = new BattleSession();
            string log = "No one is connected to enter the Session. Please try later!";
            // get user id
            string s = " Select \"user-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", v);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;

            string s1 = "SELECT \"first-id\", \"second-id\", \"session-id\" FROM public.session;";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            string sessionList = DataTableToJSONWithJSONNet(dt);
            Console.WriteLine(sessionList);
            List<BattleSession> players = JsonConvert.DeserializeObject<List<BattleSession>>(sessionList); // to do change the battlee session to have only parameter so it works
            
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].secondId == 0)
                {
                    //join a session

                    string second = playerInfo(a);
                    Console.WriteLine(second);
                    string first = playerInfo(players[i].firstId);
                    Console.WriteLine(second);


                    log = ses.combat(first, second);

                    conn.Close();
                    return log;

                }

            }

            string s5 = "INSERT INTO public.session VALUES( @first); ";
            sql = @s5;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("first", a);  // Preventing SQL injection 
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            
            System.Threading.Thread.Sleep(5000); //wait for 5 seconds
             
            // check again if you can start start
            if( noOneHere(a) == "0")
            {
                conn.Close();
                return log;
            }
            else 
            {
                log = ses.combat(a.ToString(), noOneHere(a));
                conn.Close();
                return log;
            }
           

        }

        public string noOneHere(int a)
        {
            string s1 = "SELECT \"first-id\", \"second-id\"  FROM public.session  where \"session-id\" = @id ;";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", a);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            string sess = DataTableToJSONWithJSONNet(dt);
             BattleSession b = JsonConvert.DeserializeObject<BattleSession>(sess);
            return b.secondId.ToString();
            
        }
    }
}
