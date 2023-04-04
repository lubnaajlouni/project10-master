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
    public class OrderDetailsController : Controller
    {
        private storemasterEntities db = new storemasterEntities();

        // GET: OrderDetails
        public ActionResult Index()
        {
            ViewBag.x = "OrdersDetails";

            var orderDetails = db.OrderDetails.Include(o => o.AspNetUser).Include(o => o.Item).Include(o => o.Order);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.x = "OrdersDetails";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.x = "OrdersDetails";
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "FirstName");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetaileId,OrderId,UserId,ItemId,Quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", orderDetail.UserId);
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", orderDetail.ItemId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "FirstName", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.x = "OrdersDetails";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", orderDetail.UserId);
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", orderDetail.ItemId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "FirstName", orderDetail.OrderId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetaileId,OrderId,UserId,ItemId,Quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", orderDetail.UserId);
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", orderDetail.ItemId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "FirstName", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.x = "OrdersDetails";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
