using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
   public class Response
    {
        public string version = "HTTP/1.1 ";
        public HttpStatus status = HttpStatus.Ok;
        public string content;

        public string formatted_Response()
        {
            return version + getstatus() + (String.IsNullOrEmpty(content) ? "\n\r\n\r" : "\n\r" + content);
        }

        private string getstatus()
        {
            switch(status)
            {
                case HttpStatus.Ok:
                    return "200 OK";
                case HttpStatus.No_Content:
                    return "204 No Content";
                case HttpStatus.Not_Found:
                    return "404 Not Found";
                case HttpStatus.Not_Acceptable:
                    return "406 Not Acceptable";
                case HttpStatus.Method_Not_Allowed:
                    return "405 Method Not Allowed";
                case HttpStatus.Internal_Server_Error:
                    return "500 Internal Server Error";
                case HttpStatus.Bad_Request:
                    return "400 Bad Request";
                default:
                    return "500 Internal Server Error";
            }
        }
        // takes the object and compares the values odf the object when they have different references
        public override bool Equals(object obj)
        {
            return obj is Response response &&
                   version == response.version &&
                   status == response.status &&
                   content == response.content;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(version, status, content);
        }
    }
}
