using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Business.Exceptions
{
    public class CustomerNotCreatedException : Exception
    {
        public CustomerNotCreatedException() : base()
        {
        }

        public CustomerNotCreatedException(string? message) : base(message)
        {
        }

        public CustomerNotCreatedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
