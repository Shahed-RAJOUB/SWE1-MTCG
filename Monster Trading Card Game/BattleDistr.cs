using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;

namespace Monster_Trading_Card_Game
{
    public class BattleDistr : BaseManager
    {
       
       
        List<Log> roundsLog = new List<Log>();

        private string sqll;
        private NpgsqlCommand cmdd;
        private DataTable dtt;

        public void fight(int r , List<Card> firstC , List<Card> secondC , int id1 , int id2 , int ses)
        {
            roundsLog.Add(new Log() { roundNum = r, winner = Pn(firstC[id1].deckId.ToString()), winnerPoints = scoring(firstC[id1].deckId.ToString()) + 3, loser = Pn(secondC[id2].deckId.ToString()), loserPoints = scoring(secondC[id2].deckId.ToString()) - 5 });
            insertscoring(scoring(firstC[id1].deckId.ToString()) + 3, firstC[id1].deckId.ToString(), ses);
            insertscoring(scoring(secondC[id2].deckId.ToString()) - 5, secondC[id2].deckId.ToString(), ses);
            secondC.RemoveAt(id2);
            firstC.Add(new Card() { id = secondC[id2].id, Name = secondC[id2].Name, element = secondC[id2].element, type = secondC[id2].type, damage = secondC[id2].damage });
        }
        public int scoring(string id)
        {
            
            string s1 = " select score from public.score where \"stat-id\"= @num;  ";
            sqll = @s1;
            cmdd = new NpgsqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("num", int.Parse(id));  // Preventing SQL injection
            Int32 sc = (int)cmdd.ExecuteScalar();
            return sc;
            
        }

        public void insertscoring(int s, string id , int ses)
        {
          
            string s1 = " UPDATE public.score SET score =@score , \"session-id\"=@ses  WHERE \"stat-id\"= @num ; ";
            sqll = @s1;
            cmdd = new NpgsqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("score", s);
            cmdd.Parameters.AddWithValue("ses", ses);
            cmdd.Parameters.AddWithValue("num", int.Parse(id));  // Preventing SQL injection
            dtt = new DataTable();
            dtt.Load(cmdd.ExecuteReader());

        }

        public string Pn(string id)
        {
            
            string s1 = " select username from public.users where \"stat-id\"= @num;  ";
            sqll = @s1;
            cmdd = new NpgsqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("num", int.Parse(id));  // Preventing SQL injection
            string count = (string)cmdd.ExecuteScalar();
            return count;
        }

        public string combat(string first, string second , int ses)
        {

            List<Card> firstC = JsonConvert.DeserializeObject<List<Card>>(first);

            List<Card> secondC = JsonConvert.DeserializeObject<List<Card>>(second);

            int round = 1;
            while (round != 10)
            {
                BattleMethod m = new BattleMethod();
                Random rnd1 = new Random();

                int id1 = rnd1.Next((firstC.Count)-1);
                Random rnd2 = new Random();
                int id2 = rnd2.Next((secondC.Count)-1);

                if (firstC[id1].type == "monster" && secondC[id2].type == "monster")
                {
                    if (m.MXM(firstC[id1].damage, secondC[id2].damage, id1, id2) == id1)
                    {
                        fight(round, firstC, secondC, id1, id2, ses);
                    }
                    else if (m.MXM(firstC[id1].damage, secondC[id2].damage, id1, id2) == id2)
                    {
                        fight(round, secondC , firstC , id2, id1, ses);
                    }
                    else
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = "No One", winnerPoints = 0, loser = "No One", loserPoints = 0 });
                    }


                }
                else if (firstC[id1].type == "Spell" && secondC[id2].type == "Spell")
                {
                    if (m.SXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2) == id1)
                    {
                        fight(round, firstC, secondC, id1, id2, ses);
                    }
                    else if (m.SXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2) == id2)
                    {
                        fight(round, secondC, firstC, id2, id1, ses);
                    }
                    else
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = "No One", winnerPoints = 0, loser = "No One", loserPoints = 0 });
                    }
                }
                else
                {
                    if (m.MXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2) == id1)
                    {
                        fight(round, firstC, secondC, id1, id2, ses);
                    }
                    else if (m.MXS(firstC[id1].damage, firstC[id1].element, secondC[id2].damage, secondC[id2].element, id1, id2) == id2)
                    {
                        fight(round, secondC, firstC, id2, id1, ses);
                    }
                    else
                    {
                        roundsLog.Add(new Log() { roundNum = round, winner = "No One", winnerPoints = 0, loser = "No One", loserPoints = 0 });
                    }

                }
                if (firstC.Count == 1 || secondC.Count == 1) { break; }
                round++;
            }
            string json = JsonConvert.SerializeObject(roundsLog, Formatting.Indented);
            return json;
        }

    }
}
