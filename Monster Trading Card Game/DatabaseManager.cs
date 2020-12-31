using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Npgsql;
using Npgsql.Replication.PgOutput.Messages;
using NpgsqlTypes;

namespace Monster_Trading_Card_Game
{
    public class DatabaseManager 
    {
        private string connstring = string.Format("Server={0};Port={1};" +
            "User ID={2};Password={3};Database={4};",
            "localhost",5433,"postgres","if19b166","MTCG");

        private NpgsqlConnection conn;
        private string sql;
        private string sql2;
        private NpgsqlCommand cmd2;
        private string sql1;
        private NpgsqlCommand cmd1;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private DataTable dt1;
        private DataTable dt2;

        public void DbConnection()
        {
           conn = new NpgsqlConnection(connstring);
            conn.Open();
        }

        public void InsertUser(string username, string password)
        {
            int id = maxrow("users") + 1;
            string s = "INSERT INTO public.users VALUES(@id, @id, @id, @id, @password , @username ); ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", username);  // Preventing SQL injection 
            cmd.Parameters.AddWithValue("password", password);
            cmd.Parameters.AddWithValue("id", id);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
        public int maxrow(string table)
        {
            string s = "select count(*) from public.@table ;";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("table", table);
            Int64 count = (Int64)cmd.ExecuteScalar();
            return (int)count; 
            
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

        public void InsertPackage(List<Card> des)
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
                    string s1 = "update public.cards SET  \"deck-id\" = @num  WHERE \"card-id\" = @id ; ";
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
            string s1 = "select * from cards  where \"deck-id\"= @num; ";
            sql1 = @s1;
            cmd1 = new NpgsqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("num", a);  // Preventing SQL injection
            dt2 = new DataTable();
            dt2.Load(cmd1.ExecuteReader());
            conn.Close();
            return DataTableToJSONWithJSONNet(dt2);
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