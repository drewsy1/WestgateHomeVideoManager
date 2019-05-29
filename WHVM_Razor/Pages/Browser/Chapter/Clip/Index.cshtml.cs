using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WHVM_Razor.Pages.Browser.Chapter.Clip
{
    public class IndexModel : PageModel
    {
        private readonly WHVM_Razor.Models.HomeVideoDBContext _context;

        public IndexModel(WHVM_Razor.Models.HomeVideoDBContext context)
        {
            _context = context;
        }

        public IList<Models.Clip> Clip { get;set; }

        public async Task OnGetAsync()
        {
            Clip = await _context.Clip
                .Include(c => c.Chapter).ToListAsync();
        }
    }
}
