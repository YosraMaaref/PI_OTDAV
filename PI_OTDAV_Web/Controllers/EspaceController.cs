using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PI_OTDAV_Web.Controllers
{
    public class EspaceController : Controller
    {
        // GET: Espace


        [HttpGet]
        public ActionResult DisplayEspace()
        {
            System.Net.Http.HttpClient Client = new System.Net.Http.HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpResponseMessage response = Client.GetAsync("/PI_OTDAV_4GL5B-web/api/Espace").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Models.EspaceModel>>().Result;
            }
            else
            {
                ViewBag.result = "error";
            }
            return View();

        }






    }
}