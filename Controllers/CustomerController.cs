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



        // [HttpGet("[action]/{Id}")] 
        // public ActionResult<List<Customer>> GetAll (long Id) {
        //     var customlist = _context.customers.ToList(); // List of all the buildings in the database
        //     var buildlist = _context.buildings.ToList(); // List of all the buildings in the database
        //     var battlist = _context.batteries.ToList(); // List of all the buildings in the database
        //     var columnlist = _context.columns.ToList(); // List of all the columns in the database
        //     var elevatorlist = _context.elevators.ToList(); // List of all the elevators in the database

        //     List<Elevator> list_elev = new List<Elevator>(); // Elevators will be added in this list if they respect the requirements (If they have an intervention status)
        //     List<Column> list_col = new List<Column>(); // Columns will be added in this list if they respect the requirements (If they have an intervention status)
        //     List<Battery> list_batt = new List<Battery>(); // Batteries will be added in this list if they respect the requirements (If they have an intervention status)
        //     List<Building> list_build = new List<Building>(); // // Buildings will be added in this list if they have at least a battery, column or elevator with an intervention status.
        //     List<Customer> list_customer = new List<Customer> (); // Same as list_lead but with no duplicate
            


        //     if (customlist == null) {
        //         return NotFound ("Not Found");
        //     }


        //     // Add columns in the list if they have an elevator with an intervention status
        //     foreach (var elevator in list_elev){
        //         foreach (var column in columnlist){
        //             if (column.Id == elevator.column_id){
        //                 list_col.Add(column);
        //             }
        //         }
        //     }


        //     foreach ( var column in list_col){
        //         foreach (var elevator in elevatorlist ){
        //             if (column.Id == elevator.column_id){
        //                 list_elev.Add(elevator);
        //             }
        //         }
        //     }
            
        //     // Add batteries in the list if they have a column with an intervention status
        //     foreach (var column in list_col){
        //         foreach (var battery in battlist){
        //             if (battery.Id == column.battery_id){
        //                 list_batt.Add(battery);
        //             }
        //         }
        //     }
           
        //     // Add buildings in the list if they have a battery with an intervention status
        //     foreach (var battery in list_batt){
        //         foreach (var building in buildlist){
        //             if (building.Id == battery.building_id){
        //                list_build.Add(building); 
        //             }
        //         }
        //     }

        //     foreach (var customer in list_customer){
        //             foreach (var building in buildlist)
        //             {
        //                 if (building.Id == customer.Id)
        //                 {
        //                  list_customer.Add(customer); 
        //                 }
        //             }
        //     }
        

        //     // Remove any duplicate buildings in the list
        //     List<Customer> all_customer = list_customer.Distinct().ToList();
        //     return all_customer;
         
        // }


        // // https://localhost:3003/api/Customer/GetAllCustomerBuildings/1
        // [HttpGet("[action]/{Id}")]
        // public ActionResult<IEnumerable<Building>> GetAllCustomerBuildings (long Id )
        // {
        //     // _context = datatbase   buildings = table references 
        //     var customer = _context.buildings.Where(x => x.customer_id == Id).ToList();

        //     return customer;
        // }


        // // https://localhost:3003/api/Customer/GetAllCustomerBatteries/1
        // [HttpGet("[action]/{Id}")]  
        // public ActionResult<IEnumerable<Battery>> GetAllCustomerBatteries (long Id )
        // {
        //     // _context = datatbase   buildings = table references 
        //     var battery = _context.batteries.Where(x => x.building_id == Id).ToList();

        //     return battery;
        // }

        // // https://localhost:3003/api/Customer/GetAllCustomerColumns/1
        // [HttpGet("[action]/{Id}")]   
        // public ActionResult<IEnumerable<Column>> GetAllCustomerColumns (long Id )
        // {
        //     // _context = datatbase   buildings = table references 
        //     var column = _context.columns.Where(x => x.battery_id == Id).ToList();

        //     return column;
        // }
        
        // // https://localhost:3003/api/Customer/GetAllCustomerElevators/1
        // [HttpGet("[action]/{Id}")]
        // public ActionResult<IEnumerable<Elevator>> GetAllCustomerElevators (long Id )
        // {
        //     // _context = datatbase   buildings = table references 
        //     var elevator = _context.elevators.Where(x => x.column_id == Id).ToList();

        //     return elevator;
        // }

        // GET: api/Customer/find/{email}
        [HttpGet("find/{company_contact_email}")]
        public ActionResult<Customer> GetCustomerEmail(string company_contact_email)
        {
            var decodedEmail = System.Web.HttpUtility.UrlDecode(company_contact_email);
            Console.WriteLine(decodedEmail);
            var customerEmail = _context.customers
            .Where(c => c.company_contact_email == decodedEmail);
            //.FirstOrDefaultAsync();
            if (customerEmail == null)
            {
                return NotFound();
            }
            return Ok(customerEmail);
        }

       



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
