using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public enum HttpStatus
    {
        Ok = 200,
        No_Content = 204,
        Not_Found = 404,
        Internal_Server_Error = 500,
        Bad_Request = 400,
        Not_Acceptable = 406,
        Method_Not_Allowed = 405
    }
}
