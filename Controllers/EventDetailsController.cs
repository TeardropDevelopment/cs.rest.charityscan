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
    public class EventDetailsController : ControllerBase
    {
        private readonly CharityscanDevContext _context;

        public EventDetailsController(CharityscanDevContext context)
        {
            _context = context;
        }

        // GET: api/EventDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDetail>>> GetEventDetails()
        {
          if (_context.EventDetails == null)
          {
              return NotFound();
          }
            return await _context.EventDetails.ToListAsync();
        }

        // GET: api/EventDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetail>> GetEventDetail(int id)
        {
          if (_context.EventDetails == null)
          {
              return NotFound();
          }
            var eventDetail = await _context.EventDetails.FindAsync(id);

            if (eventDetail == null)
            {
                return NotFound();
            }

            return eventDetail;
        }

        // PUT: api/EventDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventDetail(int id, EventDetail eventDetail)
        {
            if (id != eventDetail.EventId)
            {
                return BadRequest();
            }

            _context.Entry(eventDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventDetailExists(id))
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

        // POST: api/EventDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventDetail>> PostEventDetail(EventDetail eventDetail)
        {
          if (_context.EventDetails == null)
          {
              return Problem("Entity set 'CharityscanDevContext.EventDetails'  is null.");
          }
            _context.EventDetails.Add(eventDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventDetailExists(eventDetail.EventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEventDetail", new { id = eventDetail.EventId }, eventDetail);
        }

        // DELETE: api/EventDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventDetail(int id)
        {
            if (_context.EventDetails == null)
            {
                return NotFound();
            }
            var eventDetail = await _context.EventDetails.FindAsync(id);
            if (eventDetail == null)
            {
                return NotFound();
            }

            _context.EventDetails.Remove(eventDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventDetailExists(int id)
        {
            return (_context.EventDetails?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
