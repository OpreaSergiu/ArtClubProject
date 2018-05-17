using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtClub.Models;
using Microsoft.AspNet.Identity;

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

        public ActionResult ChoseLocation(int? id)
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

            string SQL_querry = "SELECT * FROM LocationsModels EXCEPT (SELECT LocationsModels.Id, LocationsModels.Name, LocationsModels.City, LocationsModels.State, LocationsModels.Street, LocationsModels.Number, LocationsModels.AddressDetails, LocationsModels.Description, LocationsModels.Capacity FROM LocationsModels INNER JOIN ApprovedReservationsModels ON ApprovedReservationsModels.LocationReserved = LocationsModels.Id AND ApprovedReservationsModels.Date = @p0)";


            var model = new EventLocationsViewModels()
            {
                Event = eventsModels,

                Locations = (db.Database.SqlQuery<LocationsModels>(SQL_querry, eventsModels.EventDate))
            };

            return View(model);
        }

        public ActionResult Invite(int? id)
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

        [HttpPost]
        public ActionResult Invite(int id, string email)
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

            var newInvite = new EventGuestsModels();

            newInvite.EventId = id;
            newInvite.GuestEmail = email;
            newInvite.EventName = eventsModels.Name;
            newInvite.EventDate = eventsModels.EventDate;
            newInvite.EventLocation = db.LocationsModels.Find(eventsModels.LocationId).Name;

            db.EventGuestsModels.Add(newInvite);
            db.SaveChanges();

            return View(eventsModels);
        }

        public ActionResult BookLocationForEvent(int id, int locationId)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (locationId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventsModels eventsModels = db.EventsModels.Find(id);
            if (eventsModels == null)
            {
                return HttpNotFound();
            }

            eventsModels.LocationId = locationId;
            eventsModels.LocationName = db.LocationsModels.Find(locationId).Name;

            db.SaveChanges();

            var newApprovedReservation = new ApprovedReservationsModels();

            newApprovedReservation.User = eventsModels.CreationUser;
            newApprovedReservation.Date = eventsModels.EventDate;
            newApprovedReservation.Reason = eventsModels.Name;
            newApprovedReservation.Phone = " ";
            newApprovedReservation.LocationReserved = eventsModels.LocationId;

            db.ApprovedReservationsModels.Add(newApprovedReservation);
            db.SaveChanges();

            var newCostsModels = new CostsModels();

            newCostsModels.UserName = eventsModels.CreationUser;
            newCostsModels.Date = DateTime.Now;
            newCostsModels.Amount = 200;
            newCostsModels.Month = eventsModels.EventDate;
            newCostsModels.EventId = eventsModels.Id;
            newCostsModels.EventName = eventsModels.Name;

            db.CostsModels.Add(newCostsModels);
            db.SaveChanges();

            string redirectUrl = "/Events";

            return Redirect(redirectUrl);
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
        public ActionResult Create([Bind(Include = "Id,Name,Description,EventDate")] EventsModels eventsModels)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ArtClub-20180506023021;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            conn.Open();

            SqlCommand com1 = new SqlCommand("SELECT SUM(Amount) FROM PaymentsModels WHERE FORMAT(Month,'yyyy-MM') = @p0", conn);
            SqlCommand com2 = new SqlCommand("SELECT SUM(Amount) FROM CostsModels WHERE FORMAT(Month,'yyyy-MM') = @p0", conn);

            com1.Parameters.AddWithValue("@p0", eventsModels.EventDate.ToString("yyyy-MM"));

            string totalPayments = com1.ExecuteScalar().ToString();

            com2.Parameters.AddWithValue("@p0", eventsModels.EventDate.ToString("yyyy-MM"));

            string totalCosts = com2.ExecuteScalar().ToString();

            float totalPaymentsForMonth = float.Parse(totalPayments);
            float totalCostsForMonth = float.Parse(totalCosts);

            if (totalPaymentsForMonth < totalCostsForMonth)
            {
                ViewBag.message = "You cannot reserve anymore. The club budget it's over for this month.";
                return View(eventsModels);
            }

            if (ModelState.IsValid)
            {
                eventsModels.CreationUser = User.Identity.GetUserName();
                eventsModels.CreationDate = DateTime.Now;
                db.EventsModels.Add(eventsModels);
                db.SaveChanges();

                string redirectUrl = "/Events/ChoseLocation/" + eventsModels.Id;

                return Redirect(redirectUrl);
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

            string user_name = User.Identity.GetUserName();

            if (eventsModels == null)
            {
                return HttpNotFound();
            }
            if (eventsModels.CreationUser == user_name)
            {
                return View(eventsModels);
            }
            return HttpNotFound();
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
