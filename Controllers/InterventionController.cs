using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RestApi.Models;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
        private readonly app_developmentContext _context;

        public InterventionController(app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Battery
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            return await _context.interventions.ToListAsync();
        }

        // GET: api/Battery/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }
            // Create a message to show the new status
            var status = new JObject ();
            status["status"] = intervention.status;
            return Content (status.ToString (), "application/json");
        }

        // PUT: api/Battery/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(long id, Intervention intervention)
        {
            var b = await _context.interventions.FindAsync (id);
            if (b == null) {
                return NotFound ();
            }
            
            b.status = intervention.status;

            _context.interventions.Update (b);
            _context.SaveChanges ();
            // Create a message to show the new status
            var status = new JObject ();
            status["message"] = "The status of the Intervention with the id number #" + b.Id + " have been changed to " + intervention.status;
            return Content (status.ToString (), "application/json");

        }
            
    }
}
