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

        public Task<Customer> InsertCustomer(Customer customer)
        {
            return Insert(customer);
        }

        public Task<IEnumerable<Customer>> GetCustomer(Expression<Func<Customer, bool>> filter = null)
        {
            return Get(filter);
        }

        public Task<Customer> UpdateCustomer(Customer customer)
        {
            return Update(customer);
        }

        public Task<bool> DeleteCustomer(Customer customer)
        {
            return Delete(customer);
        }

        public Task<bool> DeleteCustomer(Expression<Func<Customer, bool>> filter = null)
        {
            return Delete(filter);
        }
    }
}
