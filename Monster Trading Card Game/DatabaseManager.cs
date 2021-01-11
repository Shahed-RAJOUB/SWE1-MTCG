using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace Monster_Trading_Card_Game
{
    public class DatabaseManager : BaseManager
    {
        private string sql;
        private string sql2;
        private NpgsqlCommand cmd2;
        private string sql1;
        private NpgsqlCommand cmd1;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private DataTable dt1;
        private DataTable dt2;
        public void InsertUser(string username, string password)
        {
            string s = "INSERT INTO public.users VALUES( @password , @username ); ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", username);  // Preventing SQL injection 
            cmd.Parameters.AddWithValue("password", password);
           
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

        }
        public int maxnum(string table , string num)
        {
            string s = "select max( \"package-id\" ) from public.packages ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("num", num);
            cmd.Parameters.AddWithValue("table", table);
            Int32 count = (int)cmd.ExecuteScalar();
            return (int)count;

        }
        public string ShowUser(string name)
        {
            string s = "SELECT * FROM public.users inner join public.stack on ( users.\"stack-id\" = stack.\"stack-id\")  where username= @username; ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", name);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            return DataTableToJSONWithJSONNet(dt);

        }

        public bool CheckUser(string username, string password)
        {

            string s = "SELECT * FROM public.users  where username= @username  AND password= @password ; ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", username);  // Preventing SQL injection 
            cmd.Parameters.AddWithValue("password", password);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            string ch = DataTableToJSONWithJSONNet(dt).ToString();
            
            if ( ch == "[]")
            { return false; }
            else { return true; }
            
        }

        public void InsertPackage(List<Card> des) // TODO injection prevention
        {
            int m = maxnum("packages", "package-id") +1;
            string s = "INSERT INTO public.cards VALUES('"+des[0].id+ "' , '" + des[0].Name + "' , '" + des[0].element + "' , '" + des[0].type + "' , " + des[0].damage + ", 1 , " + m + ") , ('" + des[1].id + "' , '" + des[1].Name + "' , '" + des[1].element + "' , '" + des[1].type + "' , " + des[0].damage + ", 1 ," + m + ") , ('" + des[2].id + "' , '" + des[2].Name + "' , '" + des[2].element + "' , '" + des[2].type + "', " + des[0].damage + " , 1 , " + m + ") , ('" + des[3].id + "' , '" + des[3].Name + "' , '" + des[3].element + "' , '" + des[3].type + "', " + des[0].damage + ", 1 , " + m + ") , ('" + des[4].id + "' , '" + des[4].Name + "' , '" + des[4].element + "' , '" + des[4].type + "' , " + des[0].damage + ", 1 , " + m + "); ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            string s1 = "INSERT INTO public.packages VALUES(" + m + " , '" + des[0].id + "') , (" + m + " , '" + des[1].id + "') , (" + m + " , '" + des[2].id + "'), (" + m + " , '" + des[3].id + "') , (" + m + " , '" + des[4].id + "'); ";
            sql1 = @s1;
            cmd1 = new NpgsqlCommand(sql1, conn);
            dt1 = new DataTable();
            dt1.Load(cmd1.ExecuteReader());
            conn.Close();
        }

        public bool updatecardsDeck(string v, List<Card> des)
        {
            string s = "Select \"deck-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", v);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;
            if(des.Count == 4) 
            {
                for (int i = 0; i < 4; i++)
                {
                    string s1 = "update public.cards SET  \"deckId\" = @num  WHERE \"card-id\" = @id ; ";
                    sql1 = @s1;
                    cmd1 = new NpgsqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("num", a);  // Preventing SQL injection
                    cmd1.Parameters.AddWithValue("id", des[i].id);
                    dt2 = new DataTable();
                    dt2.Load(cmd1.ExecuteReader());
                }
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
           
        }

        public string getstat(string v)
        {
            string s = "Select \"stat-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", v);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;

            string s1 = " select * from public.score where \"stat-id\"= @num;  ";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("num", a);  // Preventing SQL injection
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            return DataTableToJSONWithJSONNet(dt);
        }

        public string getcardstrade(string v)
        {
            string s = " select * from public.trading ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            return DataTableToJSONWithJSONNet(dt);
        }




        public void createDeal(string user , string card_id)
        {
            string s = " Select \"user-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", user);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;
        
            string s1 = "INSERT INTO public.trading ( \"card-id\", \"user-id\") VALUES (@cardId, @userId)";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("cardId", card_id);
            cmd.Parameters.AddWithValue("userId", a);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
        }

        public string getboard()
        {

            string s = " select * from public.score ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            return DataTableToJSONWithJSONNet(dt);
        }

        public string getcardsDeck(string v)
        {
            string s = "Select \"deck-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", v);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;
            string s1 = "select * from cards  where \"deckId\"= @num; ";
            sql1 = @s1;
            cmd1 = new NpgsqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("num", a);  // Preventing SQL injection
            dt2 = new DataTable();
            dt2.Load(cmd1.ExecuteReader());
            conn.Close();
            return DataTableToJSONWithJSONNet(dt2);
        }

        public void deletedeal(string info)
        {
           
            string s1 = "DELETE FROM public.trading WHERE \"trade-id\"= @id ;  ";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            int id = int.Parse(info);
            cmd.Parameters.AddWithValue("id", id);  // Preventing SQL injection 
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
        }

        public bool buyCard(string user ,string card_id, string info)     // to do / no one can buy before creating a trade
        {
            string s = " Select \"user-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", user);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;
            string s1 = "Select \"trade-id\" FROM public.trading WHERE  \"user-id\" = @userid ;";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("userid", a);  // Preventing SQL injection 
             
            Int32 count1 = (int)cmd.ExecuteScalar();
            int a1 = count1;
            int tradeid = int.Parse(info);
            if( a1 == tradeid)
            {
                conn.Close();
                return false;
            }
            else 
            {
                // find the card id in this deal
                string s2 = "Select \"card-id\" FROM public.trading WHERE  \"trade-id\" = @tradeId ;";
                sql = @s2;
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tradeId", tradeid);  // Preventing SQL injection 
                string c = (string)cmd.ExecuteScalar();
               
                // update the deal and enter your card
                string s3 = "UPDATE public.trading SET \"card-id\" = @num  WHERE \"trade-id\" = @tradeId  ; ";
                sql1 = @s3;
                cmd1 = new NpgsqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("num", card_id); // stack id is equal to user id
                cmd1.Parameters.AddWithValue("tradeId", tradeid);
                dt = new DataTable();
                dt.Load(cmd1.ExecuteReader());

                // get package id of the old card to add the new card to it
                string s5 = "Select \"package-id\" FROM public.cards WHERE  \"card-id\" = @cardId  ;";
                sql = @s5;
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("cardId", card_id );  // Preventing SQL injection 
                Int32 count2 = (int)cmd.ExecuteScalar();
                int a2 = (int)count2;


                //update your packages and add the new card
                string s4 = "UPDATE public.packages SET \"card-id\" = @num  WHERE \"package-id\" = @id ;";
                sql1 = @s4;
                cmd1 = new NpgsqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("num", c); // stack id is equal to user id
                cmd1.Parameters.AddWithValue("id", a2);
                dt = new DataTable();
                dt.Load(cmd1.ExecuteReader());

                conn.Close();
                return true;
            }
        }

        public string getcardsStack(string v)
        {
            string s = "Select \"stack-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", v);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;
            string s1 = "select * from cards inner join stack on (cards.\"package-id\"= stack.\"package-id\") where \"stack-id\"= @num; ";
            sql1 = @s1;
            cmd1 = new NpgsqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("num", a);  // Preventing SQL injection
            dt = new DataTable();
            dt.Load(cmd1.ExecuteReader());
            conn.Close();
            return DataTableToJSONWithJSONNet(dt);
        }

        public void updateUser(string username, string password)
        {
            string s = "UPDATE public.users SET  password = @password  WHERE username = @username ; ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", username);  // Preventing SQL injection 
            cmd.Parameters.AddWithValue("password", password);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
        }
        public bool addpackageToUser( string name)
        {
            string s = "Select \"stack-id\" FROM public.users WHERE  username = @username ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", name);  // Preventing SQL injection 
            Int32 count = (int)cmd.ExecuteScalar();
            int a = (int)count;
            Console.WriteLine(a);
            int id = maxnum("packages" , "package-id");
            Console.WriteLine(id);
            if (id != 0) {
            string s1 = "INSERT INTO public.stack VALUES('new package', @id , @a ); ";
                sql1 = @s1;
                cmd1 = new NpgsqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("id", id);
                cmd1.Parameters.AddWithValue("a", a);  // Preventing SQL injection
               
               
                dt = new DataTable();
                dt.Load(cmd1.ExecuteReader());
            string s2 = "DELETE FROM public.packages WHERE \"package-id\"= @id ;  ";
                sql2 = @s2;
                cmd2 = new NpgsqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("id", id);
                dt = new DataTable();
                dt.Load(cmd2.ExecuteReader());
                conn.Close();
                return true;
                
            }
           else {
                conn.Close();
                return false;
                
            }
            
           

        }



    }

}