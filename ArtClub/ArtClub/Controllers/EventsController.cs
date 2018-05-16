using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
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

        private string run_cmd(string cmd, int args1, string args2)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\Sergiu\AppData\Local\Programs\Python\Python36-32\python.exe";
            start.CreateNoWindow = true;
            start.Arguments = string.Format("{0} {1} {2}", cmd, args1, args2);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    //Console.Write(result);
                    process.WaitForExit();
                    return result;
                }
            }
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

            var LocationsToChose = (db.Database.SqlQuery<LocationsModels>(SQL_querry, eventsModels.EventDate));

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
        public ActionResult Create([Bind(Include = "Id,Name,Description,EventDate")] EventsModels eventsModels)
        {
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

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string user_name = User.Identity.GetUserName();

            EventsModels eventsModels = db.EventsModels.Find(id);
            if (eventsModels == null)
            {
                return HttpNotFound();
            }
            if(eventsModels.CreationUser == user_name)
            {
                return View(eventsModels);
            }
            return HttpNotFound();
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
