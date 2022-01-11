namespace CarRental.Core.Domain.Entities
{
    public partial class Customer
    {
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; } = null!;
    }
}
