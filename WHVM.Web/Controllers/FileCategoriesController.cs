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
    public class FileCategoriesController : ControllerBase
    {
        private readonly HomeVideoDBContext _context;

        public FileCategoriesController(HomeVideoDBContext context)
        {
            _context = context;
        }

        // GET: api/FileCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileCategory>>> GetFileCategories()
        {
            return await _context.FileCategories.ToListAsync();
        }

        // GET: api/FileCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FileCategory>> GetFileCategory(int id)
        {
            var fileCategory = await _context.FileCategories.FindAsync(id);

            if (fileCategory == null)
            {
                return NotFound();
            }

            return fileCategory;
        }

        // PUT: api/FileCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileCategory(int id, FileCategory fileCategory)
        {
            if (id != fileCategory.FileCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(fileCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileCategoryExists(id))
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

        // POST: api/FileCategories
        [HttpPost]
        public async Task<ActionResult<FileCategory>> PostFileCategory(FileCategory fileCategory)
        {
            _context.FileCategories.Add(fileCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFileCategory", new { id = fileCategory.FileCategoryId }, fileCategory);
        }

        // DELETE: api/FileCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FileCategory>> DeleteFileCategory(int id)
        {
            var fileCategory = await _context.FileCategories.FindAsync(id);
            if (fileCategory == null)
            {
                return NotFound();
            }

            _context.FileCategories.Remove(fileCategory);
            await _context.SaveChangesAsync();

            return fileCategory;
        }

        private bool FileCategoryExists(int id)
        {
            return _context.FileCategories.Any(e => e.FileCategoryId == id);
        }
    }
}
