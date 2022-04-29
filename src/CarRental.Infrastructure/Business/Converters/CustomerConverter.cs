using CarRental.Core.Domain.Entities;
using CarRental.Infrastructure.Business.Dtos;

namespace CarRental.Infrastructure.Business.Converters
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

        public static Customer DtoToModel(CustomerDto customerDto)
        {
            if (customerDto == null) return new Customer();

            return new Customer
            {
                Id = customerDto.Id,
                IdentityNumber = customerDto.IdentityNumber,
                Name = customerDto.Name,
                Surname = customerDto.Surname,
                DateOfBirth = customerDto.DateOfBirth,
                TelephoneNumber = customerDto.TelephoneNumber
            };
        }

        public static Customer PropertiesToModel(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, Guid id = default)
        {
            return DtoToModel(new CustomerDto
            {
                Id = id,
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            });
        }
    }
}
