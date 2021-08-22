using System;

namespace CrudMvc.Exceptions
{
    public class NotValidCpfException : ApplicationException
    {
        public NotValidCpfException(string message) : base(message)
        {

        }
    }
}
