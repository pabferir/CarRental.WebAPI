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

        public Task<Customer> InsertCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = true)
        {
            var customer = CustomerConverter.PropertiesToModel(identityNumber, name, surname, dateOfBirth, telephoneNumber);
            return Insert(customer, saveChanges);
        }

        public Task<IEnumerable<Customer>> GetCustomerWhere(Expression<Func<Customer, bool>>? filter = null)
        {
            return GetWhere(filter);
        }

        public Task<Customer> UpdateCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = true)
        {
            var updatedCustomer = CustomerConverter.PropertiesToModel(identityNumber, name, surname, dateOfBirth, telephoneNumber, id);
            return Update(updatedCustomer, saveChanges);
        }

        public Task<bool> DeleteCustomerWhere(Expression<Func<Customer, bool>>? filter = null, bool saveChanges = true)
        {
            return DeleteWhere(filter, saveChanges);
        }
    }
}
