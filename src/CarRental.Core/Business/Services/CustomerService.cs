using CarRental.Core.Business.Converters;
using CarRental.Core.Business.Dtos;
using CarRental.Core.Business.Exceptions;
using CarRental.Core.Domain.Context;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.Core.Domain.ServiceInterfaces;
using CarRental.SharedKernel.Service;
using CarRental.SharedKernel.UnitOfWork;

namespace CarRental.Core.Business.Services
{
    public class CustomerService : Service<CarRentalDbContext>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IUnitOfWork<CarRentalDbContext> uoW) : base(uoW)
        {
            _customerRepository = uoW.GetRepository<ICustomerRepository>();
        }

        public async Task<CustomerDto> CreateCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber)
        {
            var result = await _customerRepository.InsertCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber, false).ConfigureAwait(false);
            if (result == null)
            {
                throw new CustomerNotCreatedException("Couldn't create the Customer in the database.");
            }
            await UoW.SaveChangesAsync();
            return CustomerConverter.ModelToDto(result);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetCustomerWhere(null).ConfigureAwait(false);
            if (!customers.Any())
            {
                throw new CustomerNotFoundException("Couldn't find any Customer in the database");
            }
            return customers.Select(CustomerConverter.ModelToDto);
        }

        public async Task<CustomerDto> GetCustomerById(Guid id)
        {
            var customer = (await _customerRepository.GetCustomerWhere(customer => customer.Id == id).ConfigureAwait(false)).FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException($"Couldn't find the Customer with Id {id} in the database.");
            }
            return CustomerConverter.ModelToDto(customer);
        }

        public async Task<CustomerDto> EditCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber)
        {
            await GetCustomerById(id);
            var result = await _customerRepository.UpdateCustomer(id, identityNumber, name, surname, dateOfBirth, telephoneNumber, false).ConfigureAwait(false);
            if (result == null)
            {
                throw new CustomerNotUpdatedException($"Couldn't update the Customer with Id {id} in the database.");
            }
            await UoW.SaveChangesAsync();
            return CustomerConverter.ModelToDto(result);
        }

        public async Task<bool> DeleteAllCustomers()
        {
            await _customerRepository.DeleteCustomerWhere(null, false).ConfigureAwait(false);
            var deleted = await UoW.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> DeleteCustomerById(Guid id)
        {
            await _customerRepository.DeleteCustomerWhere(customer => customer.Id == id, false).ConfigureAwait(false);
            var deleted = await UoW.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
