using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WHVM.Database.Models;

namespace WHVM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly HomeVideoDBContext _context;

        public CollectionsController(HomeVideoDBContext context)
        {
            _context = context;
        }

        // GET: api/Collections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {
            return await _context.Collections.ToListAsync();
        }

        // GET: api/Collections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collection>> GetCollection(int id)
        {
            var collection = await _context.Collections.FindAsync(id);

            if (collection == null)
            {
                return NotFound();
            }

            return collection;
        }

        // PUT: api/Collections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {
            if (id != collection.CollectionId)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(id))
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

        // POST: api/Collections
        [HttpPost]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollection", new { id = collection.CollectionId }, collection);
        }

        // DELETE: api/Collections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Collection>> DeleteCollection(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();

            return collection;
        }

        private bool CollectionExists(int id)
        {
            return _context.Collections.Any(e => e.CollectionId == id);
        }
    }
}
