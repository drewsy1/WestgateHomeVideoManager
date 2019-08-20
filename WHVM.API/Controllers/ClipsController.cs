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
    public class ClipsController : ControllerBase
    {
        private readonly HomeVideoDBContext _context;

        public ClipsController(HomeVideoDBContext context)
        {
            _context = context;
        }

        // GET: api/Clips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clip>>> GetClips()
        {
            return await _context.Clips.ToListAsync();
        }

        // GET: api/Clips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clip>> GetClip(int id)
        {
            var clip = await _context.Clips.FindAsync(id);

            if (clip == null)
            {
                return NotFound();
            }

            return clip;
        }

        // PUT: api/Clips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClip(int id, Clip clip)
        {
            if (id != clip.ClipId)
            {
                return BadRequest();
            }

            _context.Entry(clip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClipExists(id))
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

        // POST: api/Clips
        [HttpPost]
        public async Task<ActionResult<Clip>> PostClip(Clip clip)
        {
            _context.Clips.Add(clip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClip", new { id = clip.ClipId }, clip);
        }

        // DELETE: api/Clips/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clip>> DeleteClip(int id)
        {
            var clip = await _context.Clips.FindAsync(id);
            if (clip == null)
            {
                return NotFound();
            }

            _context.Clips.Remove(clip);
            await _context.SaveChangesAsync();

            return clip;
        }

        private bool ClipExists(int id)
        {
            return _context.Clips.Any(e => e.ClipId == id);
        }
    }
}
