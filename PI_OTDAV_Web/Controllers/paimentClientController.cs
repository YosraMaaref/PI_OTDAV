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
using PI_OTDAV_Domain.Entities;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.IO;

namespace PI_OTDAV_Web.Controllers
{
    public class paimentClientController : Controller
    {

        chequeRest cheque1;
        virementRest virement1;
        private PI_OTDAV_WebContext db = new PI_OTDAV_WebContext();

        private RestClient client = new RestClient("http://localhost:18080/PI_OTDAV_4GL5B-web/api/");

        List<paimentModel> listPaimentCotisation = new List<paimentModel>();
        List<paimentModel> listPaimentDeposit = new List<paimentModel>();
        List<paimentModel> listPaimentDeclaration = new List<paimentModel>();

        public paimentClientController()
        {
            RestRequest request = new RestRequest("paiment/cotisation/1", Method.GET);
            IRestResponse<List<paimentModel>> response = client.Execute<List<paimentModel>>(request);
            //list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocumentModels>>(response.Content);
            if (!response.Content.Equals(""))
                listPaimentCotisation.AddRange(response.Data);

            request = new RestRequest("paiment/declaration/1", Method.GET);
            response = client.Execute<List<paimentModel>>(request);
            if (!response.Content.Equals(""))
                listPaimentDeclaration.AddRange(response.Data);


            request = new RestRequest("paiment/depot/1", Method.GET);
            response = client.Execute<List<paimentModel>>(request);
            //listPaimentDeposit = Newtonsoft.Json.JsonConvert.DeserializeObject<List<paimentModel>>(response.Content);
            if (!response.Content.Equals(""))
                listPaimentDeposit.AddRange(response.Data);




        }
        // GET: paimentModels
        public ActionResult Index()
        {
            listPaimentDeclaration.OrderBy(e => e.Date);
            listPaimentDeposit.OrderBy(e => e.Date);
            ViewBag.listPaimentDeposit = listPaimentDeposit;
            ViewBag.listPaimentDeclaration = listPaimentDeclaration;

            return View(listPaimentCotisation);
        }

        // GET: paimentModels/Details/5
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

        // GET: paimentModels/Create
        public ActionResult Create()

        {



            return View();
        }

        // POST: paimentModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Price,Status,Title,bankcard_IdBank,cheque_IdCheque,oeuvreDec_id,userId,virement_IdVirement,cheque,virement")] paimentModel paimentModel, HttpPostedFileBase Image1)
        {

            if (paimentModel.cheque != null || paimentModel.virement != null)
            {
                var request = new RestRequest("paiment", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                if (paimentModel.cheque.bank != null)
                {
                    var path = Path.GetFullPath("C:\\xampp\\htdocs\\pi\\" + Image1.FileName);
                    Image1.SaveAs(path);
                    cheque1 = new chequeRest()
                    {
                        num = paimentModel.cheque.Num,
                        agence = paimentModel.cheque.agence,
                        bank = paimentModel.cheque.bank,
                        price = paimentModel.cheque.Price,
                        image = "http://localhost:8080/pi/" + Image1.FileName
                    };

                }
                else if (paimentModel.virement.bank != null)
                {
                    virement1 = new virementRest()
                    {
                        agence = paimentModel.virement.agence,
                        bank = paimentModel.virement.bank,
                        codeVirement = paimentModel.virement.codeVirement
                    };
                }
                paimentRest p = new paimentRest()
                {
                    //user = new UserRest() { id = 2},
                    title = paimentModel.Title,
                    date = 0,
                    price = paimentModel.Price,
                    status = 0,
                    cheque = cheque1,
                    virement = virement1,
                    user = new UserRest() { id=1}
                };
                Debug.WriteLine(new JavaScriptSerializer().Serialize(p));
                request.AddJsonBody(p);
                client.Execute(request);
                cheque1 = null;
                virement1 = null;
                return RedirectToAction("Index");
            }

            return View(paimentModel);
        }

        // GET: paimentModels/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.userId = new SelectList(db.Users, "id", "accountStatuts", paimentModel.userId);
            return View(paimentModel);
        }

        // POST: paimentModels/Edit/5
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

        // GET: paimentModels/Delete/5
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

        // POST: paimentModels/Delete/5
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
