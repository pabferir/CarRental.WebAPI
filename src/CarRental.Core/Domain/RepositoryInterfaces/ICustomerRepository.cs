using CarRental.Core.Domain.Entities;
using CarRental.SharedKernel.Repository;
using System.Linq.Expressions;

namespace CarRental.Core.Domain.RepositoryInterfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> InsertCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false);
        Task<IEnumerable<Customer>> GetCustomerWhere(Expression<Func<Customer, bool>>? filter);
        Task<Customer> UpdateCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false);
        Task<bool> DeleteCustomerWhere(Expression<Func<Customer, bool>>? filter, bool saveChanges = false);
    }
}
