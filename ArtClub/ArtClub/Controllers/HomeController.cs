using ArtClub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ArtClub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string intrest, string email)
        {
            if ((intrest != null) && (email != null) && (email.Length > 6))
            {
                var newUser = new UserRequestsModels();
                newUser.Email = email;
                newUser.Intrest = intrest;
                newUser.Date = DateTime.Now;

                db.UserRequestsModels.Add(newUser);
                db.SaveChanges();

                return View("Index");
            }
            ViewBag.Message = "Invalid Form";

            return View();
        }

        public ActionResult BookLocation()
        {
            return View(db.LocationsModels.ToList());
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

        public ActionResult Reserve(int id = 0)
        {
            LocationsModels locationsModels = db.LocationsModels.Find(id);
            if (locationsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.Name = locationsModels.Name;
            return View();
        }

        [HttpPost]
        public ActionResult Reserve(int id, string month)
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

            string fullScriptPath = Server.MapPath("~/CalendarRenderingScrips/") + "\\NonUsersCalendar.py";

            var textResult = run_cmd(fullScriptPath, id, month);

            ViewBag.Table = textResult;
            ViewBag.Name = locationsModels.Name;
            ViewBag.Month = month;

            return View();
        }

        [HttpPost]
        public ActionResult ReserveAction(DateTime Date, string Reason, string User, string Phone, int LocationReserved)
        {
            var check = db.ReservationsModels.Where(s => s.Date == Date).Take(1).SingleOrDefault();
            LocationsModels locationsModels = db.LocationsModels.Find(LocationReserved);

            if (check != null)
            {
                ViewBag.Message = "This Date is already booked";
                return View();
            }
            else
            {
                if ((LocationReserved != null) && (User.Length > 3) && (Phone.Length > 9) && (Date != null))
                {
                    var Reservation = new ReservationsModels();

                    Reservation.Date = Date;
                    Reservation.Reason = Reason;
                    Reservation.LocationReserved = LocationReserved;
                    Reservation.User = User;
                    Reservation.Phone = Phone;

                    db.ReservationsModels.Add(Reservation);
                    db.SaveChanges();

                    string redirectUrl = "/Home/Index/";
                    return Redirect(redirectUrl);
                }

                ViewBag.Name = locationsModels.Name;
                ViewBag.Message = "Invalid Form";
                return View();
            }
        }
    }
}