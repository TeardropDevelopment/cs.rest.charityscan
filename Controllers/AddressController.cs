using cs.api.charityscan.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cs.api.charityscan.Controllers
{
    [ApiController]
    [Route("~/api/v1/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly CharityscanDevContext _context;

        public AddressController(CharityscanDevContext context)
        {
            _context = context;
        }

        #region GET

        // GET: Addresses
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.Addresses != null ?
                        StatusCode(200, await _context.Addresses.ToListAsync()) :
                        Problem("Entity set 'CharityscanDevContext.Addresses'  is null.");
        }

        // GET: Addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var e = await _context.Addresses
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (e == null)
            {
                return NotFound();
            }

            return StatusCode(200, e);
        }

        #endregion

        #region POST

        // POST: Addresses/Create
        /// <summary>
        /// Return the ID of the created Event
        /// </summary>
        /// <param name="e">Event</param>
        /// <returns><int>id</int></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, address.EventId);
            }
            return StatusCode(StatusCodes.Status406NotAcceptable, address);
        }

        #endregion

        #region PUT

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Address address)
        {
            if (id != address.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.EventId))
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
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        #endregion

        #region DELETE

        // GET: Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var rowsDeleted = await (from entity in _context.Addresses
                                     where entity.EventId == id
                                     select entity).ExecuteDeleteAsync();

            if (rowsDeleted == 0)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        #endregion

        #region HELPERS

        private bool AddressExists(int id)
        {
            return (_context.Addresses?.Any(e => e.EventId == id)).GetValueOrDefault();
        }

        #endregion
    }
}