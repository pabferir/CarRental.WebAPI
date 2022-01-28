using CarRental.Core.Domain.Context;
using CarRental.Core.Domain.Entities;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.SharedKernel.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork<CarRentalDbContext> _uoW;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(IUnitOfWork<CarRentalDbContext> uoW)
        {
            _uoW = uoW;
            _customerRepository = uoW.GetRepository<ICustomerRepository>();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            var result = await _customerRepository.InsertCustomer(customer, false).ConfigureAwait(false);
            await _uoW.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerRepository.GetCustomerWhere(null).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("/{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var result = await _customerRepository.GetCustomerWhere(customer => customer.Id == id).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("/{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            var result = await _customerRepository.UpdateCustomer(customer, false).ConfigureAwait(false);
            await _uoW.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, result);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            await _customerRepository.DeleteCustomerWhere(null, false).ConfigureAwait(false);
            var result = await _uoW.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerById(Guid id)
        {
            await _customerRepository.DeleteCustomerWhere(customer => customer.Id == id, false).ConfigureAwait(false);
            var result = await _uoW.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, result > 0);
        }
    }
}
