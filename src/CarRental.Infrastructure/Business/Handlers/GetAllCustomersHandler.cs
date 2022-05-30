using CarRental.Infrastructure.Business.Exceptions;
using CarRental.Infrastructure.Business.Dtos;
using CarRental.Infrastructure.Business.Handlers.Queries;
using CarRental.Infrastructure.Business.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRental.Infrastructure.Business.Handlers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IList<CustomerDto>>
    {
        private readonly ILogger<GetAllCustomersHandler> _logger;
        private readonly ICustomerService _customerService;

        public GetAllCustomersHandler(ILogger<GetAllCustomersHandler> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public async Task<IList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetAllCustomers().ConfigureAwait(false);
            if (customers == null)
            {
                _logger.LogError("Error while attempting to get all Customers from the Database.");
                throw new CustomerNotFoundException("Couldn't find any Customer in the Database");
            }

            return customers.Any() ? customers.ToList() : default;
        }
    }
}
