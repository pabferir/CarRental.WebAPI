namespace CarRental.Infrastructure.Business.Customers.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
