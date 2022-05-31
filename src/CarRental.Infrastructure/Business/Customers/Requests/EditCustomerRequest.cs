using System.ComponentModel.DataAnnotations;

namespace CarRental.Infrastructure.Business.Customers.Requests
{
    public class EditCustomerRequest
    {
        [Required]
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
