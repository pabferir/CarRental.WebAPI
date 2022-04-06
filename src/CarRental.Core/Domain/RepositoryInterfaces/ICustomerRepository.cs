using CarRental.Core.Domain.Entities;
using CarRental.SharedKernel.Repository;

namespace CarRental.Core.Domain.RepositoryInterfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> InsertCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false);
        Task<IEnumerable<Customer>> GetCustomersBy(Guid id = default, string identityNumber = default, string name = default, string surname = default, DateTime dateOfBirth = default, string telephoneNumber = default);
        Task<Customer> UpdateCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false);
        Task<IEnumerable<Customer>> DeleteCustomersBy(Guid id = default, string identityNumber = default, string name = default, string surname = default, DateTime dateOfBirth = default, string telephoneNumber = default, bool saveChanges = false);
    }
}
