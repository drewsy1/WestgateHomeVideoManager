using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Chapters
{
    public class EditModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public EditModel(WHVM_Razor.Models.HomeVideoDBContext context)
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
           ViewData["SourceId"] = new SelectList(_context.Source, "SourceId", "SourceId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Chapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(Chapter.ChapterId))
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

        private bool ChapterExists(int id)
        {
            return _context.Chapter.Any(e => e.ChapterId == id);
        }
    }
}
