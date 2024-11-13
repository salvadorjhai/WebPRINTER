**Required nugget package;**
- PdfiumViewer
- PdfiumViewer.Native.x86_64.v8-xfa
    
**Sample Usage;**

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
