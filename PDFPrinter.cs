using Microsoft.Reporting.WebForms;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;

namespace RDLC.Extensions
{
    // download nugget package:
    // PdfiumViewer
    // PdfiumViewer.Native.x86_64.v8-xfa
    // rebuild project

    public class PDFPrinter
    {
        /// <summary>
        /// using RLDC;
        /// <code>
        /// LocalReport lr = new LocalReport();
        /// ..
        /// ...
        /// // print;
        /// var res = new PDFPrinter().Print(lr);
        /// 
        /// // return ok
        /// return Content("OK");
        /// </code>
        /// </summary>
        /// <param name="report"></param>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public bool Print(LocalReport report, string printerName = "Microsoft Print to PDF")
        {
            try
            {
                string mt, enc, f;
                string[] s;
                Warning[] w;

                byte[] b = report.Render("PDF", null, out mt, out enc, out f, out s, out w);

                return Print(b, printerName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }

        /// <summary>
        /// passing pdf file
        /// <code>
        /// var res = new PDFPrinter().Print("D:\\_WORK\\MEMBERSHIP CARD - DUPLEX.pdf");
        /// </code>
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public bool Print(string filename, string printerName = "Microsoft Print to PDF")
        {
            try
            {
                return Print(System.IO.File.ReadAllBytes(filename), printerName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Print(byte[] b, string printerName = "Microsoft Print to PDF")
        {
            try
            {
                // load pdf file
                using (var stream = new MemoryStream(b))
                {
                    using (var pdf = PdfDocument.Load(stream))
                    {
                        // create print doc
                        using (var printdoc = pdf.CreatePrintDocument())
                        {
                            // set printer
                            printdoc.PrinterSettings.PrinterName = printerName;

                            if (!printdoc.PrinterSettings.IsValid)
                            {
                                throw new Exception("Error: cannot find the default printer.");
                            }

                            // event handler
                            printdoc.PrintPage += new PrintPageEventHandler(PrintPageEvent);

                            // print
                            printdoc.Print();
                        }
                    }

                    return true;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrintPageEvent(object sender, PrintPageEventArgs ev)
        {

        }

    }
}