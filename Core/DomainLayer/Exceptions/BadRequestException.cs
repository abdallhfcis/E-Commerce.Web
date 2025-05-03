using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class BadRequestException(List<string> Errors):Exception("ValidationError!")
    {
        public List<string> Errors { get;  }= Errors;
    }
}
