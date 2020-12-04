using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly app_developmentContext _context;
        public AddressController(app_developmentContext context)
        {
            _context = context;
        }
        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _context.addresses.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddresses(long id)
        {
            var addresses = await _context.addresses.FindAsync(id);
            if (addresses == null)
            {
                return NotFound();
            }
            return addresses;
        }
       // GET: https://localhost:3003/api/Address/customer/{ID}
        [HttpGet("customer/{customer_id}")]
        public ActionResult<List<Address>> GetAddressesByCustomer(long? customer_id)
        {
            List<Address> addressesCustAll = _context.addresses.ToList();
            List<Address> addressesFromCustomer = new List<Address>();
            foreach (Address address in addressesCustAll)
            {
                if (address.customer_id == customer_id)
                {
                    addressesFromCustomer.Add(address);
                }
            }
            return addressesFromCustomer;
        }
        // PUT: api/Addresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddresses(long id, Address addresses)
        {
            if (id != addresses.Id)
            {
                return BadRequest();
            }
            _context.Entry(addresses).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressesExists(id))
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
        //PUT - Edit customer's addresses portal week 11
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditAdresses(long id, AddressesInfo addressesInfo)
        {
            if (id != addressesInfo.Id)
            {
                return BadRequest();
            }
            var address = await _context.addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            address.type_of_address = addressesInfo.type_of_address;
            address.number_and_street = addressesInfo.number_and_street;
            address.suite_or_apartment = addressesInfo.suite_or_apartment;
            address.city = addressesInfo.city;
           
            address.postal_code = addressesInfo.postal_code;
            address.country = addressesInfo.country;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressesExists(id))
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
        // POST: api/Addresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddresses(Address addresses)
        {
            _context.addresses.Add(addresses);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAddresses", new { id = addresses.Id }, addresses);
        }
        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddresses(long id)
        {
            var addresses = await _context.addresses.FindAsync(id);
            if (addresses == null)
            {
                return NotFound();
            }
            _context.addresses.Remove(addresses);
            await _context.SaveChangesAsync();
            return addresses;
        }
        private bool AddressesExists(long id)
        {
            return _context.addresses.Any(e => e.Id == id);
        }
    }
}