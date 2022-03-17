using CarRental.Core.Business.Converters;
using CarRental.Core.Business.Filters;
using CarRental.Core.Domain.Context;
using CarRental.Core.Domain.Entities;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.SharedKernel.PredicateBuilder;
using CarRental.SharedKernel.Repository;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CarRentalDbContext context) : base(context)
        {
        }

        public Task<Customer> InsertCustomer(string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false)
        {
            var customer = CustomerConverter.PropertiesToModel(identityNumber, name, surname, dateOfBirth, telephoneNumber);
            return Insert(customer, saveChanges);
        }

        public Task<IEnumerable<Customer>> GetCustomersByFilter(Guid id = default, string identityNumber = default, string name = default, string surname = default, DateTime dateOfBirth = default, string telephoneNumber = default)
        {
            var filter = new CustomerFilter()
            {
                Id = id,
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            };
            var predicate = PredicateBuilder.True<Customer>();

            if (filter.Id != Guid.Empty)
            {
                predicate = predicate.And(customer => customer.Id == filter.Id);
            }
            if (!string.IsNullOrWhiteSpace(filter.IdentityNumber))
            {
                predicate = predicate.And(customer => customer.IdentityNumber == filter.IdentityNumber);
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicate = predicate.And(customer => customer.Name == filter.Name);
            }
            if (!string.IsNullOrWhiteSpace(filter.Surname))
            {
                predicate = predicate.And(customer => customer.Surname == filter.Surname);
            }
            if (filter.DateOfBirth != default)
            {
                predicate = predicate.And(customer => customer.Surname == filter.Surname);
            }
            if (!string.IsNullOrWhiteSpace(filter.TelephoneNumber))
            {
                predicate = predicate.And(customer => customer.TelephoneNumber == filter.TelephoneNumber);
            }

            return GetWhere(predicate);
        }

        public Task<Customer> UpdateCustomer(Guid id, string identityNumber, string name, string surname, DateTime dateOfBirth, string telephoneNumber, bool saveChanges = false)
        {
            var updatedCustomer = CustomerConverter.PropertiesToModel(identityNumber, name, surname, dateOfBirth, telephoneNumber, id);
            return Update(updatedCustomer, saveChanges);
        }

        public Task<bool> DeleteCustomersByFilter(Guid id = default, string identityNumber = default, string name = default, string surname = default, DateTime dateOfBirth = default, string telephoneNumber = default, bool saveChanges = false)
        {
            var filter = new CustomerFilter()
            {
                Id = id,
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            };
            var predicate = PredicateBuilder.True<Customer>();

            if (filter.Id != Guid.Empty)
            {
                predicate = predicate.And(customer => customer.Id == filter.Id);
            }
            if (!string.IsNullOrWhiteSpace(filter.IdentityNumber))
            {
                predicate = predicate.And(customer => customer.IdentityNumber == filter.IdentityNumber);
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicate = predicate.And(customer => customer.Name == filter.Name);
            }
            if (!string.IsNullOrWhiteSpace(filter.Surname))
            {
                predicate = predicate.And(customer => customer.Surname == filter.Surname);
            }
            if (filter.DateOfBirth != default)
            {
                predicate = predicate.And(customer => customer.Surname == filter.Surname);
            }
            if (!string.IsNullOrWhiteSpace(filter.TelephoneNumber))
            {
                predicate = predicate.And(customer => customer.TelephoneNumber == filter.TelephoneNumber);
            }

            return DeleteWhere(predicate, saveChanges);
        }
    }
}
