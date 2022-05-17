using CarRental.Infrastructure.Business.Converters;
using CarRental.Infrastructure.Business.Dtos;
using CarRental.Infrastructure.Business.Exceptions;
using CarRental.Infrastructure.Data.Context;
using CarRental.Infrastructure.Data.RepositoryInterfaces;
using CarRental.SharedKernel.Handler;
using CarRental.SharedKernel.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRental.Infrastructure.Business.UseCases
{
    public static class GetAllCustomers
    {
        public record Query() : IRequest<Response>;
        public record Response(IList<CustomerDto> Customers);

        public class Handler : Handler<CarRentalDbContext>, IRequestHandler<Query, Response>
        {
            private readonly ILogger<Handler> _logger;
            private readonly ICustomerRepository _customerRepository;

            public Handler(ILogger<Handler> logger, IUnitOfWork<CarRentalDbContext> uoW) : base(uoW)
            {
                _logger = logger;
                _customerRepository = uoW.GetRepository<ICustomerRepository>();
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var customers = await _customerRepository.GetAllCustomers().ConfigureAwait(false);
                if (customers == null)
                {
                    _logger.LogError($"Error while attempting to get all Customers from the Database.");
                    throw new CustomerNotFoundException("Couldn't find any Customer in the Database");
                }

                return customers.Any() ? new Response(customers.Select(CustomerConverter.ModelToDto).ToList()) : null;
            }
        }
    }
}
