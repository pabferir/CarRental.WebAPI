using CarRental.Core.Domain.Entities;
using CarRental.Core.Domain.RepositoryInterfaces;
using CarRental.Infrastructure.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            var result = await _customerRepository.InsertCustomer(customer).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerRepository.GetCustomer().ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("/{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var result = await _customerRepository.GetCustomer(customer => customer.Id == id).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("/{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            var result = await _customerRepository.UpdateCustomer(customer).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            var result = await _customerRepository.DeleteCustomer().ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerById(Guid id)
        {
            var result = await _customerRepository.DeleteCustomer(customer => customer.Id == id).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
