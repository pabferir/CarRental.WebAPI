using System.ComponentModel.DataAnnotations;

namespace CarRental.Core.Application.Dtos
{
    public class CustomerDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
    }
}
