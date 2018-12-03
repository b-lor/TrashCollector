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
    public class CreateNewCustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CreateNewCustomers
        public ActionResult Index()
        {
            return View(db.CreateNewCustomers.ToList());
        }

        // GET: CreateNewCustomers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateNewCustomer createNewCustomer = db.CreateNewCustomers.Find(id);
            if (createNewCustomer == null)
            {
                return HttpNotFound();
            }
            return View(createNewCustomer);
        }

        // GET: CreateNewCustomers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreateNewCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address1,Address2,City,State,ZipCode,Username,Email,Password,ConfirmPassword")] CreateNewCustomer createNewCustomer)
        {
            if (ModelState.IsValid)
            {
                db.CreateNewCustomers.Add(createNewCustomer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = createNewCustomer.FirstName + " " + createNewCustomer.LastName + " successfully registered.";

            return View(createNewCustomer);
        }

        // GET: CreateNewCustomers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateNewCustomer createNewCustomer = db.CreateNewCustomers.Find(id);
            if (createNewCustomer == null)
            {
                return HttpNotFound();
            }
            return View(createNewCustomer);
        }

        // POST: CreateNewCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address1,Address2,City,State,ZipCode,Username,Email,Password,ConfirmPassword")] CreateNewCustomer createNewCustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(createNewCustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createNewCustomer);
        }

        // GET: CreateNewCustomers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateNewCustomer createNewCustomer = db.CreateNewCustomers.Find(id);
            if (createNewCustomer == null)
            {
                return HttpNotFound();
            }
            return View(createNewCustomer);
        }

        // POST: CreateNewCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreateNewCustomer createNewCustomer = db.CreateNewCustomers.Find(id);
            db.CreateNewCustomers.Remove(createNewCustomer);
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
        public ActionResult Login(CreateNewCustomer createNewCustomer)
        {
            var newEmployee = db.CreateNewCustomers.Single(e => e.Username == createNewCustomer.Username && e.Password == createNewCustomer.Password);
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
