using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WHVM_Razor.Pages.Browser.Chapter.Clip
{
    public class EditModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public EditModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Clip Clip { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clip = await _context.Clip
                .Include(c => c.Chapter).FirstOrDefaultAsync(m => m.ClipId == id);

            if (Clip == null)
            {
                return NotFound();
            }
           ViewData["ChapterId"] = new SelectList(_context.Chapter, "ChapterId", "ChapterId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Clip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClipExists(Clip.ClipId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClipExists(int id)
        {
            return _context.Clip.Any(e => e.ClipId == id);
        }
    }
}
