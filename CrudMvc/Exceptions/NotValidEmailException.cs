using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMvc.Exceptions
{
    public class NotValidEmailException : ApplicationException
    {
        public NotValidEmailException(string message) : base(message)
        {

        }
    }
}
