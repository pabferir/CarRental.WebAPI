using CarRental.Infrastructure.Business.Customers.Dtos;
using CarRental.Infrastructure.Business.Customers.Queries;
using CarRental.Infrastructure.Business.Customers.Services;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Handlers
{
    internal class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerService _customerService;

        public GetCustomerByIdHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _customerService.GetCustomerById(request.Id);

            return result;
        }
    }
}
