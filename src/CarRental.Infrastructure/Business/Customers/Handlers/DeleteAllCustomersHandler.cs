using CarRental.Infrastructure.Business.Customers.Commands;
using CarRental.Infrastructure.Business.Customers.Dtos;
using CarRental.Infrastructure.Business.Customers.Services;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Handlers
{
    internal class DeleteAllCustomersHandler : IRequestHandler<DeleteAllCustomersCommand, IList<CustomerDto>>
    {
        private readonly ICustomerService _customerService;

        public DeleteAllCustomersHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IList<CustomerDto>> Handle(DeleteAllCustomersCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.DeleteAllCustomers().ConfigureAwait(false);

            return result.ToList();
        }
    }
}
