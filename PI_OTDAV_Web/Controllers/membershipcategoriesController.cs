using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PI_OTDAV_Data;
using PI_OTDAV_Domain.Entities;
using PI_OTDAV_Services.Services;

namespace PI_OTDAV_Web.Controllers
{
    public class membershipcategoriesController : Controller
    {
        IMembershipCategoryService MCS;

        private Context db = new Context();

       public membershipcategoriesController()
        {
            MCS = new MembershipCategoryService();
        }
        // GET: membershipcategories
        public ActionResult Index()
        {
            var mem = MCS.getAllmembershipcategory();
            return View(mem);
        }

        // GET: membershipcategories/Details/5
        public ActionResult Details(int id)
        {
            var mem = MCS.getmembershipcategoryById(id);
           /* if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            membershipcategory membershipcategory = db.membershipcategory.Find(id);
            if (membershipcategory == null)
            {
                return HttpNotFound();
            }*/
            return View(mem);
        }

        // GET: membershipcategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: membershipcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "id,details,nom,status,type")]*/ membershipcategory membershipcategory)
        {
            // if (ModelState.IsValid)
            //{
            membershipcategory mem = new membershipcategory
            {
                nom = membershipcategory.nom,
                type = membershipcategory.type,
                details = membershipcategory.details,
                status = membershipcategory.status

            };
            MCS.createmembership(mem);


                //db.membershipcategory.Add(membershipcategory);
                //db.SaveChanges();
                return RedirectToAction("Index");
            //}
            
            //return View(membershipcategory);
        }

        // GET: membershipcategories/Edit/5
        public ActionResult Edit(int id)
        {
            membershipcategory mem = MCS.getmembershipcategoryById(id); ;
            return View(mem);
            /* if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             membershipcategory membershipcategory = db.membershipcategory.Find(id);
             if (membershipcategory == null)
             {
                 return HttpNotFound();
             }
             return View(membershipcategory);*/
        }

        // POST: membershipcategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "id,details,nom,status,type")] */membershipcategory membershipcategory,int id)
        {
            try
            {
                membershipcategory mem = MCS.getmembershipcategoryById(id);
                mem.nom = membershipcategory.nom;
                mem.type = membershipcategory.type;
                mem.details =membershipcategory.details;
                mem.status =membershipcategory.status;
                MCS.updatemembershipcategory(mem);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
           /* if (ModelState.IsValid)
            {
                db.Entry(membershipcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membershipcategory);*/
        }

        // GET: membershipcategories/Delete/5
       /* public ActionResult Delete(int id)
        {


            var mem = MCS.getmembershipcategoryById(id);
            MCS.deletemembershipcategory(mem);
            return View(mem);
        }*/

        // POST: membershipcategories/Delete/5
    /*    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            membershipcategory membershipcategory = db.membershipcategory.Find(id);
            db.membershipcategory.Remove(membershipcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */
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
