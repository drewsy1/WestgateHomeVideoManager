using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Clips
{
    public class DetailsModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public DetailsModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        public Clip Clip { get; set; }

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
    }
}
