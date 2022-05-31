using CarRental.Infrastructure.Business.Customers.Commands;
using CarRental.Infrastructure.Business.Customers.Dtos;
using CarRental.Infrastructure.Business.Customers.Services;
using MediatR;

namespace CarRental.Infrastructure.Business.Customers.Handlers
{
    internal class EditCustomerHandler : IRequestHandler<EditCustomerCommand, CustomerDto>
    {
        private readonly ICustomerService _customerService;

        public EditCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerDto> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.EditCustomer(request.Id, request.IdentityNumber, request.Name, request.Surname, request.DateOfBirth, request.TelephoneNumber).ConfigureAwait(false);

            return result;
        }
    }
}
