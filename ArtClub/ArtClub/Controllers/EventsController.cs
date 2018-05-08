using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtClub.Models;

namespace ArtClub.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.EventsModels.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventsModels eventsModels = db.EventsModels.Find(id);
            if (eventsModels == null)
            {
                return HttpNotFound();
            }
            return View(eventsModels);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,EventDate,LocationId")] EventsModels eventsModels)
        {
            if (ModelState.IsValid)
            {
                db.EventsModels.Add(eventsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventsModels);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventsModels eventsModels = db.EventsModels.Find(id);
            if (eventsModels == null)
            {
                return HttpNotFound();
            }
            return View(eventsModels);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,EventDate,LocationId")] EventsModels eventsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventsModels);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventsModels eventsModels = db.EventsModels.Find(id);
            if (eventsModels == null)
            {
                return HttpNotFound();
            }
            return View(eventsModels);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventsModels eventsModels = db.EventsModels.Find(id);
            db.EventsModels.Remove(eventsModels);
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
