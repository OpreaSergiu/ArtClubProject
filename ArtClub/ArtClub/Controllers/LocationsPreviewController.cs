using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtClub.Models;

namespace ArtClub.Controllers
{
    [Authorize]
    public class LocationsPreviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LocationsPreview
        public ActionResult Index()
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

        public ActionResult Details(int id)
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

            ViewBag.Name = locationsModels.Name;

            return View();
        }

        [HttpPost]
        public ActionResult Details(int id, string month)
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

            string fullScriptPath = Server.MapPath("~/CalendarRenderingScrips/") + "\\UsersCalendar.py";

            var textResult = run_cmd(fullScriptPath, id , month);

            ViewBag.Table = textResult;
            ViewBag.Name = locationsModels.Name;
            ViewBag.Month = month;

            return View();
        }
    }
}
