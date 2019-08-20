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
    public class FileFormatsController : ControllerBase
    {
        private readonly HomeVideoDBContext _context;

        public FileFormatsController(HomeVideoDBContext context)
        {
            _context = context;
        }

        // GET: api/FileFormats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileFormat>>> GetFileFormats()
        {
            return await _context.FileFormats.ToListAsync();
        }

        // GET: api/FileFormats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FileFormat>> GetFileFormat(int id)
        {
            var fileFormat = await _context.FileFormats.FindAsync(id);

            if (fileFormat == null)
            {
                return NotFound();
            }

            return fileFormat;
        }

        // PUT: api/FileFormats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileFormat(int id, FileFormat fileFormat)
        {
            if (id != fileFormat.FileFormatId)
            {
                return BadRequest();
            }

            _context.Entry(fileFormat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileFormatExists(id))
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

        // POST: api/FileFormats
        [HttpPost]
        public async Task<ActionResult<FileFormat>> PostFileFormat(FileFormat fileFormat)
        {
            _context.FileFormats.Add(fileFormat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFileFormat", new { id = fileFormat.FileFormatId }, fileFormat);
        }

        // DELETE: api/FileFormats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FileFormat>> DeleteFileFormat(int id)
        {
            var fileFormat = await _context.FileFormats.FindAsync(id);
            if (fileFormat == null)
            {
                return NotFound();
            }

            _context.FileFormats.Remove(fileFormat);
            await _context.SaveChangesAsync();

            return fileFormat;
        }

        private bool FileFormatExists(int id)
        {
            return _context.FileFormats.Any(e => e.FileFormatId == id);
        }
    }
}
