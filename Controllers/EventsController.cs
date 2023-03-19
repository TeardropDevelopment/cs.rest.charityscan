using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cs.api.charityscan.Entities;

namespace cs.api.charityscan.Controllers
{
    [ApiController]
    [Route("~/api/v1/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly CharityscanDevContext _context;

        public EventsController(CharityscanDevContext context)
        {
            _context = context;
        }

        // GET: Events
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Events != null ?
                          StatusCode(200, await _context.Events.ToListAsync()) :
                          Problem("Entity set 'CharityscanDevContext.Events'  is null.");
        }

        // GET: events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var e = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (e == null)
            {
                return NotFound();
            }

            return StatusCode(200, e);
        }

        // POST: Events/Create
        /// <summary>
        /// Return the ID of the created Event
        /// </summary>
        /// <param name="e">Event</param>
        /// <returns><int>id</int></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(Event e)
        {
            if (ModelState.IsValid)
            {
                _context.Add(e);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, e.Id);
            }
            return StatusCode(StatusCodes.Status406NotAcceptable, e);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Event e)
        {
            if (id != e.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(e);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(e.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction();
            }
            return StatusCode(StatusCodes.Status200OK);
        }

        // GET: Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var rowsDeleted = await (from entity in _context.Events
                           where entity.Id == id
                           select entity).ExecuteDeleteAsync();

            if (rowsDeleted == 0)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
