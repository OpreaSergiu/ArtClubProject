using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtClub.Models;
using Microsoft.AspNet.Identity;

namespace ArtClub.Controllers
{
    [Authorize]
    public class EventGuestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EventGuests
        public ActionResult Index()
        {
            var useremail = User.Identity.GetUserName();
            return View(db.EventGuestsModels.Where(s => s.GuestEmail == useremail).ToList());
        }
    }
}
