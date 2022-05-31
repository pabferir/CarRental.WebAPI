using CarRental.Infrastructure.Business.Customers.Commands;
using CarRental.Infrastructure.Business.Customers.Dtos;
using CarRental.Infrastructure.Business.Customers.Services;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Handlers
{
    internal class DeleteCustomerByIdHandler : IRequestHandler<DeleteCustomerByIdCommand, CustomerDto>
    {
        private readonly ICustomerService _customerService;

        public DeleteCustomerByIdHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerDto> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.DeleteCustomerById(request.Id).ConfigureAwait(false);

            return result;
        }
    }
}
