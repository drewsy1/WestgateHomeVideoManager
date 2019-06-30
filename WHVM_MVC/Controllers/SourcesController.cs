using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WHVM_MVC.Models;

namespace WHVM_MVC.Controllers
{
    public class SourcesController : Controller
    {
        private readonly HomeVideoDBContext _context;

        public SourcesController(HomeVideoDBContext context)
        {
            _context = context;
            SourceFormat.AllFormats = _context.SourceFormat.ToList();
        }

        // GET: Sources
        public async Task<IActionResult> Index()
        {
            var homeVideoDbContext = _context.Source;
            return View(await homeVideoDbContext.ToListAsync());
        }

        // GET: Sources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await _context.Source
                .FirstOrDefaultAsync(m => m.SourceId == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        public IActionResult DetailsModalActionResult(int? id)
        {
            ViewBag.modelId = id;

            Source CurrentSource = _context.Source.FirstOrDefault(m => m.SourceId == id);

            ViewBag.modelSourceFormat = SourceFormat.AllFormats.FirstOrDefault(c => c.SourceFormatId == CurrentSource.SourceFormatId);

            string[] modalTitleParts =
            {
                "<div class=\"badge badge-secondary\">",
                (string) ViewBag.modelSourceFormat.SourceFormatText,
                "</div > ",
                CurrentSource.SourceLabel
            };

            ViewBag.modalTitle = new HtmlString(string.Join("", modalTitleParts));

            ViewBag.modelDataDictionary = new Dictionary<string, object>
            {
                {"ID", id },
                {"Label", CurrentSource.SourceLabel},
                {"Date Burned", CurrentSource.SourceDateBurned},
                {"Date Ripped", CurrentSource.SourceDateRipped},
                {"Format", ViewBag.modelSourceFormat.SourceFormatText}
            };
            return PartialView("_DetailsModal", _context.Source.FirstOrDefault(m => m.SourceId == id));
        }

        // GET: Sources/Create
        public IActionResult Create()
        {
            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormat, "SourceFormatId", "SourceFormatId");
            return View();
        }

        // POST: Sources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SourceId,SourceLabel,SourceDateBurned,SourceDateRipped,SourceFormatId,SourceFilePath")] Source source)
        {
            if (ModelState.IsValid)
            {
                _context.Add(source);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormat, "SourceFormatId", "SourceFormatId", source.SourceFormatId);
            return View(source);
        }

        // GET: Sources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await _context.Source.FindAsync(id);
            if (source == null)
            {
                return NotFound();
            }
            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormat, "SourceFormatId", "SourceFormatId", source.SourceFormatId);
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SourceId,SourceLabel,SourceDateBurned,SourceDateRipped,SourceFormatId,SourceFilePath")] Source source)
        {
            if (id != source.SourceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(source);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SourceExists(source.SourceId))
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
            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormat, "SourceFormatId", "SourceFormatId", source.SourceFormatId);
            return View(source);
        }

        // GET: Sources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await _context.Source
                .Include(s => s.SourceFormat)
                .FirstOrDefaultAsync(m => m.SourceId == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // POST: Sources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var source = await _context.Source.FindAsync(id);
            _context.Source.Remove(source);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SourceExists(int id)
        {
            return _context.Source.Any(e => e.SourceId == id);
        }
    }
}
