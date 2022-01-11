using CarRental.Core.Business.Dtos;
using CarRental.Core.Domain.Entities;

namespace CarRental.Core.Business.Converters
{
    public static class CustomerConverter
    {
        public static CustomerDto ModelToDto(Customer customer)
        {
            if (customer == null) return new CustomerDto();

            return new CustomerDto
            {
                Id = customer.Id,
                IdentityNumber = customer.IdentityNumber,
                Name = customer.Name,
                Surname = customer.Surname,
                DateOfBirth = customer.DateOfBirth,
                TelephoneNumber = customer.TelephoneNumber
            };
        }
    }
}
