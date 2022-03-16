using CarRental.Core.Business.Converters;
using CarRental.Core.Domain.Context;
using CarRental.Core.Domain.Entities;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.SharedKernel.Repository;
using System.Linq.Expressions;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CarRentalDbContext context) : base(context)
        {
        }

        public Task<Customer> InsertCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false)
        {
            var customer = CustomerConverter.PropertiesToModel(identityNumber, name, surname, dateOfBirth, telephoneNumber);
            return Insert(customer, saveChanges);
        }

        public Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return GetCustomerWhere(null);
        }

        public async Task<Customer> GetCustomerById(Guid id)
        {
            return (await GetCustomerWhere(customer => customer.Id == id).ConfigureAwait(false)).FirstOrDefault();
        }

        public Task<Customer> UpdateCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false)
        {
            var updatedCustomer = CustomerConverter.PropertiesToModel(identityNumber, name, surname, dateOfBirth, telephoneNumber, id);
            return Update(updatedCustomer, saveChanges);
        }

        public Task<bool> DeleteAllCustomers()
        {
            return DeleteCustomerWhere(null);
        }

        public Task<bool> DeleteCustomerById(Guid id)
        {
            return DeleteCustomerWhere(customer => customer.Id == id);
        }

        private Task<IEnumerable<Customer>> GetCustomerWhere(Expression<Func<Customer, bool>> filter = null)
        {
            return GetWhere(filter);
        }

        private Task<bool> DeleteCustomerWhere(Expression<Func<Customer, bool>> filter = null, bool saveChanges = false)
        {
            return DeleteWhere(filter, saveChanges);
        }
    }
}
