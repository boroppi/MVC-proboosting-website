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
    public class BoostersController : Controller
    {
        private BoostTaskModel db = new BoostTaskModel();

        // GET: Boosters
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Boosters.ToList().OrderBy(b => b.FullName));
        }
        // GET: Boosters/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booster booster = db.Boosters.Find(id);
            if (booster == null)
            {
                return HttpNotFound();
            }
            return View(booster);
        }

        // GET: Boosters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boosters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoosterId,FirstName,LastName,Email,DateCreated")] Booster booster)
        {   
            // DateCreated is now
            booster.DateCreated = DateTime.Now;
            // Force default value to be true
            booster.IsAvailable = true;
            // Force email to be lower case when it is saved to DB
            booster.Email = booster.Email.ToLower();

            if (ModelState.IsValid)
            {
                db.Boosters.Add(booster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booster);
        }

        // GET: Boosters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booster booster = db.Boosters.Find(id);
            if (booster == null)
            {
                return HttpNotFound();
            }
            return View(booster);
        }

        // POST: Boosters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoosterId,FirstName,LastName,Email,IsAvailable")] Booster booster)
        {
            // Force email to be lower case when it is saved to DB
            booster.Email = booster.Email.ToLower();
             
            if (ModelState.IsValid)
            {
                db.Entry(booster).State = EntityState.Modified;
                db.Entry(booster).Property(b => b.DateCreated).IsModified = false;
                db.Entry(booster).Property(b => b.LastLogon).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booster);
        }

        // GET: Boosters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booster booster = db.Boosters.Find(id);
            if (booster == null)
            {
                return HttpNotFound();
            }
            return View(booster);
        }

        // POST: Boosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booster booster = db.Boosters.Find(id);
            db.Boosters.Remove(booster);
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
