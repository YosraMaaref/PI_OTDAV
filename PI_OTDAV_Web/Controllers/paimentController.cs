using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PI_OTDAV_Web.Models;
using RestSharp;
using PI_OTDAV_Services.Services;
using System.Collections;
using System.Diagnostics;
using PI_OTDAV_Domain.Entities;
using System.Net.Mail;

namespace PI_OTDAV_Web.Controllers
{
    public class paimentController : Controller
    {
        private PI_OTDAV_WebContext db = new PI_OTDAV_WebContext();
        private RestClient client = new RestClient("http://localhost:18080/PI_OTDAV_4GL5B-web/api/");
        List<paimentModel> listPaimentCotisation = new List<paimentModel>();
        List<paimentModel> listPaimentDeposit = new List<paimentModel>();
        List<paimentModel> listPaimentDeclaration = new List<paimentModel>();
        long nbOk;
        long nbNo;



        public paimentController()
        {
            RestRequest request = new RestRequest("paiment/cotisation", Method.GET);
            IRestResponse<List<paimentModel>> response = client.Execute<List<paimentModel>>(request);
            //list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocumentModels>>(response.Content);
            if (!response.Content.Equals(""))
                listPaimentCotisation.AddRange(response.Data);

            request = new RestRequest("paiment/declaration", Method.GET);
            response = client.Execute<List<paimentModel>>(request);
            if (!response.Content.Equals(""))
                listPaimentDeclaration.AddRange(response.Data);


            request = new RestRequest("paiment/depot", Method.GET);
            response = client.Execute<List<paimentModel>>(request);
            //listPaimentDeposit = Newtonsoft.Json.JsonConvert.DeserializeObject<List<paimentModel>>(response.Content);
            if (!response.Content.Equals(""))
                listPaimentDeposit.AddRange(response.Data);

            request = new RestRequest("paiment/ok", Method.GET);
            IRestResponse<long> responseStat = client.Execute<long>(request);
            responseStat = client.Execute<long>(request);
            if (!responseStat.Content.Equals(""))
                nbOk = responseStat.Data;
            request = new RestRequest("paiment/no", Method.GET);
            responseStat = client.Execute<long>(request);
            if (!responseStat.Content.Equals(""))
                nbNo = responseStat.Data;



        }


        // GET: paiment
        public ActionResult Index()
        {
            listPaimentCotisation.OrderByDescending(e => e.Date);
            //listPaimentCotisation.OrderBy(e => e.Date);
            listPaimentDeclaration.OrderBy(e => e.Date);
            listPaimentDeposit.OrderBy(e => e.Date);
            ViewBag.listPaimentDeposit = listPaimentDeposit;
            ViewBag.listPaimentDeclaration = listPaimentDeclaration;

            ViewBag.nbOk = nbOk;
            ViewBag.nbNo = nbNo;
            IPaimentService service = new PaimentService();
            foreach (var i in listPaimentCotisation)
            {
                if ((DateTime.Now - i.Date.Value).TotalDays > 365)
                {
                    paiment p = service.getPaiment(i.ID);
                    service.deletePaiment(p);
                }
            }
            foreach (var i in listPaimentDeclaration)
            {
                if ((i.Date.Value - DateTime.Now).TotalDays > 365)
                {
                    paiment p = new paiment()
                    {
                        ID = i.ID
                    };
                    service.deletePaiment(p);
                }
            }
            foreach (var i in listPaimentDeposit)
            {
                if ((i.Date.Value - DateTime.Now).TotalDays > 365)
                {
                    paiment p = new paiment()
                    {
                        ID = i.ID
                    };
                    service.deletePaiment(p);
                }
            }
            return View(listPaimentCotisation);
        }
        public JsonResult getPaimentById(int idPaiment)
        {
            paimentModel p;
            p = listPaimentCotisation.Find(e => e.ID == idPaiment);
            if (p == null)
                p = listPaimentDeclaration.Find(e => e.ID == idPaiment);
            if (p == null)
                p = listPaimentDeposit.Find(e => e.ID == idPaiment);
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        // GET: paiment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paimentModel paimentModel = db.paimentModels.Find(id);
            if (paimentModel == null)
            {
                return HttpNotFound();
            }
            return View(paimentModel);
        }

        // GET: paiment/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Users, "id", "accountStatuts");
            return View();
        }

