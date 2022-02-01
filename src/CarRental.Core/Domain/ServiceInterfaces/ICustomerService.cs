using CarRental.Core.Business.Dtos;

namespace CarRental.Core.Domain.ServiceInterfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber);
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        Task<CustomerDto> GetCustomerById(Guid id);
        Task<CustomerDto> EditCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber);
        Task<bool> DeleteAllCustomers();
        Task<bool> DeleteCustomerById(Guid id);
    }
}
