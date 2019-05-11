using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WestgateHomeVideoManager.Models;
using EntityState = System.Data.Entity.EntityState;

namespace WestgateHomeVideoManager.Views
{
    public class SourcesController : Controller
    {
        private HomeVideoDBEntities db = new HomeVideoDBEntities();

        // GET: Sources
        public ActionResult Index()
        {
            return View(db.Sources.ToList());
        }

        // GET: Sources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // GET: Sources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SourceID,SourceLabel,SourceDateBurned,SourceDateRipped")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Sources.Add(source);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(source);
        }

        // GET: Sources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SourceID,SourceLabel,SourceDateBurned,SourceDateRipped")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Entry(source).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(source);
        }

        // GET: Sources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Source source = db.Sources.Find(id);
            db.Sources.Remove(source);
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
