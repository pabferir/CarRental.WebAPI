using CarRental.Infrastructure.Business.Customers.Dtos;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Commands
{
    public class DeleteCustomerByIdCommand : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }

        public DeleteCustomerByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
