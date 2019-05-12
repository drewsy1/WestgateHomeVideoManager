using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Sources
{
    public class EditModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public EditModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Source Source { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Source = await _context.Source.FirstOrDefaultAsync(m => m.SourceId == id);

            if (Source == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Source).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceExists(Source.SourceId))
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

        private bool SourceExists(int id)
        {
            return _context.Source.Any(e => e.SourceId == id);
        }
    }
}
