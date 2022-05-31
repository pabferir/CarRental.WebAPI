namespace CarRental.Infrastructure.Business.Customers.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base()
        {
        }

        public CustomerNotFoundException(string message) : base(message)
        {
        }

        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
