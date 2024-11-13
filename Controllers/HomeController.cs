using Microsoft.Reporting.WebForms;
using RDLC.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<ActionResult> Print2()
        {
            var listener = Request.Form["listener"];
            var filename = Request.Files["filename"];
            
            if (filename == null || filename.ContentType.Contains("application/pdf")==false)
            {
                return Content("failed");
            }

            var tmp = Path.Combine(Path.GetTempPath(), "1.pdf");
            filename.SaveAs(tmp);

            string lokalprinter = $"{listener}?filename={tmp}";
            string msg = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = TimeSpan.FromSeconds(10);
                HttpResponseMessage getData = await client.GetAsync(lokalprinter);
                if (getData.IsSuccessStatusCode)
                {
                    msg = getData.Content.ReadAsStringAsync().Result;
                }
            }

            return Content("OK");
        }


    }
}