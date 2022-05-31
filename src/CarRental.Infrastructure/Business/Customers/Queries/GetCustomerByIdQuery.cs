using CarRental.Infrastructure.Business.Customers.Dtos;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }

        public GetCustomerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
