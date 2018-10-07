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
    public class BoostersController : Controller
    {
        private BoostTaskModel db = new BoostTaskModel();

        // GET: Boosters
        public ActionResult Index()
        {
            return View(db.Boosters.ToList());
        }

        // GET: Boosters/Details/5
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
        public ActionResult Create([Bind(Include = "BoosterId,FirstName,LastName,Email")] Booster booster)
        {
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
        public ActionResult Edit([Bind(Include = "BoosterId,FirstName,LastName,Email")] Booster booster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booster).State = EntityState.Modified;
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
