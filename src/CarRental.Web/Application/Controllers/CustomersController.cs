using CarRental.Core.Domain.Entities;
using CarRental.Infrastructure.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public CustomersController(CarRentalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            var result = await _context.Customers.AddAsync(customer).ConfigureAwait(false);
            result.State = EntityState.Added;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            result.State = EntityState.Detached;
            return StatusCode(StatusCodes.Status201Created, customer);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _context.Customers.ToListAsync().ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet("/{id}")]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("/{id}")]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            var updatedCustomer = new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                DateOfBirth = customer.DateOfBirth,
                IdentityNumber = customer.IdentityNumber,
                TelephoneNumber = customer.TelephoneNumber
            };
            var result = _context.Update(updatedCustomer);
            result.State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            result.State = EntityState.Detached;
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            var deleted = 0;
            var customers = await _context.Customers.ToListAsync().ConfigureAwait(false);
            if (!customers.Any())
            {
                return Ok();
            }
            foreach (var customer in customers)
            {
                var result = _context.Customers.Remove(customer);
                result.State = EntityState.Deleted;
                deleted = await _context.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
                if (deleted <= 0)
                {
                    StatusCode(StatusCodes.Status500InternalServerError, deleted > 0);
                }
            }
            return StatusCode(StatusCodes.Status200OK, deleted > 0);
        }

        [HttpDelete("/{id}")]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerById(Guid id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            if (customer == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            var result = _context.Customers.Remove(customer);
            result.State = EntityState.Deleted;
            var deleted = await _context.SaveChangesAsync().ConfigureAwait(false);
            result.State = EntityState.Detached;
            return StatusCode(StatusCodes.Status200OK, deleted > 0);
        }
    }
}
