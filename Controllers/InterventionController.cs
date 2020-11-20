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

        // GET: api/Intervention
        [HttpGet]
        public async Task<ActionResult<List<Intervention>>> GetAll()
        {
            
            var list = _context.interventions.ToList(); // list of all interventions
            
            if (list == null)
            {
                return NotFound();
            }

            List<Intervention> pending_intervention_list = new List<Intervention>(); // Interventions will be added in this list if they respect the requirements (If they have a pending status and no start date.)
            
            foreach (var intervention in list)
            {
                if(intervention.intervention_status == "Pending" && intervention.start_date_and_time == null) {
                    pending_intervention_list.Add (intervention);
                }
            }
            return pending_intervention_list;

        }

        // GET: api/Intervention/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(int id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

         

            // Create a message to show the new status
            var interstatus = new JObject ();
            interstatus["string"] = intervention.Id;
            return Content (interstatus.ToString (), "application/json");
        }

        // PUT: api/Battery/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(int id, Intervention intervention)
        {
            var b = await _context.interventions.FindAsync (id);
            if (b == null) {
                return NotFound ();
            }
            
            b.intervention_status = intervention.intervention_status;
            b.start_date_and_time = intervention.start_date_and_time;
            b.end_date_and_time = intervention.end_date_and_time;

            if (b.intervention_status == "InProgress"){
                b.start_date_and_time = DateTime.Today;
            }

            if (b.intervention_status == "Completed"){
                b.end_date_and_time = DateTime.Today;
            }

            _context.interventions.Update (b);
            _context.SaveChanges ();
            // Create a message to show the new status 
            var intstatus = new JObject ();
            intstatus["message"] = "The status of the Intervention with the id number #" + b.Id + " have been changed to " + b.intervention_status +" at Date:" + b.start_date_and_time + b.end_date_and_time ;
            return Content (intstatus.ToString (), "application/json");

        }
            
    }
}
