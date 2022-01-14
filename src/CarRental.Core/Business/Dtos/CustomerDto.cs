using System.ComponentModel.DataAnnotations;

namespace CarRental.Core.Business.Dtos
{
    public class CustomerDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string IdentityNumber { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string TelephoneNumber { get; set; } = null!;
    }
}
