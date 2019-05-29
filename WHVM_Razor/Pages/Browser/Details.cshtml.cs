using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Browser
{
    public class DetailsModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public DetailsModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

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
    }
}
