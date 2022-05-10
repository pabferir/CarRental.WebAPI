using CarRental.Infrastructure.Business.Converters;
using CarRental.Infrastructure.Business.Dtos;
using CarRental.Infrastructure.Business.Exceptions;
using CarRental.Infrastructure.Business.ServiceInterfaces;
using CarRental.Infrastructure.Data.Context;
using CarRental.Infrastructure.Data.RepositoryInterfaces;
using CarRental.SharedKernel.Service;
using CarRental.SharedKernel.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CarRental.Infrastructure.Business.Services
{
    public class CustomerService : Service<CarRentalDbContext>, ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ILogger<CustomerService> logger, IUnitOfWork<CarRentalDbContext> uoW) : base(uoW)
        {
            _logger = logger;
            _customerRepository = uoW.GetRepository<ICustomerRepository>();
        }

        /// <summary>
        /// Creates a new Customer.
        /// </summary>
        /// <param name="identityNumber"> The identity number of the new Customer. </param>
        /// <param name="name"> The name of the new Customer. </param>
        /// <param name="surname"> The surname of the new Customer. </param>
        /// <param name="dateOfBirth"> The date of birth of the new Customer. </param>
        /// <param name="telephoneNumber"> The telephone number of the new Customer. </param>
        /// <returns> A CustomerDto that represents the new entity inserted in the Database. </returns>
        /// <exception cref="CustomerNotCreatedException"> Thrown when the Insert operation can't be completed. </exception>
        public async Task<CustomerDto> CreateCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber)
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            var result = await _customerRepository.InsertCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber).ConfigureAwait(false);
            if (result == null)
            {
                _logger.LogError($"Error while attempting to create the Customer {name} {surname} in the Database.");
                throw new CustomerNotCreatedException("Couldn't create the Customer in the Database.");
            }
            await UoW.CommitAsync(transaction).ConfigureAwait(false);

            return CustomerConverter.ModelToDto(result);
        }

        /// <summary>
        /// Retrieves from the Database all the existing Customers.
        /// </summary>
        /// <returns> An enumerable containing a CustomerDto for each of the existing Customers. </returns>
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetCustomersBy().ConfigureAwait(false);
            if (customers == null)
            {
                _logger.LogError($"Error while attempting to get all Customers from the Database.");
                throw new CustomerNotFoundException("Couldn't find any Customer in the Database");
            }

            return customers.Any() ? customers.Select(CustomerConverter.ModelToDto) : new List<CustomerDto>();
        }

        /// <summary>
        /// Retrireves from the Database a specific Customer given its Id.
        /// </summary>
        /// <param name="id"> The primary key of the Customer to retrieve. </param>
        /// <returns> A CustomerDto that represents the Customer. </returns>
        /// <exception cref="CustomerNotFoundException"> Thrown when there is no Customer with a primary key matching the given Id in the Database. </exception>
        public async Task<CustomerDto> GetCustomerById(Guid id)
        {
            var customer = (await _customerRepository.GetCustomersBy(id: id).ConfigureAwait(false)).FirstOrDefault();
            if (customer == null)
            {
                _logger.LogError($"Error while attempting to get Customer with Id {id} in the Database.");
                throw new CustomerNotFoundException($"Couldn't find the Customer with Id {id} in the Database.");
            }

            return CustomerConverter.ModelToDto(customer);
        }

        /// <summary>
        /// Updates the values of the properties of an already existing Customer.
        /// </summary>
        /// <param name="id"> The primary key of the Customer to update. Cannot be changed. </param>
        /// <param name="identityNumber"> The updated identity number of the Customer. </param>
        /// <param name="name"> The updated name of the Customer. </param>
        /// <param name="surname"> The updated surname of the Customer. </param>
        /// <param name="dateOfBirth"> The updated date of birth of the Customer. </param>
        /// <param name="telephoneNumber"> The updated telephone number of the Customer. </param>
        /// <returns> A CustomerDto that represents the updated Customer. </returns>
        /// <exception cref="CustomerNotUpdatedException"> Thrown when the Update operation can't be completed. </exception>
        public async Task<CustomerDto> EditCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber)
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            var result = await _customerRepository.UpdateCustomer(id, identityNumber, name, surname, dateOfBirth, telephoneNumber).ConfigureAwait(false);
            if (result == null)
            {
                _logger.LogError($"Error while attempting to update the Customer with Id {id} in the Database.");
                throw new CustomerNotUpdatedException($"Couldn't update the Customer with Id {id} in the Database.");
            }
            await UoW.CommitAsync(transaction).ConfigureAwait(false);

            return CustomerConverter.ModelToDto(result);
        }

        /// <summary>
        /// Deletes from the Database all the existing Customers.
        /// </summary>
        /// <returns> An enumerable containing a CustomerDto for each of the deleted Customers. </returns>
        /// <exception cref="CustomerNotDeletedException"> Thrown when the Delete operations can't be completed. </exception>
        public async Task<IEnumerable<CustomerDto>> DeleteAllCustomers()
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            var customers = await _customerRepository.DeleteCustomersBy().ConfigureAwait(false);
            if (!customers.Any())
            {
                _logger.LogError("Couldn't delete all Customers in the Database");
                throw new CustomerNotDeletedException("Couldn't delete all Customers in the Database");
            }
            await UoW.CommitAsync(transaction).ConfigureAwait(false);

            return customers.Select(CustomerConverter.ModelToDto);
        }

        /// <summary>
        /// Delets from the Database a specific Customer given its Id.
        /// </summary>
        /// <param name="id"> The primary key of the Customer to delete. </param>
        /// <returns> A CustomerDto that represents the deleted Customer. </returns>
        /// <exception cref="CustomerNotDeletedException"> Thrown when the Delete operation can't be completed. </exception>
        public async Task<CustomerDto> DeleteCustomerById(Guid id)
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            var customer = (await _customerRepository.DeleteCustomersBy(id: id).ConfigureAwait(false)).FirstOrDefault();
            if (customer == null)
            {
                _logger.LogError($"Couldn't delete Customer with Id {id} in the Database");
                throw new CustomerNotDeletedException($"Couldn't delete Customer with Id {id} in the Database");
            }
            await UoW.CommitAsync(transaction).ConfigureAwait(false);

            return CustomerConverter.ModelToDto(customer);
        }
    }
}
