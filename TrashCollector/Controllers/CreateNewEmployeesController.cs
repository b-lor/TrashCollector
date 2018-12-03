using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CreateNewEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CreateNewEmployees
        public ActionResult Index()
        {
            return View(db.CreateNewEmployees.ToList());
        }

        // GET: CreateNewEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateNewEmployee createNewEmployee = db.CreateNewEmployees.Find(id);
            if (createNewEmployee == null)
            {
                return HttpNotFound();
            }
            return View(createNewEmployee);
        }

        // GET: CreateNewEmployees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreateNewEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,ZipCode,Username,Email,Password,ConfirmPassword")] CreateNewEmployee createNewEmployee)
        {
            if (ModelState.IsValid)
            {
                db.CreateNewEmployees.Add(createNewEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = createNewEmployee.FirstName + " " + createNewEmployee.LastName + " successfully registered.";

            return View(createNewEmployee);
        }

        // GET: CreateNewEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateNewEmployee createNewEmployee = db.CreateNewEmployees.Find(id);
            if (createNewEmployee == null)
            {
                return HttpNotFound();
            }
            return View(createNewEmployee);
        }

        // POST: CreateNewEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,ZipCode,Username,Email,Password,ConfirmPassword")] CreateNewEmployee createNewEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(createNewEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createNewEmployee);
        }

        // GET: CreateNewEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateNewEmployee createNewEmployee = db.CreateNewEmployees.Find(id);
            if (createNewEmployee == null)
            {
                return HttpNotFound();
            }
            return View(createNewEmployee);
        }

        // POST: CreateNewEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreateNewEmployee createNewEmployee = db.CreateNewEmployees.Find(id);
            db.CreateNewEmployees.Remove(createNewEmployee);
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

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(CreateNewEmployee createNewEmployee)
        {
            var newEmployee = db.CreateNewEmployees.Single(e => e.Username == createNewEmployee.Username && e.Password == createNewEmployee.Password);
            if (newEmployee != null)
            {
                Session["Id"] = newEmployee.Id.ToString();
                Session["Username"] = newEmployee.Username.ToString();
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong");
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
