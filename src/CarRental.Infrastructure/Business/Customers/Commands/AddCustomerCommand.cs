using CarRental.Infrastructure.Business.Customers.Dtos;
using CarRental.Infrastructure.Business.Customers.Requests;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Commands
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }

        public AddCustomerCommand(AddCustomerRequest request)
        {
            IdentityNumber = request.IdentityNumber;
            Name = request.Name;
            Surname = request.Surname;
            DateOfBirth = request.DateOfBirth;
            TelephoneNumber = request.TelephoneNumber;
        }
    }
}
