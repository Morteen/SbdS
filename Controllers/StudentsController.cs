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
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {

           
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,Fname,Lname,Adr,Username,Age,ParentName,Password,ConfirmPassword,Email,IsAdmin")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,Fname,Lname,Adr,Username,Age,ParentName,Password,ConfirmPassword,Email,IsAdmin")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

        

       
        public ActionResult LoggedInStudent()
        {
            ViewModel model = new ViewModel();

            
            List<Course>listCourse= db.Courses.ToList();
            int tempId = Convert.ToInt32(Session["UserId"]);
            if (db.UserAtCourses != null) {  
           List<UserAtCourse>listUserAt = db.UserAtCourses.Where(u => u.StudentId == tempId).ToList();
                model.UserAtCourseList = listUserAt;
            }
            model.CourseList = listCourse;
           




            return View(model);
        }

        public ActionResult LoggedInAdmin()
        {
            ViewModel model = new ViewModel();


           
           
            if (db != null)
            {
               
                model.UserAtCourseList = db.UserAtCourses.ToList(); 
                model.StudentList = db.Students.ToList();
                model.CourseList = db.Courses.ToList();
            }
           





            return View(model);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }



        



        [HttpPost]
        public JsonResult Login(string Username, string Password)
        {
            var student = db.Students.Single(u => u.Username == Username && u.Password == Password);
            if (student == null)
            {
                ModelState.AddModelError("", "Passord eller brukernavn er galt");
                return Json(null, JsonRequestBehavior.AllowGet); // no

                



            }
            else
            {

                Session["UserId"] = student.StudentId.ToString();
                Session["UserName"] = student.Fname.ToString();

                if (student.IsAdmin)
                {
                    Session["IsAdmin"] = "Admin";
                    
                }
                else if (!student.IsAdmin)
                {
                    Session["IsAdmin"] = "Student";
                   
                }

            }
            return Json(student.IsAdmin, JsonRequestBehavior.AllowGet); // yes

        }



        public ActionResult AddToCourse(int id)
        {
            if (ModelState.IsValid)
            {
                bool check = true;
                int StudentId = Convert.ToInt32(Session["UserId"]);
                List<UserAtCourse> Checklist = db.UserAtCourses.ToList();
                foreach (UserAtCourse i in Checklist) {
                    if(i.CourseId==id&&i.StudentId== StudentId)
                    {
                        check = false;
                    }
                }
                if (check) { 
                UserAtCourse at = new UserAtCourse();
                at.CourseId = id;
                at.StudentId = StudentId;
                           
                db.UserAtCourses.Add(at);
                db.SaveChanges();
                }




            }




            return RedirectToAction("LoggedInStudent");
        }

        public ActionResult MyCourses()

        {

            if (Session["UserId"] != null)
            {
                ViewModel model = new Models.ViewModel();
                int tempId = Convert.ToInt32(Session["UserId"]);
                
                return View(model);


            }
            else
            {
                return RedirectToAction("Login");
            }

           
        }






    }
}
