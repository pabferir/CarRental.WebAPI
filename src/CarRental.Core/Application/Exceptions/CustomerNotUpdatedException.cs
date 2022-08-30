namespace CarRental.Core.Application.Exceptions
{
    public class CustomerNotUpdatedException : Exception
    {
        public CustomerNotUpdatedException() : base()
        {
        }

        public CustomerNotUpdatedException(string message) : base(message)
        {
        }

        public CustomerNotUpdatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
