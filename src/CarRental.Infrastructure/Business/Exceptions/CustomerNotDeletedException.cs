namespace CarRental.Infrastructure.Business.Exceptions
{
    public class CustomerNotDeletedException : Exception
    {
        public CustomerNotDeletedException() : base()
        {
        }

        public CustomerNotDeletedException(string message) : base(message)
        {
        }

        public CustomerNotDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
