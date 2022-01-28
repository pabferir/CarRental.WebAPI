using CarRental.Core.Domain.Entities;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.Infrastructure.Data.Database;
using CarRental.SharedKernel.Repository;
using System.Linq.Expressions;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CarRentalDbContext context) : base(context)
        {
        }

        public Task<Customer> InsertCustomer(Customer customer, bool saveChanges = true)
        {
            return Insert(customer, saveChanges);
        }

        public Task<IEnumerable<Customer>> GetCustomerWhere(Expression<Func<Customer, bool>>? filter = null)
        {
            return GetWhere(filter);
        }

        public Task<Customer> UpdateCustomer(Customer customer, bool saveChanges = true)
        {
            return Update(customer, saveChanges);
        }

        public Task<bool> DeleteCustomer(Customer customer, bool saveChanges = true)
        {
            return Delete(customer, saveChanges);
        }

        public Task<bool> DeleteCustomerWhere(Expression<Func<Customer, bool>>? filter = null, bool saveChanges = true)
        {
            return DeleteWhere(filter, saveChanges);
        }
    }
}
