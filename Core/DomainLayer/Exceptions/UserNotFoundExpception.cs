using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class UserNotFoundExpception(string Email):Exception($"Email: {Email} Not Found !")
    {
    }
}
