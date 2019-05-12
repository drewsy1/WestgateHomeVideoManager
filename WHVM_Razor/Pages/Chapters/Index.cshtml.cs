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
    public class IndexModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public IndexModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        public IList<Chapter> Chapter { get;set; }

        public async Task OnGetAsync()
        {
            Chapter = await _context.Chapter
                .Include(c => c.Source).ToListAsync();
        }
    }
}
