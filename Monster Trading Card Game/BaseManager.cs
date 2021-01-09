using System.Data;
using Newtonsoft.Json;
using Npgsql;


namespace Monster_Trading_Card_Game
{
   public class BaseManager
    {
        

        public string connstring = string.Format("Server={0};Port={1};" +
         "User ID={2};Password={3};Database={4};",
         "localhost", 5433, "postgres", "if19b166", "MTCG");
        public NpgsqlConnection conn;
        public void DbConnection()
        {
            conn = new NpgsqlConnection(connstring);
            conn.Open();
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}
