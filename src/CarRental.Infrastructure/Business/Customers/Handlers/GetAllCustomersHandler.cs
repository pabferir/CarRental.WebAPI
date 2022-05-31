using CarRental.Infrastructure.Business.Customers.Dtos;
using CarRental.Infrastructure.Business.Customers.Exceptions;
using CarRental.Infrastructure.Business.Customers.Queries;
using CarRental.Infrastructure.Business.Customers.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRental.Infrastructure.Business.Customers.Handlers
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
            var result = await _customerService.GetAllCustomers().ConfigureAwait(false);
            if (result == null)
            {
                _logger.LogError("Error while attempting to get all Customers from the Database.");
                throw new CustomerNotFoundException("Couldn't find any Customer in the Database");
            }

            return result.ToList();
        }
    }
}
