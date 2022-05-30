using CarRental.Infrastructure.Business.Dtos;
using MediatR;

namespace CarRental.Infrastructure.Business.Handlers.Queries
{
    public class GetAllCustomersQuery : IRequest<IList<CustomerDto>>
    {
    }
}
