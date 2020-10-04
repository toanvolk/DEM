using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.Infrastructure
{
    public enum StatuCode
    {
        OK=200,
        Created = 201,
        BadRequest = 404,
        Unauthorized = 401 ,
        InternalServerError = 500
    }   
}
