using CarRental.Infrastructure.Business.Customers.Dtos;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<IList<CustomerDto>>
    {
    }
}
