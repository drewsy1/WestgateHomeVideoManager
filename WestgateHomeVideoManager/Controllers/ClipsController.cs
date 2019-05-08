using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WestgateHomeVideoManager.Models;

namespace WestgateHomeVideoManager.Controllers
{
    public class ClipsController : Controller
    {
        private HomeVideoDBEntities db = new HomeVideoDBEntities();

        // GET: Clips
        public ActionResult Index()
        {
            var clips = db.Clips.Include(c => c.Chapter);
            return View(clips.ToList());
        }

        // GET: Clips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clip clip = db.Clips.Find(id);
            if (clip == null)
            {
                return HttpNotFound();
            }
            return View(clip);
        }

        // GET: Clips/Create
        public ActionResult Create()
        {
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "ChapterLength");
            return View();
        }

        // POST: Clips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClipID,ChapterID,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewer,CameraOperator,Description")] Clip clip)
        {
            if (ModelState.IsValid)
            {
                db.Clips.Add(clip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "ChapterLength", clip.ChapterID);
            return View(clip);
        }

        // GET: Clips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clip clip = db.Clips.Find(id);
            if (clip == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "ChapterLength", clip.ChapterID);
            return View(clip);
        }

        // POST: Clips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClipID,ChapterID,ClipNumber,ClipTimeStart,ClipTimeEnd,ClipVidTimeStart,ClipVidTimeEnd,ClipVidTimeLength,ClipReviewer,CameraOperator,Description")] Clip clip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "ChapterLength", clip.ChapterID);
            return View(clip);
        }

        // GET: Clips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clip clip = db.Clips.Find(id);
            if (clip == null)
            {
                return HttpNotFound();
            }
            return View(clip);
        }

        // POST: Clips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clip clip = db.Clips.Find(id);
            db.Clips.Remove(clip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
