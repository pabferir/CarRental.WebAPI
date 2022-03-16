using CarRental.Core.Domain.Entities;
using CarRental.SharedKernel.Repository;

namespace CarRental.Core.Domain.RepositoryInterfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> InsertCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(Guid id);
        Task<Customer> UpdateCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false);
        Task<bool> DeleteAllCustomers();
        Task<bool> DeleteCustomerById(Guid id);
    }
}
