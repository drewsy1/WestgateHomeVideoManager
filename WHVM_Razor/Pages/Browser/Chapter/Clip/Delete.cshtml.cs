﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WHVM_Razor.Pages.Browser.Chapter.Clip
{
    public class DeleteModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public DeleteModel(WHVM_Razor.Models.HomeVideoDBContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clip = await _context.Clip.FindAsync(id);

            if (Clip != null)
            {
                _context.Clip.Remove(Clip);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}