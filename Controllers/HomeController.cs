using Microsoft.Reporting.WebForms;
using RDLC.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPRINTER.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.InstalledPrinter = PrinterSettings.InstalledPrinters;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Print()
        {
            var res = new PDFPrinter().Print("D:\\_WORK\\MEMBERSHIP ID CARD - SINGLE.pdf");
            return Content("OK");
        }
    }
}