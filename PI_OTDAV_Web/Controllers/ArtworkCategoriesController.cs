using PI_OTDAV_Data;
using PI_OTDAV_Domain.Entities;
using PI_OTDAV_Services.Services;
using PI_OTDAV_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;


namespace PI_OTDAV_Web.Controllers
{
    public class ArtworkCategoriesController : Controller
    {
        Context cx = new Context();

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArtworkCategories
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri("http://localhost:18080");
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = Client.GetAsync("/PI_OTDAV_4GL5B-web/api/artworkcategory").Result;

                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<artworkcategory>>().Result;

                return View();
            }


        }


        // GET: ArtworkCategories/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: ArtworkCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*Bind(Include = "id,nom,type,details,status")] */ArtworkCategory artworkCategories)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.PostAsJsonAsync<ArtworkCategory>("http://localhost:18080/PI_OTDAV_4GL5B-web/api/artworkcategory/add", artworkCategories)
                .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        // GET: ArtworkCategories/Edit/5



        public ActionResult Edit(int id)
        {
            Models.ArtworkCategory ACS = null;

            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:18080/PI_OTDAV_4GL5B-web/api/artworkcategory/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                ACS = response.Content.ReadAsAsync<Models.ArtworkCategory>().Result;

            }

            else
            {
                ViewBag.result = "error";
            }



            return View(ACS);
        }

        // POST: Message/Edit/5
        [HttpPost]
        public ActionResult Edit(ArtworkCategory AC)
        {
            try
            {

                HttpClient Client = new HttpClient();
                HttpResponseMessage response = Client.PutAsJsonAsync<ArtworkCategory>("http://localhost:18080/PI_OTDAV_4GL5B-web/api/artworkcategory/edit", AC).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetDataArtworkCategory()
        {

            var Actif = cx.artworkcategory.Where(x => (bool)x.status == true).Count();
            var NonActif = cx.artworkcategory.Where(x => (bool)x.status == false).Count();

            Ration obj = new Ration();
            obj.actif = Actif;
            obj.nonActif = NonActif;
            obj.SommeStatus = obj.actif + obj.nonActif;
            obj.moyenneAcif = ((obj.actif) * 100) / obj.SommeStatus;
            obj.moyenneNonAcif = ((obj.nonActif) * 100) / obj.SommeStatus;

            //return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ration
        {
            public int actif { get; set; }
            public int nonActif { get; set; }
            public float moyenneAcif { get; set; }
            public float moyenneNonAcif { get; set; }
            public int SommeStatus { get; set; }

        }
    }
}

