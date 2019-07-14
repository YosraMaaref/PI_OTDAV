using iTextSharp.text;
using iTextSharp.text.pdf;
using PI_OTDAV_Domain.Entities;
using PI_OTDAV_Services;
using PI_OTDAV_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PI_OTDAV_Web.Controllers
{
    public class RepartitionController : Controller
    {
        // GET: Repartition
        public ActionResult Index(int idPerception)
        {
            RepartitionService rp = new RepartitionService();
            rp.CalculateRepartitionOtdav(idPerception);
            return View();
        }


        public ActionResult DisplayRepartition()
        {
            RepartitionService rp = new RepartitionService();
            var meme=  rp.getAlll();

            int x = rp.countRepartitions();

            ViewBag.count1 = x;
           

            return View(meme);
        }

        public ActionResult DisplayRepartitionbyArtwork(int idoeuv)
        {
            RepartitionService rp = new RepartitionService();
            ViewBag.result = rp.getRepartitionByArtWork(idoeuv);

            return View();
        }

        public FileResult CreatePdf()
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("SamplePdf" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();

            //Create PDF Table with 5 columns  
            PdfPTable tableLayout = new PdfPTable(4);

            //Create PDF Table  

            //file will created in this path  
            string strAttachment = Server.MapPath("~/Downloadss/" + strPDFFileName);


            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(Add_Content_To_PDF(tableLayout));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);

        }
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.HELVETICA, 8, 1)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5
            });
        }

        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.HELVETICA, 8, 1)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5
            });
        }
        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout)
        {

            float[] headers = { 50, 24, 45, 35 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  
            RepartitionService rs = new RepartitionService();
            List<repartition> repartitions = rs.GetAll().ToList();
            //KAmalha mba3d 

            tableLayout.AddCell(new PdfPCell(new Phrase("Bordureau de  Repartition ", new Font(Font.HELVETICA, 20, 1, Color.RED)))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.HELVETICA, 10, 1, Color.RED)))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            ////Add header  

           
            AddCellToHeader(tableLayout, "Benifice OTDAV");
            AddCellToHeader(tableLayout, "Benifice User");
            AddCellToHeader(tableLayout, "net Percue");
            AddCellToHeader(tableLayout, "Impot");
         


            ////Add body  

            foreach (var emp in repartitions)
            {

        
                AddCellToBody(tableLayout, emp.benificeOTDAV.ToString());
                AddCellToBody(tableLayout, emp.benificeUser.ToString());
                AddCellToBody(tableLayout, emp.netPercue.ToString());
                AddCellToBody(tableLayout, emp.impot.ToString());
               


            }

            return tableLayout;
        }






        public ActionResult ExportToExcel()
        {
            RepartitionService rp = new RepartitionService();

            var gv = new GridView();
            var liste = rp.GetMany();
            gv.DataSource = liste.ToList();
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");

        }

    }
}