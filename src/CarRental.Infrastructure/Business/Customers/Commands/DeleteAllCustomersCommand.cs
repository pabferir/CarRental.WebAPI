using CarRental.Infrastructure.Business.Customers.Dtos;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Commands
{
    public class DeleteAllCustomersCommand : IRequest<IList<CustomerDto>>
    {
    }
}
