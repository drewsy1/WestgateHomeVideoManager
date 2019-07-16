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
    public class ClipsController : Controller
    {
        private readonly HomeVideoDBContext _context;

        public ClipsController(HomeVideoDBContext context)
        {
            _context = context;
            TagsCollections.AllCollections = _context.TagsCollections.ToList();
            TagsPeople.AllPeople = _context.TagsPeople.ToList();
        }

        // GET: Clips
        public async Task<IActionResult> Index()
        {
            var homeVideoDbContext = _context.Clip;
            return View(await homeVideoDbContext.ToListAsync().ConfigureAwait(false));
        }

        // GET: Clips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clip
                .Include(c => c.Source)
                .FirstOrDefaultAsync(m => m.ClipId == id)
                .ConfigureAwait(false);

            Dictionary<int, string> ModelsList = new Dictionary<int, string>();
            await _context.Clip.ForEachAsync(model => ModelsList.Add(model.ClipId, model.ClipDescription)).ConfigureAwait(false);
            ViewData["ModelsList"] = ModelsList;
            ViewData["ModelID"] = clip.ClipId;

            if (clip == null)
            {
                return NotFound();
            }

            return View(clip);
        }

        public IActionResult ModalBtnActionResult(int? id, string modalName = "_DetailsModal")
        {
            ViewBag.modelId = id;

            Clip currentClip = _context.Clip.FirstOrDefault(m => m.ClipId == id);

            string[] modalTitleParts =
            {
                currentClip?.ClipDescription
            };

            ViewBag.modalTitle = new HtmlString(string.Join("", modalTitleParts));

            ViewBag.modelDataDictionary = new Dictionary<string, object>();
            ViewBag.modelDataDictionary.Add("ID", currentClip?.ClipId);
            ViewBag.modelDataDictionary.Add("Label", currentClip?.ClipDescription);
            ViewBag.modelDataDictionary.Add("Camera Operator", currentClip?.ClipCameraOperatorId);
            ViewBag.modelDataDictionary.Add("Clip Number", currentClip?.ClipNumber);
            ViewBag.modelDataDictionary.Add("Clip Reviewer", currentClip?.ClipReviewerId);
            ViewBag.modelDataDictionary.Add("Start (Timestamp)", currentClip?.ClipTimeStart);
            ViewBag.modelDataDictionary.Add("End (Timestamp)", currentClip?.ClipTimeEnd);
            ViewBag.modelDataDictionary.Add("Start (Clip Timestamp)", currentClip?.ClipVidTimeStart);
            ViewBag.modelDataDictionary.Add("End (Clip Timestamp)", currentClip?.ClipVidTimeEnd);
            ViewBag.modelDataDictionary.Add("Clip Length", currentClip?.ClipVidTimeLength);
            ViewBag.Controller = "Clips";
        return PartialView(modalName, currentClip);
        }

        // GET: Clips/Create
        public IActionResult Create()
        {
            ViewData["SourceId"] = new SelectList(_context.Source, "SourceId", "SourceId");
            return View();
        }

        // POST: Clips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClipId,SourceId,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewerID,ClipCameraOperatorID,ClipDescription,ClipFilePath")] Clip clip)
        {
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

            var clip = await _context.Clip.FindAsync(id).ConfigureAwait(false);
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
        public async Task<IActionResult> Edit(int id, [Bind("ClipId,SourceId,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewerID,ClipCameraOperatorID,ClipDescription,ClipFilePath")] Clip clip)
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

            var clip = await _context.Clip
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
            var clip = await _context.Clip.FindAsync(id).ConfigureAwait(false);
            _context.Clip.Remove(clip);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        private bool ClipExists(int id)
        {
            return _context.Clip.Any(e => e.ClipId == id);
        }
    }
}
