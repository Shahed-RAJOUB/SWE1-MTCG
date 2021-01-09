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
        
        public void addSecondUser(int s, int n)
        {
            string s1 = " UPDATE public.session SET \"secondId\"=@score  WHERE \"sessionId\"= @num ; ";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("score", s);
            cmd.Parameters.AddWithValue("num", n);  // Preventing SQL injection
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
        }
        public void deletLog()
        {
            string s1 = " DELETE FROM public.session WHERE \"secondId\" = 0; ";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
        }
        public string playerInfo(int id)
        {
            string s3 = "SELECT * FROM public.cards where \"deckId\" = @id;";
            sql = @s3;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);  // Preventing SQL injection
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return DataTableToJSONWithJSONNet(dt);
        }

        public string Battlelog(string v , int player)
        {
           
            string log = " No one is connected to enter the Session. Please try later!";


            if ((player % 2) == 0)
            {
                BattleDistr dist = new BattleDistr();
                dist.DbConnection();

                
                // get user id
                string s = " Select \"user-id\" FROM public.users WHERE  username = @username ;";
                sql = @s;
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("username", v);  // Preventing SQL injection 
                Int32 count = (int)cmd.ExecuteScalar();
                int a = (int)count;

                string s1 = "SELECT \"firstId\", \"secondId\", \"sessionId\" FROM public.session;";
                sql = @s1;
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                string sessionList = DataTableToJSONWithJSONNet(dt);


                List<BattleSession> players = JsonConvert.DeserializeObject<List<BattleSession>>(sessionList);

                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].secondId == 0)
                    {
                        //join a session
                        addSecondUser(a, players[i].sessionId);


                        string second = playerInfo(a);
                        // Console.WriteLine(second);
                        string first = playerInfo(players[i].firstId);
                        // Console.WriteLine(first);


                        log = dist.combat(first, second, players[i].sessionId);

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

                string s7 = " select \"sessionId\" from public.session where \"secondId\"= @num;  ";
                sql = @s7;
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("num", 0);  // Preventing SQL injection
                Int32 ch = (int)cmd.ExecuteScalar();

                System.Threading.Thread.Sleep(40000); //wait for 40 seconds

                // check again if you can start start
                if (noOneHere(ch) == 0)
                {
                    deletLog();
                    conn.Close();
                    return log;
                }
                else
                {
                    string s6 = " select \"sessionId\" from public.session where \"firstId\"= @num;  ";
                    sql = @s6;
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("num", a);  // Preventing SQL injection
                    Int32 sc = (int)cmd.ExecuteScalar();

                    string second = playerInfo(noOneHere(sc));
                    string first = playerInfo(noOneHere(a));
                    log = dist.combat(first, second, sc);
                    conn.Close();
                    return log;
                }

            }
            else if ((player % 2) != 0)
            {
                BattleDistr dist = new BattleDistr();
                dist.DbConnection();
                string s = " Select \"user-id\" FROM public.users WHERE  username = @username ;";
                sql = @s;
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("username", v);  // Preventing SQL injection 
                Int32 count = (int)cmd.ExecuteScalar();
                int a = (int)count;
                string s1 = "SELECT \"firstId\", \"secondId\", \"sessionId\" FROM public.session;";
                sql = @s1;
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                string sessionList = DataTableToJSONWithJSONNet(dt);
                List<BattleSession> players = JsonConvert.DeserializeObject<List<BattleSession>>(sessionList);
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].secondId == 0)
                    {
                        //join a session
                        addSecondUser(a, players[i].sessionId);
                        conn.Close();
                        return " you are connected to a second player";

                    }

                }
                return " could not connected to a second player";
            }

            else
            {
                return " There are a session login after 10 second!";
            }
           

        }

        public int noOneHere(int ch)
        {
            
            string s1 = "SELECT \"firstId\", \"secondId\", \"sessionId\" FROM public.session  where \"sessionId\" = @id ;";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", ch);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            string sess = DataTableToJSONWithJSONNet(dt);
            Console.WriteLine(sess);
            List<BattleSession> n = JsonConvert.DeserializeObject<List<BattleSession>>(sess);
            return n[0].secondId;
            
        }

    }
}
