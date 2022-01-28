using CarRental.Core.Domain.Entities;
using CarRental.SharedKernel.Repository;
using System.Linq.Expressions;

namespace CarRental.Core.Domain.RepositoryInterfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> InsertCustomer(Customer customer, bool saveChanges);
        Task<IEnumerable<Customer>> GetCustomerWhere(Expression<Func<Customer, bool>>? filter);
        Task<Customer> UpdateCustomer(Customer customer, bool saveChanges);
        Task<bool> DeleteCustomer(Customer customer, bool saveChanges);
        Task<bool> DeleteCustomerWhere(Expression<Func<Customer, bool>>? filter, bool saveChanges);
    }
}
