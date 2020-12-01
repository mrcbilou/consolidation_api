using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using Newtonsoft.Json.Linq;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly app_developmentContext _context;

        public CustomerController(app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.customers.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{company_contact_email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string company_contact_email)
        {
            var customer = await _context.customers.FindAsync(company_contact_email);

            if (customer == null)
            {
                return NotFound();
            }

            var customeremail = new JObject ();
            customeremail["Email"] = customer.company_contact_email;
            return Content (customeremail.ToString (), "application/json");
        }

        // GET: api/Customer/5
        // [HttpGet("{id}")]
        //   public async Task<ActionResult<Customer>> GetCustomer(long id)
        // {
        //     var customer = await _context.customers.FindAsync(id);

        //     if (customer == null)
        //     {
        //         return NotFound();
        //     }
        //     // Create a message to show the new status
        //     var status = new JObject ();
        //     status["status"] = customer.Id;
        //     return Content (status.ToString (), "application/json");
        // }

        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostCustomer(Customer customer)
        {
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(long id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(long id)
        {
            return _context.customers.Any(c => c.Id == id);
        }
    }
}
