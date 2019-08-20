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
    public class SourceFormatsController : ControllerBase
    {
        private readonly HomeVideoDBContext _context;

        public SourceFormatsController(HomeVideoDBContext context)
        {
            _context = context;
        }

        // GET: api/SourceFormats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SourceFormat>>> GetSourceFormats()
        {
            return await _context.SourceFormats.ToListAsync();
        }

        // GET: api/SourceFormats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SourceFormat>> GetSourceFormat(int id)
        {
            var sourceFormat = await _context.SourceFormats.FindAsync(id);

            if (sourceFormat == null)
            {
                return NotFound();
            }

            return sourceFormat;
        }

        // PUT: api/SourceFormats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSourceFormat(int id, SourceFormat sourceFormat)
        {
            if (id != sourceFormat.SourceFormatId)
            {
                return BadRequest();
            }

            _context.Entry(sourceFormat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceFormatExists(id))
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

        // POST: api/SourceFormats
        [HttpPost]
        public async Task<ActionResult<SourceFormat>> PostSourceFormat(SourceFormat sourceFormat)
        {
            _context.SourceFormats.Add(sourceFormat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSourceFormat", new { id = sourceFormat.SourceFormatId }, sourceFormat);
        }

        // DELETE: api/SourceFormats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SourceFormat>> DeleteSourceFormat(int id)
        {
            var sourceFormat = await _context.SourceFormats.FindAsync(id);
            if (sourceFormat == null)
            {
                return NotFound();
            }

            _context.SourceFormats.Remove(sourceFormat);
            await _context.SaveChangesAsync();

            return sourceFormat;
        }

        private bool SourceFormatExists(int id)
        {
            return _context.SourceFormats.Any(e => e.SourceFormatId == id);
        }
    }
}
