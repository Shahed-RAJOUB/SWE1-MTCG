using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace REST_HTTP_based_plain_text_Webservices
{
    public class EndPointsResponse
    {

        public static List<string> Messages = new List<string>();
        public static int check = 1;
        string command = "";
        int id;
        string[] substrings;
        bool check1 = true;
        bool check2 = true;
        bool check3 = true;

        public EndPointsResponse(){}

        public Response Methodhandler(string method , string path , string Msg )
        {
            getCommandandID(path);


            if (check1 == false) { 
                    Console.WriteLine(" Invalid Request Method!");
                return new Response { status = HttpStatus.Not_Acceptable };
            }



            if (check1 == false)
            {
                
                Console.WriteLine(" You Entered extra values , please enter an autherized format!");
                return new Response { status = HttpStatus.Not_Acceptable };

            }

            if (command == "messages")
            {
                if (method == "GET")
                {
                    if (substrings.Length == 3) { return ListAllMessages(); }
                    else { return ShowMessagebbId(id); }
                }
                else if (method == "POST")
                {
                    check = 0;
                    if (substrings.Length == 3) { return AddMessage(Msg); }
                    else { Console.WriteLine("Adding a massage does not need ID!"); }
                }
                else if (method == "PUT")
                {
                    if (substrings.Length == 5) { return UpdateMessagebyId(id, Msg); }
                    else { Console.WriteLine("To update a massage enter the ID!"); }
                }
                else if (method == "DELETE")
                {
                    if (substrings.Length == 5) { return DeleteMessagebyId(id); }
                    else { Console.WriteLine("To delete a massage enter the ID!"); }
                }
                else
                {
                    Console.WriteLine(" Invalid Request Method!");
                    return new Response { status = HttpStatus.Method_Not_Allowed };
                }
            }
            else
            {
                check3 = false;
                return wrongCommand();
               
            }

            return new Response { status = HttpStatus.Bad_Request };

        }

        public void getCommandandID(string path)
            {
                string pattern = "(/)";
                substrings = Regex.Split(path, pattern);// Split on hyphens
                                                        //Console.WriteLine(substrings.Length);
                                                        //foreach (string match in substrings){Console.WriteLine("'{0}'", match);}
                if (substrings.Length == 3)
                {
                    command = substrings[2];
                    
                }
                else if (substrings.Length == 5)
                {
                    command = substrings[2];

                    if (Int32.TryParse(substrings[4], out id))
                    {
                        Console.WriteLine("You Entered this Message ID: " + id);
                    }
                    else
                    {
                        Console.WriteLine("I couldn't convert the Id!");
                         check1 = false;
                    }
                }
            else { check2 = false; }
            }

        public string getCommand() { return command; }
        public int getId() { return id; }
        public bool getCheck1() { return check1; }
        public bool getCheck2() { return check2; }
        public bool getCheck3() { return check3; }

        private Response wrongCommand()
        {
            Console.WriteLine(" You entered wrong value !.");
            Console.WriteLine(" Sending Response ------------->");
            return new Response { status = HttpStatus.Not_Found};
        }

        private Response DeleteMessagebyId(int MId )
        {
            if (Messages[MId] != "")
            {
                Messages[MId] = "";
                Console.WriteLine(" This Message will be deleted.");
                Console.WriteLine(" Sending Response ------------->");
                return new Response { status = HttpStatus.Ok , content = " Message deleted at this id." }; 
                
            }
            else 
            {
                return new Response { status = HttpStatus.Not_Found };
            }
        }

        private Response UpdateMessagebyId(int MId , string M)
        {
          
            if (Messages[MId] == "")
            {
                return new Response { status = HttpStatus.No_Content};
            }
            else
            {
                Messages[MId] = M;
                Console.WriteLine(" This Message is updated to  : " + Messages[MId]);
                Console.WriteLine(" Sending Response ------------->");
                return new Response { status = HttpStatus.Ok , content = Messages[MId] + " is updated at this id." };
            }
            
        }

        

        private Response AddMessage( string M)
        {
            
            Messages.Add(M);
            int index = 0;
            for(int i=0; i < Messages.Count; i++) { if (Messages[i] == M) index = i; }
            Console.WriteLine(" You added this Message : " + M + " ---->  at this id ( " + index +" )" );
            Console.WriteLine(" Sending Response ------------->");
            return new Response { status = HttpStatus.Ok, content= M  + " is added." };
        }

        private Response ShowMessagebbId(int MId )
        {
           
            if (Messages[MId] == "")
            {
                return new Response { status = HttpStatus.No_Content };
            }
            else
            {
                string M = Messages[MId];
                Console.WriteLine(" This Message is : " + M);
                Console.WriteLine(" Sending Response ------------->");
                return new Response { status = HttpStatus.Ok , content= M + " is saved at this id." };
            }
        }

        private Response ListAllMessages( )
        {
            
            if (check==1)
            {
                return new Response { status = HttpStatus.No_Content };
            }
            else
            {
                string joined = string.Join<string>("\n", Messages);
                Console.WriteLine("You have these Messages:");
                for (int i = 0; i < Messages.Count; i++)
                {
                    Console.WriteLine(" Message : " + Messages[i] + " ---->  at this id ( " + i + " )");

                }

                Console.WriteLine(" Sending Response ------------->");
                return new Response { status = HttpStatus.Ok, content = " You have these Messages:\n" + joined };
              
            }
        }
    }
}
