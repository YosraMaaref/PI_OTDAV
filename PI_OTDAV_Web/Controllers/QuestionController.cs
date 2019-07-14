

using PI_OTDAV_Domain;
using PI_OTDAV_Services;
using PI_OTDAV_Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PI_OTDAV_Web.Controllers
{
    public class QuestionController : Controller
    {





        WebService ws = new WebService();
        String path = "http://localhost:8585/pidev_neoxam-web/rest/question/";

        // GET: Question
        public async Task<ActionResult> Index()
        {
            IEnumerable<Question> questions = await ws.GetProductAsync("http://localhost:8585/pidev_neoxam-web/rest/question");
            List <QuestionVM> questionVM = new List<QuestionVM>();

            foreach (Question q in questions)
            {

                QuestionVM b = new QuestionVM();

                b.questionId = q.questionId;
                b.question = q.question;
                b.prop1 = q.prop1;
                b.prop2 = q.prop2;
                b.prop3 = q.prop3;
                b.prop4 = q.prop4;
                b.response = q.response;
                b.description = q.description;
                questionVM.Add(b);

            }
            

            return View(questionVM);
        }

        // GET: Question/Details/5
        public async Task<ActionResult> Details(int questionId)
        {

            Question q = await ws.GetProductAsyncByPath(path + questionId);
            QuestionVM b = new QuestionVM();

            b.questionId = q.questionId;
            b.question = q.question;
            b.prop1 = q.prop1;
            b.prop2 = q.prop2;
            b.prop3 = q.prop3;
            b.prop4 = q.prop4;
            b.response = q.response;
            b.description = q.description;


            return View(b);
        }

        // GET: Question/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: Question/Create
        [HttpPost]
        public async Task<ActionResult> Create(QuestionVM BVM)
        {


            Question b = new Question
            {
                question = BVM.question,
                prop1 = BVM.prop1,
                prop2 = BVM.prop2,
                prop3 = BVM.prop3,
                prop4 = BVM.prop4,
                response = BVM.response,
                description = BVM.description




            };

            Uri u = await ws.CreateProductAsync(b);

            return RedirectToAction("Create");
        }

        // GET: Question/Edit/5
        public async Task<ActionResult> Edit(int questionId)
        {
            try
            {

                Question q = await ws.GetProductAsyncByPath(path + questionId);
                QuestionVM b = new QuestionVM();

                b.questionId = q.questionId;
                b.question = q.question;
                b.prop1 = q.prop1;
                b.prop2 = q.prop2;
                b.prop3 = q.prop3;
                b.prop4 = q.prop4;
                b.response = q.response;
                b.description = q.description;

                //Question u = await ws.UpdateProductAsync(b);
                return View(b);
            }
            catch
            {
                return View();
            }
        }

        // POST: Question/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int questionId, QuestionVM q)
        {

            Question b = new Question();//await ws.GetProductAsyncByPath(path+questionId);
            Console.WriteLine(q);

                b.questionId = q.questionId;
                b.question = q.question;
                b.prop1 = q.prop1;
                b.prop2 = q.prop2;
                b.prop3 = q.prop3;
                b.prop4 = q.prop4;
                b.response = q.response;
                b.description = q.description;

                Question u = await ws.UpdateProductAsync(b);
                return RedirectToAction("Index");
            
        }

        // GET: Question/Delete/5
        public async Task<ActionResult> Delete(int questionId)
        {
            try
            {
                await ws.DeleteProductAsync(questionId);
                // TODO: Add delete logic here



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Question/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int questionId, QuestionVM BVM)
        {
            try
            {
                await ws.DeleteProductAsync(questionId);
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
