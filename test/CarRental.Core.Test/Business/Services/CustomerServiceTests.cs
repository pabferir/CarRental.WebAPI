using CarRental.Core.Business.Dtos;
using CarRental.Core.Business.Exceptions;
using CarRental.Core.Business.Services;
using CarRental.Core.Domain.Context;
using CarRental.Core.Domain.Entities;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.SharedKernel.UnitOfWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Core.Test.Business.Services
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _sut;
        private readonly Mock<IUnitOfWork<CarRentalDbContext>> _mockUoW = new();
        private readonly Mock<ICustomerRepository> _mockCustomerRepository = new();

        public CustomerServiceTests()
        {
            _mockUoW.Setup(uoW => uoW.GetRepository<ICustomerRepository>()).Returns(_mockCustomerRepository.Object);
            _sut = new CustomerService(_mockUoW.Object);
        }

        [Fact]
        public async Task CreateCustomer_ShouldReturnCustomerDto_WhenAllParametersAreValid_AndSaveChangesIsFalse_v2()
        {
            Guid id = Guid.NewGuid();
            const string? identityNumber = "12345678A";
            const string? name = "John";
            const string? surname = "Doe";
            DateTime dateOfBirth = new(1999, 01, 01);
            const string? telephoneNumber = "900900900";
            Customer? customer = new()
            {
                Id = id,
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            };
            _mockCustomerRepository.Setup(repository => repository.InsertCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber, false)).ReturnsAsync(customer);
            _mockUoW.Setup(uoW => uoW.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _sut.CreateCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber);

            Assert.Equal(identityNumber, result.IdentityNumber);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(dateOfBirth, result.DateOfBirth);
            Assert.Equal(telephoneNumber, result.TelephoneNumber);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnListOfCustomerDto_WhenDatabaseIsNotEmpty()
        {
            var id = Guid.NewGuid();
            var identityNumber = "12345678A";
            var name = "John";
            var surname = "Doe";
            var dateOfBirth = new DateTime(1999, 01, 01);
            var telephoneNumber = "900900900";
            var customer = new Customer
            {
                Id = id,
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            };
            var dbResult = new List<Customer> { customer, customer, customer };
            _mockCustomerRepository.Setup(repository => repository.GetCustomerWhere(null)).ReturnsAsync(dbResult);

            var result = await _sut.GetAllCustomers();

            Assert.IsType<CustomerDto>(result.ElementAt(0));
            Assert.IsType<CustomerDto>(result.ElementAt(1));
            Assert.IsType<CustomerDto>(result.ElementAt(2));
            Assert.Equal(3, result.Count());
            Assert.Equal(id, result.ElementAt(0).Id);
            Assert.Equal(id, result.ElementAt(1).Id);
            Assert.Equal(id, result.ElementAt(2).Id);
            Assert.Equal(identityNumber, result.ElementAt(0).IdentityNumber);
            Assert.Equal(identityNumber, result.ElementAt(1).IdentityNumber);
            Assert.Equal(identityNumber, result.ElementAt(2).IdentityNumber);
            Assert.Equal(name, result.ElementAt(0).Name);
            Assert.Equal(name, result.ElementAt(1).Name);
            Assert.Equal(name, result.ElementAt(2).Name);
            Assert.Equal(surname, result.ElementAt(0).Surname);
            Assert.Equal(surname, result.ElementAt(1).Surname);
            Assert.Equal(surname, result.ElementAt(2).Surname);
            Assert.Equal(dateOfBirth, result.ElementAt(0).DateOfBirth);
            Assert.Equal(dateOfBirth, result.ElementAt(1).DateOfBirth);
            Assert.Equal(dateOfBirth, result.ElementAt(2).DateOfBirth);
            Assert.Equal(telephoneNumber, result.ElementAt(0).TelephoneNumber);
            Assert.Equal(telephoneNumber, result.ElementAt(1).TelephoneNumber);
            Assert.Equal(telephoneNumber, result.ElementAt(2).TelephoneNumber);
        }

        [Fact]
        public void GetAllCUstomers_ShouldThrowCustomerNotFoundException_WhenDatabaseIsEmpty()
        {
            var dbResult = new List<Customer>();
            _mockCustomerRepository.Setup(repository => repository.GetCustomerWhere(null)).ReturnsAsync(dbResult);

            Assert.ThrowsAsync<CustomerNotFoundException>(async () => await _sut.GetAllCustomers());
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnCustomerDto_WhenCustomerExists()
        {
            var id = Guid.NewGuid();
            var identityNumber = "12345678A";
            var name = "John";
            var surname = "Doe";
            var dateOfBirth = new DateTime(1999, 01, 01);
            var telephoneNumber = "900900900";
            var customer = new Customer
            {
                Id = id,
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            };
            var dbResult = new List<Customer> { customer };
            _mockCustomerRepository.Setup(repository => repository.GetCustomerWhere(customer => customer.Id == id)).ReturnsAsync(dbResult);

            var result = await _sut.GetCustomerById(id);

            Assert.IsType<CustomerDto>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(identityNumber, result.IdentityNumber);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(dateOfBirth, result.DateOfBirth);
            Assert.Equal(telephoneNumber, result.TelephoneNumber);
        }

        [Fact]
        public void GetCustomerById_ShouldThrowCustomerNotFoundException_WhenCustomerDoesNotExist()
        {
            var id = Guid.NewGuid();
            var dbResult = new List<Customer>();
            _mockCustomerRepository.Setup(repository => repository.GetCustomerWhere(customer => customer.Id == id)).ReturnsAsync(dbResult);

            Assert.ThrowsAsync<CustomerNotFoundException>(async () => await _sut.GetCustomerById(id));
        }

        [Fact]
        public async Task EditCustomer_ShouldReturnUpdatedCustomerDto_WhenCustomerExists_AndSaveChangesIsFalse()
        {
            var id = Guid.Empty;
            var identityNumber = "12345678A";
            var name = "John";
            var surname = "Doe";
            var dateOfBirth = new DateTime(1999, 01, 01);
            var telephoneNumber = "900900900";
            var customer = new Customer
            {
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            };
            var dbResult = new List<Customer> { customer };
            var updatedIdentityNumber = "87654321B";
            var updatedName = "Jane";
            var updatedSurname = "Roe";
            var updatedDateOfBirth = new DateTime(2000, 01, 01);
            var updatedTelephoneNumber = "099099099";
            var updatedCustomer = new Customer
            {
                IdentityNumber = updatedIdentityNumber,
                Name = updatedName,
                Surname = updatedSurname,
                DateOfBirth = updatedDateOfBirth,
                TelephoneNumber = updatedTelephoneNumber
            };
            _mockCustomerRepository.Setup(repository => repository.GetCustomerWhere(customer => customer.Id == id)).ReturnsAsync(dbResult);
            _mockCustomerRepository.Setup(repository => repository.UpdateCustomer(id, updatedIdentityNumber, updatedName, updatedSurname, updatedDateOfBirth, updatedTelephoneNumber, false)).ReturnsAsync(updatedCustomer);
            _mockUoW.Setup(uoW => uoW.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _sut.EditCustomer(id, updatedIdentityNumber, updatedName, updatedSurname, updatedDateOfBirth, updatedTelephoneNumber);

            Assert.IsType<CustomerDto>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(updatedIdentityNumber, result.IdentityNumber);
            Assert.Equal(updatedName, result.Name);
            Assert.Equal(updatedSurname, result.Surname);
            Assert.Equal(updatedDateOfBirth, result.DateOfBirth);
            Assert.Equal(updatedTelephoneNumber, result.TelephoneNumber);
            Assert.NotEqual(identityNumber, result.IdentityNumber);
            Assert.NotEqual(name, result.Name);
            Assert.NotEqual(surname, result.Surname);
            Assert.NotEqual(dateOfBirth, result.DateOfBirth);
            Assert.NotEqual(telephoneNumber, result.TelephoneNumber);
        }

        [Fact]
        public void EditCustomer_ShouldThrowCustomerNotFoundException_WhenCustomerDoesNotExist_AndSaveChangesIsFalse()
        {
            var id = Guid.NewGuid();
            var identityNumber = "12345678A";
            var name = "John";
            var surname = "Doe";
            var dateOfBirth = new DateTime(1999, 01, 01);
            var telephoneNumber = "900900900";
            var dbResult = new List<Customer>();
            _mockCustomerRepository.Setup(repository => repository.GetCustomerWhere(customer => customer.Id == id)).ReturnsAsync(dbResult);

            Assert.ThrowsAsync<CustomerNotFoundException>(async () => await _sut.EditCustomer(id, identityNumber, name, surname, dateOfBirth, telephoneNumber));
        }

        [Fact]
        public async Task DeleteAllCustomers_ShouldReturnTrue_WhenDatabaseIsNotEmpty_AndSaveChangesIsFalse()
        {
            _mockCustomerRepository.Setup(repository => repository.DeleteCustomerWhere(null, false)).ReturnsAsync(false);
            _mockUoW.Setup(uoW => uoW.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _sut.DeleteAllCustomers();

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAllCustomers_ShouldReturnFalse_WhenDatabaseIsEmpty_AndSaveChangesIsFalse()
        {
            _mockCustomerRepository.Setup(repository => repository.DeleteCustomerWhere(null, false)).ReturnsAsync(false);
            _mockUoW.Setup(uoW => uoW.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _sut.DeleteAllCustomers();

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteCustomerById_ShouldReturnTrue_WhenCustomerExists_AndSaveChangesIsFalse()
        {
            var id = Guid.NewGuid();
            _mockCustomerRepository.Setup(repository => repository.DeleteCustomerWhere(customer => customer.Id == id, false)).ReturnsAsync(false);
            _mockUoW.Setup(uoW => uoW.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _sut.DeleteCustomerById(id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCustomerById_ShouldReturnFalse_WhenCustomerDoesNotExist_AndSaveChangesIsFalse()
        {
            var id = Guid.NewGuid();
            _mockCustomerRepository.Setup(repository => repository.DeleteCustomerWhere(customer => customer.Id == id, false)).ReturnsAsync(false);
            _mockUoW.Setup(uoW => uoW.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _sut.DeleteCustomerById(id);

            Assert.False(result);
        }
    }
}
