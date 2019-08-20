using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WHVM.Database.Models;

namespace WHVM.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly HomeVideoDBContext _context;

        public SourcesController(HomeVideoDBContext context)
        {
            _context = context;
        }

        // GET: api/Sources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Source>>> GetSources()
        {
            List<Source> sourcesList = await _context.Sources
                .Include(source => source.Clips)
                .Include(source => source.SourceFormat)
                .ToListAsync();
            return sourcesList;
        }

        // GET: api/Sources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Source>> GetSource(int id)
        {
            var source = await _context.Sources
                .Include(s => s.Clips)
                .Include(s => s.SourceFormat).FirstAsync(s => s.SourceId == id);

            if (source == null)
            {
                return NotFound();
            }

            return source;
        }

        // PUT: api/Sources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSource(int id, Source source)
        {
            if (id != source.SourceId)
            {
                return BadRequest();
            }

            _context.Entry(source).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceExists(id))
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

        // POST: api/Sources
        [HttpPost]
        public async Task<ActionResult<Source>> PostSource(Source source)
        {
            _context.Sources.Add(source);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSource", new {id = source.SourceId}, source);
        }

        // DELETE: api/Sources/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Source>> DeleteSource(int id)
        {
            var source = await _context.Sources.FindAsync(id);
            if (source == null)
            {
                return NotFound();
            }

            _context.Sources.Remove(source);
            await _context.SaveChangesAsync();

            return source;
        }

        private bool SourceExists(int id)
        {
            return _context.Sources.Any(e => e.SourceId == id);
        }
    }
}