using CarRental.Core.Business.Dtos;
using CarRental.Core.Business.Exceptions;
using CarRental.Core.Business.Services;
using CarRental.Core.Domain.Context;
using CarRental.Core.Domain.Entities;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
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
        private readonly Mock<IDbContextTransaction> _mockDbContextTransaction = new();

        public CustomerServiceTests()
        {
            _mockUoW.Setup(uoW => uoW.GetRepository<ICustomerRepository>()).Returns(_mockCustomerRepository.Object);
            _mockUoW.Setup(uoW => uoW.BeginTransactionAsync()).ReturnsAsync(_mockDbContextTransaction.Object);
            _sut = new CustomerService(_mockUoW.Object);
        }

        #region CreateCustomer

        [Fact]
        public async Task CreateCustomer_WhenOperationsSucceed_ShouldCommitTransactionAsync()
        {
            var customer = InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber);
            _mockCustomerRepository.Setup(repository => repository.InsertCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber, false)).ReturnsAsync(customer);

            var result = await _sut.CreateCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber);

            _mockUoW.Verify(uoW => uoW.CommitAsync(_mockDbContextTransaction.Object), Times.Once());
        }

        [Fact]
        public async Task CreateCustomer_WhenAllParametersAreValid_ShouldReturnCustomerDtoWithProvidedValuesAsync()
        {
            var customer = InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber);
            _mockCustomerRepository.Setup(repository => repository.InsertCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber, false)).ReturnsAsync(customer);

            var result = await _sut.CreateCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber);

            Assert.Equal(identityNumber, result.IdentityNumber);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(dateOfBirth, result.DateOfBirth);
            Assert.Equal(telephoneNumber, result.TelephoneNumber);
        }

        [Fact]
        public Task CreateCustomer_WhenCustomerRepositoryReturnsNull_ShouldThrowCustomerNotCreatedException()
        {
            InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber);
            _mockCustomerRepository.Setup(repository => repository.InsertCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber, false)).ReturnsAsync(() => null);

            return Assert.ThrowsAsync<CustomerNotCreatedException>(() => _sut.CreateCustomer(identityNumber, name, surname, dateOfBirth, telephoneNumber));
        }

        #endregion
   
        #region GetAllCustomers

        [Fact]
        public async Task GetAllCustomers_WhenDatabaseIsNotEmpty_ShouldReturnListOfCustomerDtoAsync()
        {
            var customerOne = InitializeCustomer(out Guid id1, out string identityNumber1, out string name1, out string surname1, out DateTime dateOfBirth1, out string telephoneNumber1);
            var customerTwo = InitializeCustomer(out Guid id2, out string identityNumber2, out string name2, out string surname2, out DateTime dateOfBirth2, out string telephoneNumber2);
            var customerThree = InitializeCustomer(out Guid id3, out string identityNumber3, out string name3, out string surname3, out DateTime dateOfBirth3, out string telephoneNumber3);
            _mockCustomerRepository.Setup(repository => repository.GetAllCustomers()).ReturnsAsync(new List<Customer> { customerOne, customerTwo, customerThree });

            var result = await _sut.GetAllCustomers();

            Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(result);
            Assert.Equal(3, result.Count());
            Assert.IsType<CustomerDto>(result.ElementAt(0));
            Assert.IsType<CustomerDto>(result.ElementAt(1));
            Assert.IsType<CustomerDto>(result.ElementAt(2));
            Assert.Equal(id1, result.ElementAt(0).Id);
            Assert.Equal(id2, result.ElementAt(1).Id);
            Assert.Equal(id3, result.ElementAt(2).Id);
            Assert.Equal(identityNumber1, result.ElementAt(0).IdentityNumber);
            Assert.Equal(identityNumber2, result.ElementAt(1).IdentityNumber);
            Assert.Equal(identityNumber3, result.ElementAt(2).IdentityNumber);
            Assert.Equal(name1, result.ElementAt(0).Name);
            Assert.Equal(name2, result.ElementAt(1).Name);
            Assert.Equal(name3, result.ElementAt(2).Name);
            Assert.Equal(surname1, result.ElementAt(0).Surname);
            Assert.Equal(surname2, result.ElementAt(1).Surname);
            Assert.Equal(surname3, result.ElementAt(2).Surname);
            Assert.Equal(dateOfBirth1, result.ElementAt(0).DateOfBirth);
            Assert.Equal(dateOfBirth2, result.ElementAt(1).DateOfBirth);
            Assert.Equal(dateOfBirth3, result.ElementAt(2).DateOfBirth);
            Assert.Equal(telephoneNumber1, result.ElementAt(0).TelephoneNumber);
            Assert.Equal(telephoneNumber2, result.ElementAt(1).TelephoneNumber);
            Assert.Equal(telephoneNumber3, result.ElementAt(2).TelephoneNumber);
        }

        [Fact]
        public async Task GetAllCustomers_WhenDatabaseIsEmpty_ShouldReturnEmptyListOfCustomerDtoAsync()
        {
            _mockCustomerRepository.Setup(repository => repository.GetAllCustomers()).ReturnsAsync(new List<Customer>());

            var result = await _sut.GetAllCustomers();

            Assert.IsType<List<CustomerDto>>(result);
            Assert.False(result.Any());
        }

        #endregion

        #region GetCustomerById

        [Fact]
        public async Task GetCustomerById_WhenCustomerExists_ShouldReturnCustomerDtoAsync()
        {
            var customer = InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber);
            _mockCustomerRepository.Setup(repository => repository.GetCustomerById(id)).ReturnsAsync(customer);

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
        public Task GetCustomerById_WhenCustomerDoesNotExist_ShouldThrowCustomerNotFoundException()
        {
            var id = Guid.NewGuid();
            _mockCustomerRepository.Setup(repository => repository.GetCustomerById(id)).ReturnsAsync(() => null);

            return Assert.ThrowsAsync<CustomerNotFoundException>(async () => await _sut.GetCustomerById(id));
        }

        #endregion

        #region EditCustomer

        [Fact]
        public async Task EditCustomer_WhenOperationsSucceed_ShouldCommitTransactionAsync()
        {
            var customer = InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber);
            var updatedCustomer = InitializeCustomer(out Guid updatedId, out string updatedIdentityNumber, out string updatedName, out string updatedSurname, out DateTime updatedDateOfBirth, out string updatedTelephoneNumber);
            _mockCustomerRepository.Setup(repository => repository.UpdateCustomer(id, updatedIdentityNumber, updatedName, updatedSurname, updatedDateOfBirth, updatedTelephoneNumber, false)).ReturnsAsync(updatedCustomer);

            var result = await _sut.EditCustomer(id, updatedIdentityNumber, updatedName, updatedSurname, updatedDateOfBirth, updatedTelephoneNumber);

            _mockUoW.Verify(uoW => uoW.CommitAsync(_mockDbContextTransaction.Object), Times.Once());
        }

        [Fact]
        public async Task EditCustomer_WhenCustomerExists_ShouldReturnUpdatedCustomerDtoAsync()
        {
            var customer = InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber);
            var updatedCustomer = InitializeCustomer(out Guid updatedId, out string updatedIdentityNumber, out string updatedName, out string updatedSurname, out DateTime updatedDateOfBirth, out string updatedTelephoneNumber);
            _mockCustomerRepository.Setup(repository => repository.UpdateCustomer(id, updatedIdentityNumber, updatedName, updatedSurname, updatedDateOfBirth, updatedTelephoneNumber, false)).ReturnsAsync(updatedCustomer);

            var result = await _sut.EditCustomer(id, updatedIdentityNumber, updatedName, updatedSurname, updatedDateOfBirth, updatedTelephoneNumber);

            Assert.IsType<CustomerDto>(result);
            //Assert.Equal(id, result.Id);
            Assert.Equal(updatedIdentityNumber, result.IdentityNumber);
            Assert.Equal(updatedName, result.Name);
            Assert.Equal(updatedSurname, result.Surname);
            Assert.Equal(updatedDateOfBirth, result.DateOfBirth);
            Assert.Equal(updatedTelephoneNumber, result.TelephoneNumber);
        }

        [Fact]
        public Task EditCustomer_WhenRepositoryReturnsNull_ShouldThrowCustomerNotUpdatedException()
        {
            InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber);

            return Assert.ThrowsAsync<CustomerNotUpdatedException>(async () => await _sut.EditCustomer(id, identityNumber, name, surname, dateOfBirth, telephoneNumber));
        }

        #endregion

        #region DeleteAllCustomers

        [Fact]
        public async Task DeleteAllCustomers_WhenOperationsSucceed_ShouldCommitTransactionAsync()
        {
            _mockCustomerRepository.Setup(repository => repository.DeleteAllCustomers()).ReturnsAsync(true);

            var result = await _sut.DeleteAllCustomers();

            _mockUoW.Verify(uoW => uoW.CommitAsync(_mockDbContextTransaction.Object), Times.Once());
        }

        [Fact]
        public async Task DeleteAllCustomers_WhenDatabaseIsNotEmpty_ShouldReturnTrueAsync()
        {
            _mockCustomerRepository.Setup(repository => repository.DeleteAllCustomers()).ReturnsAsync(true);

            var result = await _sut.DeleteAllCustomers();

            Assert.True(result);
        }

        [Fact]
        public Task DeleteAllCustomers_WhenRepositoryReturnsFalse_ShouldThrowCustomerNotDeletedException()
        {
            _mockCustomerRepository.Setup(repository => repository.DeleteAllCustomers()).ReturnsAsync(false);

            return Assert.ThrowsAsync<CustomerNotDeletedException>(async () => await _sut.DeleteAllCustomers());
        }

        #endregion

        #region DeleteCustomerById

        [Fact]
        public async Task DeleteCustomerById_WhenOperationsSucceed_ShouldCommitTransactionAsync()
        {
            var id = Guid.NewGuid();
            _mockCustomerRepository.Setup(repository => repository.DeleteCustomerById(id)).ReturnsAsync(true);

            var result = await _sut.DeleteCustomerById(id);

            _mockUoW.Verify(uoW => uoW.CommitAsync(_mockDbContextTransaction.Object), Times.Once());
        }

        [Fact]
        public async Task DeleteCustomerById_WhenCustomerExists_ShouldReturnTrueAsync()
        {
            var id = Guid.NewGuid();
            _mockCustomerRepository.Setup(repository => repository.DeleteCustomerById(id)).ReturnsAsync(true);

            var result = await _sut.DeleteCustomerById(id);

            Assert.True(result);
        }

        [Fact]
        public Task DeleteCustomerById_WhenCustomerDoesNotExist_ShouldThrowCustomerNotDeletedException()
        {
            var id = Guid.NewGuid();
            _mockCustomerRepository.Setup(repository => repository.DeleteCustomerById(id)).ReturnsAsync(false);

            return Assert.ThrowsAsync<CustomerNotDeletedException>(async () => await _sut.DeleteCustomerById(id));
        }

        #endregion

        private static Customer InitializeCustomer(out Guid id, out string identityNumber, out string name, out string surname, out DateTime dateOfBirth, out string telephoneNumber)
        {
            id = Guid.NewGuid();
            identityNumber = GetRandomIdentityNumber();
            name = GetRandomCustomerName();
            surname = GetRandomCustomerSurname();
            dateOfBirth = GetRandomCustomerDateOfBirth();
            telephoneNumber = GetRandomCustomerTelephoneNumber();
            return new Customer
            {
                Id = id,
                IdentityNumber = identityNumber,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                TelephoneNumber = telephoneNumber
            };
        }

        private static string GetRandomIdentityNumber()
        {
            var identityNumbers = new List<string> { "12345678A", "87654321B", "18723654C", "78563412D" };
            return identityNumbers[new Random().Next(identityNumbers.Count)];
        }

        private static string GetRandomCustomerName()
        {
            var names = new List<string> { "John", "Paul", "George", "Ringo" };
            return names[new Random().Next(names.Count)];
        }

        private static string GetRandomCustomerSurname()
        {
            var surnames = new List<string> { "Lennon", "McCartney", "Harrison", "Starr" };
            return surnames[new Random().Next(surnames.Count)];
        }

        private static DateTime GetRandomCustomerDateOfBirth()
        {
            var datesOfBirth = new List<DateTime> { new(1989, 01, 31), new(1999, 12, 01), new(2009, 04, 12), new(2019, 08, 22) };
            return datesOfBirth[new Random().Next(datesOfBirth.Count)];
        }

        private static string GetRandomCustomerTelephoneNumber()
        {
            var telephoneNumbers = new List<string> { "+34 666 33 39 99", "+44 7700 900800", "+1 202-191-2132", "+81 50-801-3742" };
            return telephoneNumbers[new Random().Next(telephoneNumbers.Count)];
        }
    }
}
