﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace Monster_Trading_Card_Game
{
    public class EndPointsResponse
    {

        
        string command = "";
        string info;
        string[] substrings;
        public string getCommand() { return command; }
        public string getInfo() { return info; }


        public EndPointsResponse() { }

        public Response Methodhandler(string method, string path, string Msg , string Auth , int player)
        {
            getCommandandInfo(path);
            PlayerManager user = new PlayerManager();
            // configure a player
            switch (command)
            {
                case "users":

                    {
                        if (method == "GET")
                        {
                            if (substrings.Length == 3) { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                            else { return user.ShowProfilebyName(info); }
                        }
                        else if (method == "POST")
                        {
                            
                            if (substrings.Length == 3) { return user.RegisterUser(Msg); }
                            else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                        }
                        else if (method == "PUT")
                        {
                            
                            if (substrings.Length == 5) { return user.UpdateUser(info, Msg , Auth); }
                            else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                        }
                        else
                        {
                            return new Response { status = HttpStatus.Method_Not_Allowed };
                        }

                    }


                case "sessions":
                    {
                        if (method == "POST")
                        {
                            
                            if (substrings.Length == 3) { return user.LoginUser(Msg); }
                            else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                        }
                        else
                        {
                            return new Response { status = HttpStatus.Method_Not_Allowed };
                        }

                    }
                case "packages":
                    if (method == "POST" && Auth== "Bearer admin-mtcgToken")
                    {
                        
                        if (substrings.Length == 3) { return user.AddPackage(Msg); } // if user is admin
                        else if(substrings.Length == 5) { return user.AddRandomPackage(); } // if user is admin
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed , content = " Only admin cann add packages." };
                    }
                case "transactions":
                    if (method == "POST" && info == "packages")
                    {
                       
                        if (substrings.Length == 5) { return user.AcquirePackage(  Auth , Msg ); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed };
                    }
                case "cards":
                    if (method == "GET")
                    {
                        
                        if (substrings.Length == 3) { return user.ShowtCards(Auth); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed };
                    }
                case "deck":
                    if (method == "GET")
                    {
                        
                        if (substrings.Length == 3) { return user.ShowDeck(Auth); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else if (method == "PUT")
                    {
                       
                        if (substrings.Length == 3) { return user.UpdateDeck(Auth , Msg); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed };
                    }
                case "stats":
                    if (method == "GET")
                    {
                       
                        if (substrings.Length == 3) { return user.ShowStats(Auth); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed };
                    }
                case "score":
                    if (method == "GET")
                    {
                        
                        if (substrings.Length == 3) { return user.ShowBoard(Auth); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed };
                    }
                case "battles":
                    if (method == "POST")
                    {
                      
                        if (substrings.Length == 3) { return user.StartBattle(Auth , player); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed };
                    }
                case "tradings":
                    if (method == "GET")
                    {
                        
                        if (substrings.Length == 3) { return user.ShowtradingCards(Auth); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    if (method == "POST")
                    {
                        
                        if (substrings.Length == 3) { return user.TradeCard(Auth , Msg); }
                        else if (substrings.Length == 5) { return user.TakeCard(Auth , info, Msg); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    if (method == "DELETE")
                    {
                        
                        if (substrings.Length == 5) { return user.DeleteCard(Auth , info); }
                        else { return new Response { status = HttpStatus.Method_Not_Allowed }; }
                    }
                    else
                    {
                        return new Response { status = HttpStatus.Method_Not_Allowed };
                    }




                default:
                    return wrongCommand();

            }

        }

        public void getCommandandInfo(string path)
        {
            string pattern = "(/)";
            substrings = Regex.Split(path, pattern);// Split on hyphens
                                                    //Console.WriteLine(substrings.Length);
                                                    //foreach (string match in substrings){Console.WriteLine("'{0}'", match);}
            if (substrings.Length == 3)
            {
                command = substrings[2];
                info = "";
            }
            else if (substrings.Length == 5)
            {
                command = substrings[2];
                info = substrings[4];
            }
            else
            {
                command = "";
                info = "";
            }
        }

        private Response wrongCommand()
        {
            return new Response { status = HttpStatus.Not_Found };
        }



    }
}
