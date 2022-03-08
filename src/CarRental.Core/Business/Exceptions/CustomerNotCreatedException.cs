namespace CarRental.Core.Business.Exceptions
{
    public class CustomerNotCreatedException : Exception
    {
        public CustomerNotCreatedException() : base()
        {
        }

        public CustomerNotCreatedException(string message) : base(message)
        {
        }

        public CustomerNotCreatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
