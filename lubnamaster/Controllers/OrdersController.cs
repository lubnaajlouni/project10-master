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
    public class OrdersController : Controller
    {
        private storemasterEntities db = new storemasterEntities();

        // GET: Orders
        public ActionResult Index()
        {
            ViewBag.x = "Orders";

            var orders = db.Orders.Include(o => o.AspNetUser).Include(o => o.City1);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.x = "Orders";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.x = "Orders";

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.City = new SelectList(db.Cities, "CityId", "CityName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,FirstName,LastName,City,Adress,UserId,Email,PhoneNumber,PaymentMethod,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            ViewBag.City = new SelectList(db.Cities, "CityId", "CityName", order.City);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.x = "Orders";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            ViewBag.City = new SelectList(db.Cities, "CityId", "CityName", order.City);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,FirstName,LastName,City,Adress,UserId,Email,PhoneNumber,PaymentMethod,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            ViewBag.City = new SelectList(db.Cities, "CityId", "CityName", order.City);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.x = "Orders";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
