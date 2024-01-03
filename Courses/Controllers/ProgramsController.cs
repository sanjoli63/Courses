using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Courses.Models;

namespace Courses.Controllers
{
    
    public class ProgramsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Programs
        public ActionResult Index()
        {
            var programs = db.Programs.Include(p => p.Field);
            return View(programs.ToList());
        }

        // GET: Programs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs programs = db.Programs.Find(id);
            if (programs == null)
            {
                return HttpNotFound();
            }
            return View(programs);
        }

        // GET: Programs/Create
        public ActionResult Create()
        {
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "FieldName");
            return View();
        }

        // POST: Programs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Programs programs, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                programs.CourseImage = upload.FileName;
                db.Programs.Add(programs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldId = new SelectList(db.Fields, "Id", "FieldName", programs.FieldId);
            return View(programs);
        }

        // GET: Programs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs programs = db.Programs.Find(id);
            if (programs == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "FieldName", programs.FieldId);
            return View(programs);
        }

        // POST: Programs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Programs programs, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    programs.CourseImage = upload.FileName;
                    db.Entry(programs).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("MyImage", "Please select an image for a course");
                }
            }
                ViewBag.FieldId = new SelectList(db.Fields, "Id", "FieldName", programs.FieldId);
            return View(programs);
        }

        // GET: Programs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs programs = db.Programs.Find(id);
            if (programs == null)
            {
                return HttpNotFound();
            }
            return View(programs);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Programs programs = db.Programs.Find(id);
            db.Programs.Remove(programs);
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
