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
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            return View(new ReportModels());
        }

        private string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\Sergiu\AppData\Local\Programs\Python\Python36-32\python.exe";
            start.CreateNoWindow = true;
            start.Arguments = string.Format("{0} {1}", cmd, args);
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

        [HttpPost]
        public FileResult GenerateIncomeRerport(string month)
        {
            var filePath = Server.MapPath("~/ReportingFolder/") + "\\Income_Rerpot.xlsx";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            string fullScriptPath = Server.MapPath("~/ReportingScripts/") + "\\incomereport.py";

            var textResult = run_cmd(fullScriptPath, month);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@filePath);
            string fileName = "Income_Rerpot.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public FileResult GenerateMembersPaymentsRerport(string month)
        {
            var filePath = Server.MapPath("~/ReportingFolder/") + "\\Members_Payments_Rerpot.xlsx";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            string fullScriptPath = Server.MapPath("~/ReportingScripts/") + "\\memberspayments.py";

            var textResult = run_cmd(fullScriptPath, month);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@filePath);
            string fileName = "Members_Payments_Rerpot.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}