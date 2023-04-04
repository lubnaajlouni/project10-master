using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lubnamaster.Models;

namespace lubnamaster.Controllers
{
    public class ItemsController : Controller
    {
        private storemasterEntities db = new storemasterEntities();

        // GET: Items
        public ActionResult Index()
        {
            ViewBag.x = "item";
            return View(db.Items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Item_Id,Item_Name,Item_Description,ItemPrice,Gender,CategoryId,Image1,Image2,Image3,Image4,Image5")] Item item  , HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4, HttpPostedFileBase Image5)
        {
            if (ModelState.IsValid)
            {
               

                if (Image1 != null)
                {
                  
                    Guid guid1 = Guid.NewGuid();

                    item.Image1 = guid1 + "-" + Image1.FileName;
                    string itemimage = guid1 + "-" + Image1.FileName;
                    Image1.SaveAs(Server.MapPath("../Images/" + itemimage));
                    //db.Items.Add(item);

                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }

                if (Image2 != null)
                {
                    Guid guid1 = Guid.NewGuid();

                    item.Image2 = guid1 + "-" + Image2.FileName;
                    string itemimage = guid1 + "-" + Image2.FileName;
                    Image2.SaveAs(Server.MapPath("../Images/" + itemimage));
                    //db.Items.Add(item);

                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                if (Image3 != null)
                {
                    Guid guid1 = Guid.NewGuid();

                    item.Image3 = guid1 + "-" + Image3.FileName;
                    string itemimage = guid1 + "-" + Image3.FileName;
                    Image3.SaveAs(Server.MapPath("../Images/" + itemimage));
                    //db.Items.Add(item);

                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                if (Image4 != null)
                {
                    Guid guid1 = Guid.NewGuid();

                    item.Image4 = guid1 + "-" + Image4.FileName;
                    string itemimage = guid1 + "-" + Image4.FileName;
                    Image4.SaveAs(Server.MapPath("../Images/" + itemimage));
                    //db.Items.Add(item);

                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                if (Image5 != null)
                {
                    Guid guid1 = Guid.NewGuid();

                    item.Image5 = guid1 + "-" + Image5.FileName;
                    string itemimage = guid1 + "-" + Image5.FileName;
                    Image5.SaveAs(Server.MapPath("../Images/" + itemimage));
                    //db.Items.Add(item);

                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                return View(item);

            }
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Item_Id,Item_Name,Item_Description,ItemPrice,Gender,CategoryId,Image1,Image2,Image3,Image4,Image5")] Item item, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4, HttpPostedFileBase Image5, int? id)
        {
            
                if (ModelState.IsValid)
                {

                    if (Image1 != null)
                    {
                        string imageName = Path.GetFileName(Image1.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Images"), imageName);
                        Image1.SaveAs(imagePath);
                        item.Image1 = imageName;
                    }
                    else
                    {
                        item.Image1 = db.Items.AsNoTracking().Where(i => i.Item_Id == item.Item_Id).Select(i => i.Image1).FirstOrDefault();
                    }

                    if (Image2 != null)
                    {
                        string imageName = Path.GetFileName(Image2.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Images"), imageName);
                        Image2.SaveAs(imagePath);
                        item.Image2 = imageName;
                    }
                    else
                    {
                        item.Image2 = db.Items.AsNoTracking().Where(i => i.Item_Id == item.Item_Id).Select(i => i.Image2).FirstOrDefault();
                    }

                    if (Image3 != null)
                    {
                        string imageName = Path.GetFileName(Image3.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Images"), imageName);
                        Image3.SaveAs(imagePath);
                        item.Image3 = imageName;
                    }
                    else
                    {
                        item.Image3 = db.Items.AsNoTracking().Where(i => i.Item_Id == item.Item_Id).Select(i => i.Image3).FirstOrDefault();
                    }

                    if (Image4 != null)
                    {
                        string imageName = Path.GetFileName(Image4.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Images"), imageName);
                        Image4.SaveAs(imagePath);
                        item.Image4 = imageName;
                    }
                    else
                    {
                        item.Image4 = db.Items.AsNoTracking().Where(i => i.Item_Id == item.Item_Id).Select(i => i.Image4).FirstOrDefault();
                    }

                    if (Image5 != null)
                    {
                        string imageName = Path.GetFileName(Image5.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Images"), imageName);
                        Image5.SaveAs(imagePath);
                        item.Image5 = imageName;
                    }
                    else
                    {
                        item.Image5 = db.Items.AsNoTracking().Where(i => i.Item_Id == item.Item_Id).Select(i => i.Image5).FirstOrDefault();
                    }

                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // Handle invalid model state
                return View(item);
            }
        


            // GET: Items/Delete/5
            public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