        // POST: paiment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Price,Status,Title,bankcard_IdBank,cheque_IdCheque,oeuvreDec_id,userId,virement_IdVirement")] paimentModel paimentModel)
        {
            if (ModelState.IsValid)
            {
                db.paimentModels.Add(paimentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Users, "id", "accountStatuts", paimentModel.userId);
            return View(paimentModel);
        }

        // GET: paiment/Edit/5
        public ActionResult Valider(int? id)
        {
            var request = new RestRequest("paiment/valider/" + id, Method.PUT);
            client.Execute(request);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("ahmed.boukhtioua@esprit.tn");
            mail.To.Add("ahmedboukhtioua@gmail.com");
            mail.Subject = "Etat de votre paiment";
            mail.IsBodyHtml = true;
            mail.Body = "<html>\n"
                    + "<head>\n"
                    + "<style>\n"
                    + ".div_principale a:hover {\n"
                    + "	border-bottom: 1px solid #253c93; }\n"
                    + "</style>\n"
                    + "</head>\n"
                    + "<body>\n"
                    + "<div style=\"width:80%;margin-left:10%;border: 1.5px solid #1b1a19;/* outer shadows  (note the rgba is red, green, blue, alpha) */-webkit-box-shadow: 0px 0px 12px rgba(0, 0, 0, 0.4); -moz-box-shadow: 0px 1px 6px rgba(23, 69, 88, .5);\n"
                    + "\n"
                    + "-webkit-border-radius: 12px;\n"
                    + "-moz-border-radius: 7px; \n"
                    + "border-radius: 7px;\n"
                    + "\n"
                    + "background: -webkit-gradient(linear, left top, left bottom, \n"
                    + "color-stop(0%, white), color-stop(15%, white), color-stop(100%, #D7E9F5)); \n"
                    + "background: -moz-linear-gradient(top, white 0%, white 55%, #D5E4F3 130%); \">\n"
                    + "<img src=\"http://www.tunisietravail.net/uploads/2017/01/otdav.png\" style=\"width: 90%;height: 250;margin-left: 5%;\">\n"
                    + "<div style=\"margin-left:7%;\">\n"
                    + "<p class=\"p\"><h3>Bonjour " + "Bouktioua" + " " + "Ahmed" + ",</h3>\n"
                    + "Bienvenue chez <b>OTDAV</b>. Nous vous informons que votre paiment a été " + "accpeter" + "!<br><br>\n"
                    + "<p>\n"
                    + "Vous pouvez accéder au site : <a href=\"\" style=\"color: #253c93;text-decoration: none;\">www.OTDAV.tn</a>\n"
                    + "</p>\n"
                    + "<div>\n"
                    + "</div>\n"
                    + "</body>\n"
                    + "</html>";

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ahmed.boukhtioua@esprit.tn", "Blackbokh2018");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            return RedirectToAction("Index");
        }

        // GET: paiment/Edit/5
        public ActionResult Annuler(int? id)
        {
            var request = new RestRequest("paiment/annuler/" + id, Method.PUT);
            client.Execute(request);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("ahmed.boukhtioua@esprit.tn");
            mail.To.Add("ahmedboukhtioua@gmail.com");
            mail.Subject = "Etat de votre paiment";
            mail.IsBodyHtml = true;
            mail.Body = "<html>\n"
                    + "<head>\n"
                    + "<style>\n"
                    + ".div_principale a:hover {\n"
                    + "	border-bottom: 1px solid #253c93; }\n"
                    + "</style>\n"
                    + "</head>\n"
                    + "<body>\n"
                    + "<div style=\"width:80%;margin-left:10%;border: 1.5px solid #1b1a19;/* outer shadows  (note the rgba is red, green, blue, alpha) */-webkit-box-shadow: 0px 0px 12px rgba(0, 0, 0, 0.4); -moz-box-shadow: 0px 1px 6px rgba(23, 69, 88, .5);\n"
                    + "\n"
                    + "-webkit-border-radius: 12px;\n"
                    + "-moz-border-radius: 7px; \n"
                    + "border-radius: 7px;\n"
                    + "\n"
                    + "background: -webkit-gradient(linear, left top, left bottom, \n"
                    + "color-stop(0%, white), color-stop(15%, white), color-stop(100%, #D7E9F5)); \n"
                    + "background: -moz-linear-gradient(top, white 0%, white 55%, #D5E4F3 130%); \">\n"
                    + "<img src=\"http://www.tunisietravail.net/uploads/2017/01/otdav.png\" style=\"width: 90%;height: 250;margin-left: 5%;\">\n"
                    + "<div style=\"margin-left:7%;\">\n"
                    + "<p class=\"p\"><h3>Bonjour " + "Bouktioua" + " " + "Ahmed" + ",</h3>\n"
                    + "Bienvenue chez <b>OTDAV</b>. Nous vous informons que votre paiment a été " + "refuser" + "!<br><br>\n"
                    + "<p>\n"
                    + "Vous pouvez accéder au site : <a href=\"\" style=\"color: #253c93;text-decoration: none;\">www.OTDAV.tn</a>\n"
                    + "</p>\n"
                    + "<div>\n"
                    + "</div>\n"
                    + "</body>\n"
                    + "</html>";

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ahmed.boukhtioua@esprit.tn", "Blackbokh2018");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            return RedirectToAction("Index");
        }

        // POST: paiment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Price,Status,Title,bankcard_IdBank,cheque_IdCheque,oeuvreDec_id,userId,virement_IdVirement")] paimentModel paimentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paimentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Users, "id", "accountStatuts", paimentModel.userId);
            return View(paimentModel);
        }

        // GET: paiment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paimentModel paimentModel = db.paimentModels.Find(id);
            if (paimentModel == null)
            {
                return HttpNotFound();
            }
            return View(paimentModel);
        }

        // POST: paiment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paimentModel paimentModel = db.paimentModels.Find(id);
            db.paimentModels.Remove(paimentModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
