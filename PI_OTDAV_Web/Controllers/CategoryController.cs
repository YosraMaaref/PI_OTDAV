using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PI_OTDAV_Web.Models;
using System.Threading.Tasks;
using PI_OTDAV_Domain;
using PI_OTDAV_Services;

namespace PI_OTDAV_Web.Controllers
{
    public class CategoryController : Controller
    {
        WebServiceCategory ws = new WebServiceCategory();
        String path = "http://localhost:18080/piOtdav-web/rest/categories/";
        // GET: Category
        public async Task<ActionResult> Index()
        {
            IEnumerable<Category> categories = await ws.GetCategory("http://localhost:18080/piOtdav-web/rest/categories");
            List<CategoryVM> categoriesVM = new List<CategoryVM>();

            foreach (Category q in categories)
            {

                CategoryVM c = new CategoryVM();

                c.idCategory = q.idCategory;
                c.libelle = q.libele;
                


                categoriesVM.Add(c);

            }


            return View(categoriesVM);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        
        }
        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryVM CVM)
        {


            Category c= new Category
            {
                libele = CVM.libelle

            



            };

            Uri u = await ws.CreateCategoryAsync(c);

            return RedirectToAction("Create");
        }



        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
