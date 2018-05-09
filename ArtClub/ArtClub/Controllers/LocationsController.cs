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
    [Authorize(Roles = "Admin")]
    public class LocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.LocationsModels.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationsModels locationsModels = db.LocationsModels.Find(id);
            if (locationsModels == null)
            {
                return HttpNotFound();
            }
            return View(locationsModels);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,City,State,Street,Number,AddressDetails,Description,Capacity")] LocationsModels locationsModels)
        {
            if (ModelState.IsValid)
            {
                db.LocationsModels.Add(locationsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locationsModels);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationsModels locationsModels = db.LocationsModels.Find(id);
            if (locationsModels == null)
            {
                return HttpNotFound();
            }
            return View(locationsModels);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,City,State,Street,Number,AddressDetails,Description,Capacity")] LocationsModels locationsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locationsModels);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationsModels locationsModels = db.LocationsModels.Find(id);
            if (locationsModels == null)
            {
                return HttpNotFound();
            }
            return View(locationsModels);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationsModels locationsModels = db.LocationsModels.Find(id);
            db.LocationsModels.Remove(locationsModels);
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
