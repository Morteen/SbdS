using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SbdS.Models;

namespace SbdS.Controllers
{
    public class UserAtCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserAtCourses
        public ActionResult Index()
        {
            var usCourses = db.UserAtCourses.Include(u => u.Course).Include(u => u.Student);
            return View(usCourses.ToList());
        }

        // GET: UserAtCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAtCourse userAtCourse = db.UserAtCourses.Find(id);
            if (userAtCourse == null)
            {
                return HttpNotFound();
            }
            return View(userAtCourse);
        }

        // GET: UserAtCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Fname");
            return View();
        }

        // POST: UserAtCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AtCourseId,IsPayed,StudentId,CourseId")] UserAtCourse userAtCourse)
        {
            if (ModelState.IsValid)
            {
                db.UserAtCourses.Add(userAtCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", userAtCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Fname", userAtCourse.StudentId);
            return View(userAtCourse);
        }

        // GET: UserAtCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAtCourse userAtCourse = db.UserAtCourses.Find(id);
            if (userAtCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", userAtCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Fname", userAtCourse.StudentId);
            return View(userAtCourse);
        }

        // POST: UserAtCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AtCourseId,IsPayed,StudentId,CourseId")] UserAtCourse userAtCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAtCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", userAtCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Fname", userAtCourse.StudentId);
            return View(userAtCourse);
        }

        // GET: UserAtCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAtCourse userAtCourse = db.UserAtCourses.Find(id);
            if (userAtCourse == null)
            {
                return HttpNotFound();
            }
            return View(userAtCourse);
        }

        // POST: UserAtCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAtCourse userAtCourse = db.UserAtCourses.Find(id);
            db.UserAtCourses.Remove(userAtCourse);
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


        [HttpPost]
        public JsonResult AtCourseDelete(int AtCourseId)
        {

            UserAtCourse at = db.UserAtCourses.Find(AtCourseId);
            db.UserAtCourses.Remove(at);
            db.SaveChanges();

            //Check to see if Atcourse was actually deleted
            if (db.UserAtCourses.Find(AtCourseId) == null)
                return Json(true, JsonRequestBehavior.AllowGet); // yes
            else
                return Json(null, JsonRequestBehavior.AllowGet); // no
        }





    }
}
