using CarRental.Infrastructure.Business.Customers.Commands;
using CarRental.Infrastructure.Business.Customers.Dtos;
using CarRental.Infrastructure.Business.Customers.Services;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Handlers
{
    internal class AddCustomerHandler : IRequestHandler<AddCustomerCommand, CustomerDto>
    {
        private readonly ICustomerService _customerService;

        public AddCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.CreateCustomer(request.IdentityNumber, request.Name, request.Surname, request.DateOfBirth, request.TelephoneNumber).ConfigureAwait(false);

            return result;
        }
    }
}
