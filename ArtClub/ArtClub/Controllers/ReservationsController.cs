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
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            return View(db.ReservationsModels.Where(s => s.approved == false).ToList());
        }

        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationsModels reservationsModels = db.ReservationsModels.Find(id);

            if (reservationsModels.approved == false)
            {
                reservationsModels.approved = true;

                db.SaveChanges();

                var newApprovedReservation = new ApprovedReservationsModels();

                newApprovedReservation.User = reservationsModels.User;
                newApprovedReservation.Date = reservationsModels.Date;
                newApprovedReservation.Reason = reservationsModels.Reason;
                newApprovedReservation.Phone = reservationsModels.Phone;
                newApprovedReservation.LocationReserved = reservationsModels.LocationReserved;

                db.ApprovedReservationsModels.Add(newApprovedReservation);
                db.SaveChanges();

                var newCostsModels = new CostsModels();

                newCostsModels.UserName = reservationsModels.User;
                newCostsModels.Date = reservationsModels.Date;
                newCostsModels.Amount = 200;
                newCostsModels.Month = reservationsModels.Date;
                newCostsModels.EventId = 0;
                newCostsModels.EventName = reservationsModels.Reason;

                db.CostsModels.Add(newCostsModels);
                db.SaveChanges();

                var newPaymentsModelss = new PaymentsModels();

                newPaymentsModelss.UserName = reservationsModels.User;
                newPaymentsModelss.Date = DateTime.Now;
                newPaymentsModelss.Amount = 400;
                newPaymentsModelss.Month = DateTime.Now;
                newPaymentsModelss.Member = false;

                db.PaymentsModels.Add(newPaymentsModelss);
                db.SaveChanges();

                string redirectUrlInside = "/Reservations/Index/";
                return Redirect(redirectUrlInside);
            }

            string redirectUrl = "/Reservations/Index/";
            return Redirect(redirectUrl);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationsModels reservationsModels = db.ReservationsModels.Find(id);
            if (reservationsModels == null)
            {
                return HttpNotFound();
            }
            return View(reservationsModels);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservationsModels reservationsModels = db.ReservationsModels.Find(id);
            db.ReservationsModels.Remove(reservationsModels);
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
