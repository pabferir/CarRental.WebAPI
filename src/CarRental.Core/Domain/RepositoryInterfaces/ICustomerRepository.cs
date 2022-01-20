using CarRental.Core.Domain.Entities;
using CarRental.SharedKernel.Repository;
using System.Linq.Expressions;

namespace CarRental.Core.Domain.RepositoryInterfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> InsertCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetCustomer(Expression<Func<Customer, bool>> filter = null);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(Customer customer);
        Task<bool> DeleteCustomer(Expression<Func<Customer, bool>> filter = null);
    }
}
