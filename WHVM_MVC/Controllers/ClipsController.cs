using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WHVM_MVC.Models;

namespace WHVM_MVC.Controllers
{
    public class ClipsController : Controller
    {
        private readonly HomeVideoDBContext _context;

        public ClipsController(HomeVideoDBContext context)
        {
            _context = context;
        }

        // GET: Clips
        public async Task<IActionResult> Index()
        {
            var homeVideoDBContext = _context.Clip.Include(c => c.Chapter);
            return View(await homeVideoDBContext.ToListAsync());
        }

        // GET: Clips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clip
                .Include(c => c.Chapter)
                .FirstOrDefaultAsync(m => m.ClipId == id);
            if (clip == null)
            {
                return NotFound();
            }

            return View(clip);
        }

        // GET: Clips/Create
        public IActionResult Create()
        {
            ViewData["ChapterId"] = new SelectList(_context.Chapter, "ChapterId", "ChapterId");
            return View();
        }

        // POST: Clips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClipId,ChapterId,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewer,CameraOperator,Description")] Clip clip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapter, "ChapterId", "ChapterId", clip.ChapterId);
            return View(clip);
        }

        // GET: Clips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clip.FindAsync(id);
            if (clip == null)
            {
                return NotFound();
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapter, "ChapterId", "ChapterId", clip.ChapterId);
            return View(clip);
        }

        // POST: Clips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClipId,ChapterId,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewer,CameraOperator,Description")] Clip clip)
        {
            if (id != clip.ClipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClipExists(clip.ClipId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapter, "ChapterId", "ChapterId", clip.ChapterId);
            return View(clip);
        }

        // GET: Clips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clip
                .Include(c => c.Chapter)
                .FirstOrDefaultAsync(m => m.ClipId == id);
            if (clip == null)
            {
                return NotFound();
            }

            return View(clip);
        }

        // POST: Clips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clip = await _context.Clip.FindAsync(id);
            _context.Clip.Remove(clip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClipExists(int id)
        {
            return _context.Clip.Any(e => e.ClipId == id);
        }
    }
}
