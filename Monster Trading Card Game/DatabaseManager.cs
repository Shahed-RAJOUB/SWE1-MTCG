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
        private NpgsqlCommand cmd;
        private DataTable dt;

        public void DbConnection()
        {
           conn = new NpgsqlConnection(connstring);
            conn.Open();
        }

        public void InsertUser(string username, string password)
        {
            string s = "INSERT INTO public.users VALUES(1, 1, 1, 1, '"+password+"' , '"+username+"' ); ";
            sql = @s;
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

        }
    }

}