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
    public class EventCodesController : ControllerBase
    {
        private readonly CharityscanDevContext _context;

        public EventCodesController(CharityscanDevContext context)
        {
            _context = context;
        }

        // GET: api/EventCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventCode>>> GetEventCodes()
        {
          if (_context.EventCodes == null)
          {
              return NotFound();
          }
            return await _context.EventCodes.ToListAsync();
        }

        // GET: api/EventCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventCode>> GetEventCode(int id)
        {
          if (_context.EventCodes == null)
          {
              return NotFound();
          }
            var eventCode = await _context.EventCodes.FindAsync(id);

            if (eventCode == null)
            {
                return NotFound();
            }

            return eventCode;
        }

        // PUT: api/EventCodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventCode(int id, EventCode eventCode)
        {
            if (id != eventCode.EventId)
            {
                return BadRequest();
            }

            _context.Entry(eventCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventCodeExists(id))
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

        // POST: api/EventCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventCode>> PostEventCode(EventCode eventCode)
        {
          if (_context.EventCodes == null)
          {
              return Problem("Entity set 'CharityscanDevContext.EventCodes'  is null.");
          }
            _context.EventCodes.Add(eventCode);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventCodeExists(eventCode.EventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEventCode", new { id = eventCode.EventId }, eventCode);
        }

        // DELETE: api/EventCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventCode(int id)
        {
            if (_context.EventCodes == null)
            {
                return NotFound();
            }
            var eventCode = await _context.EventCodes.FindAsync(id);
            if (eventCode == null)
            {
                return NotFound();
            }

            _context.EventCodes.Remove(eventCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventCodeExists(int id)
        {
            return (_context.EventCodes?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
