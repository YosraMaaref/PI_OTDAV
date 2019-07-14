using iTextSharp.text;
using iTextSharp.text.pdf;
using PI_OTDAV_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PI_OTDAV_Web.Controllers
{
    public class PerceptionController : Controller
    {
        // GET: Perception



        [HttpGet]
        public ActionResult DisplayPerception()
        {
            System.Net.Http.HttpClient Client = new System.Net.Http.HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpResponseMessage response = Client.GetAsync("/PI_OTDAV_4GL5B-web/api/CalculPerception/affichagePerceptions").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Models.PerceptionModel>>().Result;
            }
            else
            {
                ViewBag.result = "error";
            }
            return View();

        }


        public IEnumerable<PerceptionModel> Displaylistper()
        {
            System.Net.Http.HttpClient Client = new System.Net.Http.HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpResponseMessage response = Client.GetAsync("/PI_OTDAV_4GL5B-web/api/CalculPerception/affichagePerceptions").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<PerceptionModel>>().Result;
            }
            else
            {
                ViewBag.result = "error";
            }
            return null;

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

            List<PerceptionModel> perceptions = Displaylistper().ToList();


            tableLayout.AddCell(new PdfPCell(new Phrase("Liste des Perceptions ", new Font(Font.HELVETICA, 20, 1, Color.RED)))
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

            AddCellToHeader(tableLayout, "Id");
            AddCellToHeader(tableLayout, "Status");
            AddCellToHeader(tableLayout, "Montant Total");
            AddCellToHeader(tableLayout, "Oeuvre");
          

            ////Add body  

            foreach (var emp in perceptions)
            {

                AddCellToBody(tableLayout, emp.idPerception.ToString());
                AddCellToBody(tableLayout, emp.Statuts.ToString());
                AddCellToBody(tableLayout, emp.montantotale.ToString());
                AddCellToBody(tableLayout, emp.oeuvreD.titre);
               

            }

            return tableLayout;
        }


    }
}