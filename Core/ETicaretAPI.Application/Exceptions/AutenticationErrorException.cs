using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class AutenticationErrorException : Exception
    {
        public AutenticationErrorException() : base("Autorization Error")
        {
        }

        public AutenticationErrorException(string? message) : base(message)
        {
        }

        public AutenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
