using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lubnamaster.Models;

namespace lubnamaster.Controllers
{
    public class CategoreisController : Controller
    {
        private storemasterEntities db = new storemasterEntities();

        // GET: Categoreis
        public ActionResult Index()
        {
            return View(db.Categoreis.ToList());
        }

        // GET: Categoreis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorei categorei = db.Categoreis.Find(id);
            if (categorei == null)
            {
                return HttpNotFound();
            }
            return View(categorei);
        }

        // GET: Categoreis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoreis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Categorei categorei)
        {
            if (ModelState.IsValid)
            {
                db.Categoreis.Add(categorei);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorei);
        }

        // GET: Categoreis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorei categorei = db.Categoreis.Find(id);
            if (categorei == null)
            {
                return HttpNotFound();
            }
            return View(categorei);
        }

        // POST: Categoreis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] Categorei categorei)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorei).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorei);
        }

        // GET: Categoreis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorei categorei = db.Categoreis.Find(id);
            if (categorei == null)
            {
                return HttpNotFound();
            }
            return View(categorei);
        }

        // POST: Categoreis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorei categorei = db.Categoreis.Find(id);
            db.Categoreis.Remove(categorei);
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
