using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Browser.Chapter
{
    public class DetailsModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public DetailsModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        public Models.Chapter Chapter { get; set; }

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
    }
}
