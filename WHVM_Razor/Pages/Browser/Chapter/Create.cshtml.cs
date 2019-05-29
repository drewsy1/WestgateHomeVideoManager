using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Browser.Chapter
{
    public class CreateModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public CreateModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SourceId"] = new SelectList(_context.Source, "SourceId", "SourceId");
            return Page();
        }

        [BindProperty]
        public Models.Chapter Chapter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Chapter.Add(Chapter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}