using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class UnauthorizedException(string Message = "UserName or Password not Valid") : Exception(Message)
    {
        
    }
}
