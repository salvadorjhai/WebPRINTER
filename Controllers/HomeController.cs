using Microsoft.Reporting.WebForms;
using RDLC.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
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
            var selectedprinter = Request.QueryString["printer"];

            LocalReport lr = new LocalReport ();
            lr.ReportPath = Path.Combine(Server.MapPath("/REPORT/"), "Report1.rdlc");
            lr.DataSources.Clear();

            // lr.DataSources.Add(new ReportDataSource("DataSet1", models));
            
            // print by local report (rdlc)
            var res = new PDFPrinter().Print(lr, selectedprinter);

            // print by file
            // var res = new PDFPrinter().Print("D:\\_WORK\\MEMBERSHIP ID CARD - SINGLE.pdf", selectedprinter);

            return Content("OK");
        }
    }
}