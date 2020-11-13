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
    class EndPointsResponse
    {

        public static List<string> Messages = new List<string>();
        public static int check = 1;
        string command = "";
        int id = 0;
        string[] substrings;

        public EndPointsResponse(){}

        public void Methodhandler(string method , string path , string Msg , NetworkStream stream)
        {

            getCommandandID(path , stream);

            if (command == "messages")
            {
                if (method == "GET")
                {
                    if (substrings.Length == 3) { ListAllMessages(stream); }
                    else { ShowMessagebbId(id, stream); }
                }
                else if (method == "POST")
                {
                    check = 0;
                    if (substrings.Length == 3) { AddMessage(Msg, stream); }
                    else { Console.WriteLine("Adding a massage does not need ID!"); }
                }
                else if (method == "PUT")
                {
                    if (substrings.Length == 5) { UpdateMessagebyId(id, Msg, stream); }
                    else { Console.WriteLine("To update a massage enter the ID!"); }
                }
                else if (method == "DELETE")
                {
                    if (substrings.Length == 5) { DeleteMessagebyId(id, stream); }
                    else { Console.WriteLine("To delete a massage enter the ID!"); }
                }
                else
                {
                    Console.WriteLine(" Invalid Request Method!");
                    byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 405 Method Not Allowed\n\r\n\r");
                    stream.Write(response, 0, response.Length);
                    stream.Flush();
                }
            }
            else
            {
                wrongCommand(stream);
            }

        }

        private void getCommandandID(string path , NetworkStream stream)
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
                    Console.WriteLine(" Invalid Request Method!");
                    byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 406 Not Acceptable\n\r\n\r");
                    stream.Write(response, 0, response.Length);
                    stream.Flush();
                }


            }
        }

        private void wrongCommand(NetworkStream stream)
        {
            Console.WriteLine(" You entered wrong value !.");
            Console.WriteLine(" Sending Response ------------->");
            byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 204 No Content\n\r\n\r");
            stream.Write(response, 0, response.Length);
            stream.Flush();
        }

        private void DeleteMessagebyId(int MId , NetworkStream stream)
        {
            if (Messages[MId] != "")
            {
                Messages[MId] = "";
                Console.WriteLine(" This Message will be deleted.");
                Console.WriteLine(" Sending Response ------------->");
                byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\n\r" + " Message deleted at this id.");
                stream.Write(response, 0, response.Length);
                stream.Flush();
            }
            else 
            {
                byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 404 Not Found\n\r\n\r");
                stream.Write(response, 0, response.Length);
                stream.Flush();
            }
        }

        private void UpdateMessagebyId(int MId , string M, NetworkStream stream)
        {
            byte[] response;
            if (Messages[MId] == "")
            {
                response = Encoding.ASCII.GetBytes("HTTP/1.1 204 No Content\n\r\n\r");
            }
            else
            {
                Messages[MId] = M;
                Console.WriteLine(" This Message is updated to  : " + Messages[MId]);
                Console.WriteLine(" Sending Response ------------->");
                 response = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\n\r" + Messages[MId] + " is updated at this id.");
            }
            stream.Write(response, 0, response.Length);
            stream.Flush();
        }

        

        private void AddMessage( string M, NetworkStream stream)
        {
            
            Messages.Add(M);
            int index = 0;
            for(int i=0; i < Messages.Count; i++) { if (Messages[i] == M) index = i; }
            Console.WriteLine(" You added this Message : " + M + " ---->  at this id ( " + index +" )" );
            Console.WriteLine(" Sending Response ------------->");
            byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\n\r" + M + " is added.");
            stream.Write(response, 0, response.Length);
            stream.Flush();
        }

        private void ShowMessagebbId(int MId , NetworkStream stream)
        {
            byte[] response;
            if (Messages[MId] == "")
            {
                response = Encoding.ASCII.GetBytes("HTTP/1.1 204 No Content\n\r\n\r");
            }
            else
            {
                string M = Messages[MId];
                Console.WriteLine(" This Message is : " + M);
                Console.WriteLine(" Sending Response ------------->");
                response = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\n\r" + M + " is saved at this id.");
            }
            stream.Write(response, 0, response.Length);
            stream.Flush();
        }

        private void ListAllMessages(NetworkStream stream)
        {
            byte[] response;
            if (check==1)
            {
                response = Encoding.ASCII.GetBytes("HTTP/1.1 204 No Content\n\r\n\r");
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
                 response = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\n\r" + " You have these Messages:\n" + joined);
              
            }
            stream.Write(response, 0, response.Length);
            stream.Flush();
        }
    }
}
