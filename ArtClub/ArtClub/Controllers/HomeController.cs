using ArtClub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult BookLocation()
        {
            return View(db.LocationsModels.ToList());
        }

        public ActionResult Reserve()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reserve(DateTime Date, string Reason, string User, string Phone, int id = 0)
        {
            var check = db.ReservationsModels.Where(s => s.Date == Date).Take(1).SingleOrDefault();

            if (check != null)
            {
                ViewBag.Message = "This Date is already booked";
                return View();
            }
            else
            {
                if ((id != 0) && (User.Length > 3) && (Phone.Length > 9) && (Date != null))
                {
                    var Reservation = new ReservationsModels();

                    Reservation.Date = Date;
                    Reservation.Reason = Reason;
                    Reservation.LocationReserved = id;
                    Reservation.User = User;
                    Reservation.Phone = Phone;

                    db.ReservationsModels.Add(Reservation);
                    db.SaveChanges();

                    string redirectUrl = "/Home/Index/";
                    return Redirect(redirectUrl);
                }

                ViewBag.Message = "Invalid Form";
                return View();
            }
        }
    }
}