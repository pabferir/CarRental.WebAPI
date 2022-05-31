using CarRental.Infrastructure.Business.Customers.Dtos;

namespace CarRental.Infrastructure.Business.Customers.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber);
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        Task<CustomerDto> GetCustomerById(Guid id);
        Task<CustomerDto> EditCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber);
        Task<IEnumerable<CustomerDto>> DeleteAllCustomers();
        Task<CustomerDto> DeleteCustomerById(Guid id);
    }
}
