using CarRental.Infrastructure.Business.Dtos;
using CarRental.Infrastructure.Business.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomer(CustomerDto customerDto)
        {
            var result = await _customerService.CreateCustomer(customerDto.IdentityNumber, customerDto.Name, customerDto.Surname, customerDto.DateOfBirth, customerDto.TelephoneNumber);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllCustomers();

            return Ok(result);
        }

        [HttpGet("/{id}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var result = await _customerService.GetCustomerById(id);

            return Ok(result);
        }

        [HttpPut("/{id}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomer(CustomerDto customerDto)
        {
            var result = await _customerService.EditCustomer(customerDto.Id, customerDto.IdentityNumber, customerDto.Name, customerDto.Surname, customerDto.DateOfBirth, customerDto.TelephoneNumber).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            var result = await _customerService.DeleteAllCustomers().ConfigureAwait(false);

            return Ok(result);
        }

        [HttpDelete("/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerById(Guid id)
        {
            var result = await _customerService.DeleteCustomerById(id).ConfigureAwait(false);

            return Ok(result);
        }
    }
}
