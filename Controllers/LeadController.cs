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
    public class LeadController : ControllerBase
    {
        private readonly app_developmentContext _context;

        public LeadController(app_developmentContext context)
        {
            _context = context;
        }
            public ActionResult<List<Lead>> GetAll () {
            var listl = _context.leads.ToList(); // List of all the leads in the database
            var UserAll = _context.users.ToList(); // List of all the users in the database
            var CustomerAll = _context.customers.ToList(); // List of all the customers in the database

            if (listl == null) {
                return NotFound ("Not Found");
            }

            List<Lead> list_lead = new List<Lead> (); // Leads will be added in the list if they have been created in the last 30 days.
            List<Lead> list_l = new List<Lead> (); // Same as list_lead but with no duplicate

            // Contains the date that is 30 days from current day
            DateTime currentDate = DateTime.Now.AddDays (-30);

            // Find the list of leads created in the last 30 days
            foreach (var l in listl) {

                if (l.created_at >= currentDate) {

                    list_lead.Add (l);
                }
            }
            // Find the list of leads with no customer attached to it
            foreach (var lead in list_lead)
            {
                foreach (var customer in CustomerAll)
                {
                    if (lead.user_id != customer.user_id) 
                    {
                        list_l.Add(lead);
                    }
                }
            }
            // Remove duplicates in the list
            List<Lead> final_list = list_l.Distinct().ToList();
            
            return final_list;
        }
    }
}
