using CarRental.Infrastructure.Business.Dtos;
using CarRental.Infrastructure.Business.ServiceInterfaces;
using CarRental.Infrastructure.Business.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMediator _mediator;

        public CustomersController(ICustomerService customerService, IMediator mediator)
        {
            _customerService = customerService;
            _mediator = mediator;
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
            var response = await _mediator.Send(new GetAllCustomers.Query());

            if (response == null)
                return NoContent();

            return Ok(response);
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
