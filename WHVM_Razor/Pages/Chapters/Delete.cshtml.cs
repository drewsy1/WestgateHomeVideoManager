using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Chapters
{
    public class DeleteModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public DeleteModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Chapter Chapter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chapter = await _context.Chapter
                .Include(c => c.Source).FirstOrDefaultAsync(m => m.ChapterId == id);

            if (Chapter == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chapter = await _context.Chapter.FindAsync(id);

            if (Chapter != null)
            {
                _context.Chapter.Remove(Chapter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
