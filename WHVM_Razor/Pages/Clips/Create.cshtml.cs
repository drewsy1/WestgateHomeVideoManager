﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WHVM_Razor.Models;

namespace WHVM_Razor.Pages.Clips
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
        ViewData["ChapterId"] = new SelectList(_context.Chapter, "ChapterId", "ChapterId");
            return Page();
        }

        [BindProperty]
        public Clip Clip { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Clip.Add(Clip);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}