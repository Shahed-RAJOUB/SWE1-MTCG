using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;

namespace Monster_Trading_Card_Game
{
   public class BattleSession : BaseManager
    {
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;

        List<Log> roundsLog = new List<Log>();
        public int sessionId { get; set;}
        public int firstId { get; set;}
        public int secondId { get; set;}

        public float scoring(string id)
        {
            string s1 = " select score from public.score where \"stat-id\"= @num;  ";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("num", id);  // Preventing SQL injection
            float sc = (float)cmd.ExecuteScalar();
            return sc;
        }

        public void insertscoring(float s , string id)
        {
            string s1 = " UPDATE public.score SET score = @score  WHERE \"stat-id\" = @id ; ";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("score", s);
            cmd.Parameters.AddWithValue("num", id);  // Preventing SQL injection
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

        }

        public string Pn (string id)
        {
            string s1 = " select username from public.users where \"stat-id\"= @num;  ";
            sql = @s1;
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("num", id);  // Preventing SQL injection
            string count = (string)cmd.ExecuteScalar();
            return count;
        }

        public string combat(string first, string second)
        {

            List<Card> firstC = JsonConvert.DeserializeObject<List<Card>>(first);
           
            List<Card> secondC = JsonConvert.DeserializeObject<List<Card>>(second);
            
            int round = 1;
            while(round != 100) { 
                BattleMethod m = new BattleMethod();
                Random rnd1 = new Random();

                int id1 = rnd1.Next(firstC.Count);
                Random rnd2 = new Random();
                int id2 = rnd2.Next(secondC.Count);
               
                if(firstC[id1].type == "monster" && secondC[id2].type == "monster")
                {
                   if( m.MXM(firstC[id1].damage, secondC[id2].damage , id1 , id2) == id1)
                    {
                        roundsLog.Add(new Log() { roundNum = round , winner = Pn(id1.ToString()), winnerPoints = scoring(id1.ToString())+3 , loser = Pn(id2.ToString()), loserPoints = scoring(id2.ToString())-5 });
                        insertscoring(scoring(id1.ToString()) + 3, id1.ToString());
                        insertscoring(scoring(id2.ToString()) - 5 , id2.ToString());
                        secondC.RemoveAt(id2);
                        firstC.Add(new Card () { id = secondC[id2].id, Name = secondC[id2].Name, element = secondC[id2].element, type = secondC[id2].type, damage = secondC[id2].damage });
                    }
                    else if (m.MXM(firstC[id1].damage, secondC[id2].damage, id1, id2) == id2)
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = Pn(id2.ToString()), winnerPoints = scoring(id2.ToString())+3, loser = Pn(id1.ToString()), loserPoints = scoring(id1.ToString())-5 });
                        insertscoring(scoring(id1.ToString()) + 3, id1.ToString());
                        insertscoring(scoring(id2.ToString()) - 5, id2.ToString());
                        firstC.RemoveAt(id1);
                        secondC.Add(new Card () { id = firstC[id1].id, Name = firstC[id1].Name, element = firstC[id1].element, type = firstC[id1].type, damage = firstC[id1].damage });
                    }
                    else
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = "No One", winnerPoints = 0 , loser = "No One", loserPoints = 0 });
                    }


                }
                else if(firstC[id1].type == "Spell" && secondC[id2].type == "Spell")
                {
                    if (m.SXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2) == id1)
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = Pn(id1.ToString()), winnerPoints = scoring(id1.ToString())+3, loser = Pn(id2.ToString()), loserPoints = scoring(id2.ToString())-5});
                        insertscoring(scoring(id1.ToString()) + 3, id1.ToString());
                        insertscoring(scoring(id2.ToString()) - 5, id2.ToString());
                        secondC.RemoveAt(id2);
                        firstC.Add(new Card() { id = secondC[id2].id, Name = secondC[id2].Name, element = secondC[id2].element, type = secondC[id2].type, damage = secondC[id2].damage });
                    }
                    else if (m.SXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2) == id2)
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = Pn(id2.ToString()), winnerPoints = scoring(id2.ToString())+3, loser = Pn(id1.ToString()), loserPoints = scoring(id1.ToString())-5 });
                        insertscoring(scoring(id1.ToString()) + 3, id1.ToString());
                        insertscoring(scoring(id2.ToString()) - 5, id2.ToString());
                        firstC.RemoveAt(id1);
                        secondC.Add(new Card() { id = firstC[id1].id, Name = firstC[id1].Name, element = firstC[id1].element, type = firstC[id1].type, damage = firstC[id1].damage });
                    }
                    else
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = "No One", winnerPoints = 0, loser = "No One", loserPoints = 0 });
                    }
                }
                else
                {
                    if(m.MXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2)==id1)
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = Pn(id1.ToString()), winnerPoints = scoring(id1.ToString()), loser = Pn(id2.ToString()), loserPoints = scoring(id2.ToString()) });
                        insertscoring(scoring(id1.ToString()) + 3, id1.ToString());
                        insertscoring(scoring(id2.ToString()) - 5, id2.ToString());
                        secondC.RemoveAt(id2);
                        firstC.Add(new Card() { id = secondC[id2].id, Name = secondC[id2].Name, element = secondC[id2].element, type = secondC[id2].type, damage = secondC[id2].damage });
                    }
                    else if (m.MXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2) == id2)
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = Pn(id2.ToString()), winnerPoints = scoring(id2.ToString()), loser = Pn(id1.ToString()), loserPoints = scoring(id1.ToString()) });
                        insertscoring(scoring(id1.ToString()) + 3, id1.ToString());
                        insertscoring(scoring(id2.ToString()) - 5, id2.ToString());
                        firstC.RemoveAt(id1);
                        secondC.Add(new Card() { id = firstC[id1].id, Name = firstC[id1].Name, element = firstC[id1].element, type = firstC[id1].type, damage = firstC[id1].damage });
                    }
                    else
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = "No One", winnerPoints = 0, loser = "No One", loserPoints = 0 });
                    }

                }
                if (firstC.Count == 0 || secondC.Count == 0) { break; }
                round++;
            }
            string json = JsonConvert.SerializeObject(roundsLog, Formatting.Indented);
            return json;
        }
    }
}
