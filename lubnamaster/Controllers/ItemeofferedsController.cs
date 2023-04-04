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
    public class ItemeofferedsController : Controller
    {
        private storemasterEntities db = new storemasterEntities();

        // GET: Itemeoffereds
        public ActionResult Index()
        {
            ViewBag.x = "Itemeoffereds";
            var itemeoffereds = db.Itemeoffereds.Include(i => i.Item);
            return View(itemeoffereds.ToList());
        }

        // GET: Itemeoffereds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Itemeoffered itemeoffered = db.Itemeoffereds.Find(id);
            if (itemeoffered == null)
            {
                return HttpNotFound();
            }
            return View(itemeoffered);
        }

        // GET: Itemeoffereds/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name");
            return View();
        }

        // POST: Itemeoffereds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorId,ItemId,ColorName,Size,Quantity,QuantityLeft")] Itemeoffered itemeoffered)
        {
            if (ModelState.IsValid)
            {
                db.Itemeoffereds.Add(itemeoffered);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", itemeoffered.ItemId);
            return View(itemeoffered);
        }

        // GET: Itemeoffereds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Itemeoffered itemeoffered = db.Itemeoffereds.Find(id);
            if (itemeoffered == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", itemeoffered.ItemId);
            return View(itemeoffered);
        }

        // POST: Itemeoffereds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorId,ItemId,ColorName,Size,Quantity,QuantityLeft")] Itemeoffered itemeoffered)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemeoffered).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", itemeoffered.ItemId);
            return View(itemeoffered);
        }

        // GET: Itemeoffereds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Itemeoffered itemeoffered = db.Itemeoffereds.Find(id);
            if (itemeoffered == null)
            {
                return HttpNotFound();
            }
            return View(itemeoffered);
        }

        // POST: Itemeoffereds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Itemeoffered itemeoffered = db.Itemeoffereds.Find(id);
            db.Itemeoffereds.Remove(itemeoffered);
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
