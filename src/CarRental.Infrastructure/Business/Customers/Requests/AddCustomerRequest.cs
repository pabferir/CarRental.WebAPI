namespace CarRental.Infrastructure.Business.Customers.Requests
{
    public class AddCustomerRequest
    {
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
