using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc_proboosting.Models;

namespace mvc_proboosting.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        // disable the db connection
        // private BoostTaskModel db = new BoostTaskModel();

        private ICustomersMock db;

        // default constructor uses the live db
        public CustomersController()
        {
            this.db = new EFCustomers();
        }

        // mock constructor
        public CustomersController(ICustomersMock mock)
        {
            this.db = mock;
        }

        // GET: Customers
        [AllowAnonymous]
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Booster).OrderBy(c => c.FullName);
            return View("Index", customers.ToList());
        }

        [AllowAnonymous]
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // show error page instead of http status code
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return this.View("Error");
            }
            Customer customer = db.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                //return HttpNotFound();
                return View("Error");

            }
            return View("Details", customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BoosterId = new SelectList(db.Boosters, "BoosterId", "FullName");
            return View("Create");
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoosterId,FirstName,LastName,Email,DateCreated")] Customer customer)
        {
            // Set DateCreated to now.
            customer.DateCreated = DateTime.Now;
            // Force Email to be lowercase
            customer.Email = customer.Email.ToLower();

            if (ModelState.IsValid)
            {
                //db.Customers.Add(customer);
                db.Save(customer);
                return RedirectToAction("Index");
            }

            ViewBag.BoosterId = new SelectList(db.Boosters, "BoosterId", "FirstName", customer.BoosterId);
            return View("Create", customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return this.View("Error");
            }

            Customer customer = db.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                //return HttpNotFound();
                return this.View("Error");
            }

            ViewBag.BoosterId = new SelectList(db.Boosters, "BoosterId", "FullName", customer.BoosterId);
            return View("Edit", customer);
        }

        //// POST: Customers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CustomerId,BoosterId,FirstName,LastName,Email")] Customer customer)
        //{
        //    // Force Email to be lowercase
        //    customer.Email = customer.Email.ToLower();

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(customer).State = EntityState.Modified;
        //        db.Entry(customer).Property(c => c.DateCreated).IsModified = false;
        //        db.Entry(customer).Property(c => c.LastLogon).IsModified = false;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BoosterId = new SelectList(db.Boosters, "BoosterId", "FirstName", customer.BoosterId);
        //    return View(customer);
        //}

        //// GET: Customers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Customer customer = db.Customers.Find(id);
        //    db.Customers.Remove(customer);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
