using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WHVM.Database.Models;

namespace WHVM_MVC.Controllers
{
    public class SourcesController : Controller
    {
        private readonly HomeVideoDBContext _context;
        private readonly List<SourceFormat> allSourceFormats;

        public SourcesController(HomeVideoDBContext context)
        {
            _context = context;
            allSourceFormats = _context.SourceFormats.ToList();
        }

        // GET: Sources
        public async Task<IActionResult> Index()
        {
            ViewData["AllSourceFormats"] = allSourceFormats;
            var homeVideoDbContext = _context.Sources;
            return View(await homeVideoDbContext.ToListAsync().ConfigureAwait(false));
        }

        // GET: Sources/Details/5
        public async Task<IActionResult> Details(int? id, List<Source> sourceList)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["AllSourceFormats"] = allSourceFormats;

            var source = await _context.Sources
                .FirstOrDefaultAsync(m => m.SourceId == id)
                .ConfigureAwait(false);

            Dictionary<int,string> ModelsList = new Dictionary<int, string>();

            if (sourceList.Count > 0)
            {
                sourceList.ForEach(model => ModelsList.Add(model.SourceId, model.SourceName));
            }
            else
            {
                await _context.Sources.ForEachAsync(model => ModelsList.Add(model.SourceId, model.SourceName)).ConfigureAwait(false);
            }

            ViewData["ModelsList"] = ModelsList;
            ViewData["ModelID"] = source.SourceId;

            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        public IActionResult ModalBtnActionResult(int? id, string modalName = "_DetailsModal")
        {
            ViewBag.modelId = id;

            var currentSource = _context.Sources.FirstOrDefault(m => m.SourceId == id);
            _context.Entry(currentSource ?? throw new InvalidOperationException()).Reference(s => s.SourceFormat).Load();

            ViewBag.modelSourceFormat =
                ((List<SourceFormat>)ViewData["AllSourceFormats"]).FirstOrDefault(c => c.SourceFormatId == currentSource.SourceFormatId);

            ViewBag.modelID = id;
            ViewBag.modelLabel = currentSource.SourceName;
            ViewBag.modelDateCreated = currentSource.SourceDateCreated;
            ViewBag.modelDateImported = currentSource.SourceDateImported;
            ViewBag.modelFormat = currentSource.SourceFormat.SourceFormatName;
            ViewBag.modelController = "Sources";

            ViewBag.modelDataDictionary = new Dictionary<string, object>();
            ViewBag.modelDataDictionary.Add("ID", id);
            ViewBag.modelDataDictionary.Add("Label", currentSource.SourceName);
            ViewBag.modelDataDictionary.Add("Date Created", currentSource.SourceDateCreated);
            ViewBag.modelDataDictionary.Add("Date Imported", currentSource.SourceDateImported);
            ViewBag.modelDataDictionary.Add("Format", currentSource.SourceFormat.SourceFormatName);
            ViewBag.Controller = "Sources";
            return PartialView(modalName, currentSource);
        }

        // GET: Sources/Create
        public IActionResult Create()
        {
            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormats, "SourceFormatId", "SourceFormatId");
            return View();
        }

        // POST: Sources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("SourceId,SourceName,SourceDateCreated,SourceDateImported,SourceFormatId,SourceFilePath")]
            Source source)
        {
            ViewData["AllSourceFormats"] = allSourceFormats;

            if (ModelState.IsValid)
            {
                _context.Add(source);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }

            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormats, "SourceFormatId", "SourceFormatId",
                source.SourceFormatId);
            return View(source);
        }

        // GET: Sources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["AllSourceFormats"] = allSourceFormats;

            var source = await _context.Sources.FindAsync(id).ConfigureAwait(false);
            if (source == null)
            {
                return NotFound();
            }

            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormats, "SourceFormatId", "SourceFormatId",
                source.SourceFormatId);
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("SourceId,SourceName,SourceDateCreated,SourceDateImported,SourceFormatId,SourceFilePath")]
            Source source)
        {
            if (id != source.SourceId)
            {
                return NotFound();
            }

            ViewData["AllSourceFormats"] = allSourceFormats;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(source);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
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

            ViewData["SourceFormatId"] = new SelectList(_context.SourceFormats, "SourceFormatId", "SourceFormatId",
                source.SourceFormatId);
            return View(source);
        }

        // GET: Sources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["AllSourceFormats"] = allSourceFormats;

            var source = await _context.Sources
                .Include(s => s.SourceFormat)
                .FirstOrDefaultAsync(m => m.SourceId == id)
                .ConfigureAwait(false);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // POST: Sources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, bool returnJson = false)
        {
            var source = await _context.Sources.FindAsync(id).ConfigureAwait(false);
            _context.Sources.Remove(source);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            if(returnJson) return Json(Url.Action("Index", "Sources"));
            return RedirectToAction(nameof(Index));
        }

        private bool SourceExists(int id)
        {
            return _context.Sources.Any(e => e.SourceId == id);
        }
    }
}