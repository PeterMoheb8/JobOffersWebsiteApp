using JobOffersWebsiteApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {


            return View(db.Categories.ToList());
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);

            if (job == null)
            {
                return HttpNotFound();
            }

            Session["JobId"] = JobId;
            return View(job);

        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var Check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (Check.Count < 1 )
            {
                var Job = new ApplyForJob();

                Job.JobId = JobId;
                Job.UserId = UserId;
                Job.Meesage = Message;
                Job.ApplayDate = DateTime.Now;

                db.ApplyForJobs.Add(Job);
                db.SaveChanges();
                ViewBag.Result = "تمت الاضافة بنجاح "; 
            }
            else
            {
                ViewBag.Result = "المعذرة , لقد سبق وقدمت الي الوظيفة";
            }

      

            return View();
        }



        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());

        }

        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }

          
            return View(job);
        }


        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID 
                       select app;


            var grouped = from j in Jobs
                          group j by j.Job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr

                          };

            return View(grouped.ToList());
        }



        //Get : Edit
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }


        // POST: Edit
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplayDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");

            }
            return View(job);
        }

        //Get Delete
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);

        }

        //Post Delete

        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {

            job.ApplayDate = DateTime.Now;
            var MyRole = db.Roles.Find(job.Id);
            db.Roles.Remove(MyRole);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");



        }

        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var Result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDeccriptions.Contains(searchName)).ToList();

            return View(Result);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {

            var mail = new MailMessage();
            var LoginInfo = new NetworkCredential("petermoheb888@gmail.com", "PeterMoheb123@@");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("petermoheb888@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل :" + contact.Name + "<br>" +
                          " بريد المرسل :" + contact.Email + "<br>" +
                          "  عنوان الرسالة:" + contact.Subject + "<br>" +
                          "نص الرسالة:<b>" + contact.Message + "</b>";
            mail.Body = body;

            var smtClient = new SmtpClient("smtp.gmail.com", 587);
            smtClient.EnableSsl = true;
            smtClient.Credentials = LoginInfo;
            smtClient.Send(mail);
            return RedirectToAction("Index");
        }
    }
}