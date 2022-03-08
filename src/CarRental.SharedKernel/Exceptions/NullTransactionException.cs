namespace CarRental.SharedKernel.Exceptions
{
    public class NullTransactionException : Exception
    {
        public NullTransactionException() : base()
        {
        }

        public NullTransactionException(string message) : base(message)
        {
        }

        public NullTransactionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
