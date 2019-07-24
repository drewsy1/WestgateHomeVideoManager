using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WHVM.Database.Models;

namespace WHVM_MVC.Controllers
{
    public class ClipsController : Controller
    {
        private readonly HomeVideoDBContext _context;
        private readonly List<Collection> allCollections;
        private readonly List<Person> allPersons;
        private readonly List<Source> allSources;

        public ClipsController(HomeVideoDBContext context)
        {
            _context = context;
            allCollections = _context.Collections.ToList();
            allPersons = _context.Persons.ToList();
            allSources = _context.Sources.ToList();
        }

        // GET: Clips
        public async Task<IActionResult> Index()
        {
            ViewData["AllCollections"] = allCollections;
            ViewData["AllPersons"] = allPersons;
            ViewData["AllSources"] = allSources;

            var homeVideoDbContext = _context.Clips;
            return View(await homeVideoDbContext.ToListAsync().ConfigureAwait(false));
        }

        // GET: Clips/Details/5
        public async Task<IActionResult> Details(int? id, int? sourceId)
        {
            if (!(id == null | sourceId == null))
            {
                return NotFound();
            }

            ViewData["AllCollections"] = allCollections;
            ViewData["AllPersons"] = allPersons;
            ViewData["AllSources"] = allSources;

            Clip currentClip = await _context.Clips
                .FirstOrDefaultAsync(m => m.ClipId == id)
                .ConfigureAwait(false);
            Source currentSource = await _context.Sources
                .FirstOrDefaultAsync(m => m.SourceId == sourceId)
                .ConfigureAwait(false);

            currentClip = currentClip ?? currentSource.Clips.First();
            currentSource = currentSource ?? currentClip.Source;

            if (!currentSource.Clips.Contains(currentClip))
            {
                throw new InvalidOperationException("Sources "+ currentSource.SourceId +" does not contain Clips "+ currentClip.ClipId);
                    ;
            }

            Dictionary<int, string> ModelsList = new Dictionary<int, string>();
            currentSource.Clips.ToList().ForEach(model => ModelsList.Add(model.ClipId, model.ClipName));

            ViewData["ModelsList"] = ModelsList;
            ViewData["ModelID"] = currentClip.ClipId;

            return View(currentClip);
        }

        public IActionResult ModalBtnActionResult(int? id, string modalName = "_DetailsModal")
        {
            ViewBag.modelId = id;

            Clip currentClip = _context.Clips.FirstOrDefault(m => m.ClipId == id);

            string[] modalTitleParts =
            {
                currentClip?.ClipName
            };

            ViewBag.modalTitle = new HtmlString(string.Join("", modalTitleParts));

            ViewBag.modelDataDictionary = new Dictionary<string, object>();
            ViewBag.modelDataDictionary.Add("ID", currentClip?.ClipId);
            ViewBag.modelDataDictionary.Add("Label", currentClip?.ClipName);
            ViewBag.modelDataDictionary.Add("Camera Operator", currentClip?.ClipCameraOperatorId);
            ViewBag.modelDataDictionary.Add("Clips Number", currentClip?.ClipNumber);
            ViewBag.modelDataDictionary.Add("Clips Reviewer", currentClip?.ClipReviewerId);
            ViewBag.modelDataDictionary.Add("Start (Timestamp)", currentClip?.ClipTimeStart);
            ViewBag.modelDataDictionary.Add("End (Timestamp)", currentClip?.ClipTimeEnd);
            ViewBag.modelDataDictionary.Add("Start (Clips Timestamp)", currentClip?.ClipVidTimeStart);
            ViewBag.modelDataDictionary.Add("End (Clips Timestamp)", currentClip?.ClipVidTimeEnd);
            ViewBag.modelDataDictionary.Add("Clips Length", currentClip?.ClipVidTimeLength);
            ViewBag.Controller = "Clips";
        return PartialView(modalName, currentClip);
        }

        // GET: Clips/Create
        public IActionResult Create()
        {
            ViewData["AllCollections"] = allCollections;
            ViewData["AllPersons"] = allPersons;
            ViewData["AllSources"] = allSources;

            ViewData["SourceId"] = new SelectList(_context.Sources, "SourceId", "SourceId");
            return View();
        }

        // POST: Clips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClipId,SourceId,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewerID,ClipCameraOperatorID,ClipName,ClipFilePath")] Clip clip)
        {
            ViewData["AllCollections"] = allCollections;
            ViewData["AllPersons"] = allPersons;
            ViewData["AllSources"] = allSources;

            if (ModelState.IsValid)
            {
                _context.Add(clip);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(clip);
        }

        // GET: Clips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["AllCollections"] = allCollections;
            ViewData["AllPersons"] = allPersons;
            ViewData["AllSources"] = allSources;

            var clip = await _context.Clips.FindAsync(id).ConfigureAwait(false);
            if (clip == null)
            {
                return NotFound();
            }
            return View(clip);
        }

        // POST: Clips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClipId,SourceId,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewerID,ClipCameraOperatorID,ClipName,ClipFilePath")] Clip clip)
        {
            if (id != clip.ClipId)
            {
                return NotFound();
            }

            ViewData["AllCollections"] = allCollections;
            ViewData["AllPersons"] = allPersons;
            ViewData["AllSources"] = allSources;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clip);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
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
            return View(clip);
        }

        // GET: Clips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clips
                .Include(c => c.Source)
                .FirstOrDefaultAsync(m => m.ClipId == id)
                .ConfigureAwait(false);
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
            var clip = await _context.Clips.FindAsync(id).ConfigureAwait(false);
            _context.Clips.Remove(clip);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        private bool ClipExists(int id)
        {
            return _context.Clips.Any(e => e.ClipId == id);
        }
    }
}
