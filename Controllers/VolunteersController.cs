using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs.api.charityscan.Entities;

namespace cs.api.charityscan.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {
        private readonly CharityscanDevContext _context;

        public VolunteersController(CharityscanDevContext context)
        {
            _context = context;
        }

        // GET: api/Volunteers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Volunteer>>> GetVolunteers()
        {
          if (_context.Volunteers == null)
          {
              return NotFound();
          }
            return await _context.Volunteers.ToListAsync();
        }

        // GET: api/Volunteers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Volunteer>> GetVolunteer(int id)
        {
          if (_context.Volunteers == null)
          {
              return NotFound();
          }
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return volunteer;
        }

        // PUT: api/Volunteers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolunteer(int id, Volunteer volunteer)
        {
            if (id != volunteer.Id)
            {
                return BadRequest();
            }

            _context.Entry(volunteer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
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

        // POST: api/Volunteers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Volunteer>> PostVolunteer(Volunteer volunteer)
        {
          if (_context.Volunteers == null)
          {
              return Problem("Entity set 'CharityscanDevContext.Volunteers'  is null.");
          }
            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteer", new { id = volunteer.Id }, volunteer);
        }

        // DELETE: api/Volunteers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteer(int id)
        {
            if (_context.Volunteers == null)
            {
                return NotFound();
            }
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VolunteerExists(int id)
        {
            return (_context.Volunteers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
