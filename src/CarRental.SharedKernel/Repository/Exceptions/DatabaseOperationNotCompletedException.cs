namespace CarRental.SharedKernel.Repository.Exceptions
{
    public class DatabaseOperationNotCompletedException : Exception
    {
        public DatabaseOperationNotCompletedException() : base()
        {
        }

        public DatabaseOperationNotCompletedException(string message) : base(message)
        {
        }

        public DatabaseOperationNotCompletedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
