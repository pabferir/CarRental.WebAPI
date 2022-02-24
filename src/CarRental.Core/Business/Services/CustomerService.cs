using CarRental.Core.Business.Converters;
using CarRental.Core.Business.Dtos;
using CarRental.Core.Business.Exceptions;
using CarRental.Core.Domain.Context;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.Core.Domain.ServiceInterfaces;
using CarRental.SharedKernel.Service;
using CarRental.SharedKernel.UnitOfWork;
using System.Data;

namespace CarRental.Core.Business.Services
{
    public class CustomerService : Service<CarRentalDbContext>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IUnitOfWork<CarRentalDbContext> uoW) : base(uoW)
        {
            _customerRepository = uoW.GetRepository<ICustomerRepository>();
        }

        /// <summary>
        /// Creates a new Customer
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="telephoneNumber"></param>
        /// <returns></returns>
        /// <exception cref="CustomerNotCreatedException"></exception>
        public async Task<CustomerDto> CreateCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber)
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var result = await _customerRepository.InsertCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber, false).ConfigureAwait(false);
                if (result == null)
                {
                    throw new CustomerNotCreatedException("Couldn't create the Customer in the database.");
                }
                await UoW.CommitAsync(transaction).ConfigureAwait(false);
                //await transaction.CommitAsync().ConfigureAwait(false);
                return CustomerConverter.ModelToDto(result);
            }
            catch (DBConcurrencyException ex)
            {
                await UoW.RollbackAsync(transaction).ConfigureAwait(false);
                //transaction.Rollback();
                throw new DBConcurrencyException("Couldn't create the Customer in the database.", ex);
            }
        }

        /// <summary>
        /// Gets all existing Customers from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetCustomerWhere(null).ConfigureAwait(false);
            if (!customers.Any())
            {
                //throw new CustomerNotFoundException("Couldn't find any Customer in the database");
                return new List<CustomerDto>();
            }

            return customers.Select(CustomerConverter.ModelToDto);
        }

        /// <summary>
        /// Gets a Customer provided its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        public async Task<CustomerDto> GetCustomerById(Guid id)
        {
            var customer = (await _customerRepository.GetCustomerWhere(customer => customer.Id == id).ConfigureAwait(false)).FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException($"Couldn't find the Customer with Id {id} in the database.");
            }

            return CustomerConverter.ModelToDto(customer);
        }

        /// <summary>
        /// Updates an exisiting Customer provided its properties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="identityNumber"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="telephoneNumber"></param>
        /// <returns></returns>
        /// <exception cref="CustomerNotUpdatedException"></exception>
        public async Task<CustomerDto> EditCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber)
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                //await GetCustomerById(id);
                var result = await _customerRepository.UpdateCustomer(id, identityNumber, name, surname, dateOfBirth, telephoneNumber, false).ConfigureAwait(false);
                if (result == null)
                {
                    throw new CustomerNotUpdatedException($"Couldn't update the Customer with Id {id} in the database.");
                }
                await UoW.CommitAsync(transaction).ConfigureAwait(false);
                //await transaction.CommitAsync().ConfigureAwait(false);
                return CustomerConverter.ModelToDto(result);
            }
            catch (DBConcurrencyException ex)
            {
                await UoW.RollbackAsync(transaction).ConfigureAwait(false);
                //transaction.Rollback();
                throw new DBConcurrencyException($"Couldn't update the Customer with Id {id} in the database.", ex);
            }
        }


        public async Task<bool> DeleteAllCustomers()
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var deleted = await _customerRepository.DeleteCustomerWhere(null).ConfigureAwait(false);
                if (!deleted)
                {
                    throw new CustomerNotDeletedException("Couldn't delete all customers in the database");
                }
                await UoW.CommitAsync(transaction).ConfigureAwait(false);
                //await transaction.CommitAsync().ConfigureAwait(false);

                return true;
            }
            catch (DBConcurrencyException ex)
            {
                await UoW.RollbackAsync(transaction).ConfigureAwait(false);
                //transaction.Rollback();
                throw new DBConcurrencyException("Couldn't delete all customers in the database", ex);
            }
        }

        /// <summary>
        /// Deletes a Customer provided its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteCustomerById(Guid id)
        {
            using var transaction = await UoW.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var deleted = await _customerRepository.DeleteCustomerWhere(customer => customer.Id == id, false).ConfigureAwait(false);
                if (!deleted)
                {
                    throw new CustomerNotDeletedException($"Couldn't delete customer with id { id } in the database");
                }
                await UoW.CommitAsync(transaction).ConfigureAwait(false);
                //await transaction.CommitAsync().ConfigureAwait(false);

                return true;
            }
            catch (DBConcurrencyException ex)
            {
                await UoW.RollbackAsync(transaction).ConfigureAwait(false);
                //transaction.Rollback();
                throw new DBConcurrencyException($"Couldn't update the Customer with Id {id} in the database.", ex);
            }
        }
    }
}
