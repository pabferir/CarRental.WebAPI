namespace CarRental.Infrastructure.Business.Exceptions
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
