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
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index()
        {
            return View(db.PaymentsModels.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentsModels paymentsModels = db.PaymentsModels.Find(id);
            if (paymentsModels == null)
            {
                return HttpNotFound();
            }
            return View(paymentsModels);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Amount,Month,Member")] PaymentsModels paymentsModels)
        {
            if (ModelState.IsValid)
            {
                paymentsModels.Date = DateTime.Now;
                db.PaymentsModels.Add(paymentsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentsModels);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentsModels paymentsModels = db.PaymentsModels.Find(id);
            if (paymentsModels == null)
            {
                return HttpNotFound();
            }
            return View(paymentsModels);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Amount,Month,Member")] PaymentsModels paymentsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentsModels);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentsModels paymentsModels = db.PaymentsModels.Find(id);
            if (paymentsModels == null)
            {
                return HttpNotFound();
            }
            return View(paymentsModels);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentsModels paymentsModels = db.PaymentsModels.Find(id);
            db.PaymentsModels.Remove(paymentsModels);
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
