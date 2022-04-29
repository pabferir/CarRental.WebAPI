namespace CarRental.SharedKernel.UnitOfWork.Exceptions
{
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException() : base()
        {
        }

        public RepositoryNotFoundException(string message) : base(message)
        {
        }

        public RepositoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
