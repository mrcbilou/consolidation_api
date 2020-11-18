  
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
    public class ColumnController : ControllerBase
    {
        private readonly app_developmentContext _context;

        public ColumnController(app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Column
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> Getcolumns()
        {
            return await _context.columns.ToListAsync();
        }

        // GET: api/Column/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumn(long id)
        {
            var column =  await _context.columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }
            // Create a message to show the new status
            var status = new JObject ();
            status["status"] = column.column_status;
            return Content (status.ToString (), "application/json");
        }

        // PUT: api/Column/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutColumn(long id, Column column)
        {
            var c = await _context.columns.FindAsync (id);
            if (c == null) 
            {
                return NotFound ();
            }

            c.column_status = column.column_status;

            _context.columns.Update (c);
            _context.SaveChanges ();
            // Create a message to show the new status
            var status = new JObject ();
            status["message"] = "The status of the Column with the id number #" + c.Id + " have been changed to " + c.column_status;
            return Content (status.ToString (), "application/json");
        }

    }
}
