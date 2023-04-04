using lubnamaster.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace lubnamaster.Controllers
{
    public class HomeController : Controller
    {
        storemasterEntities db = new storemasterEntities();
        
        public ActionResult Home()
        {

            var items = db.Items.ToList();
            return View(items);
        }

        public ActionResult Items(string gender = "", int categoryId = 0)
        {
            var items = db.Items.AsQueryable();

            if (!string.IsNullOrEmpty(gender))
            {
                items = items.Where(x => x.Gender == gender);
            }

            if (categoryId != 0)
            {
                items = items.Where(x => x.CategoryId == categoryId);
            }

            return View(items.ToList());
        }





        public ActionResult MEN(int categoryId = 0)
        {
            const string MEN = "MEN";
            var items = db.Items.Where(x => x.Gender == MEN);

            if (categoryId != 0)
            {
                items = items.Where(x => x.CategoryId == categoryId);
            }

            return View(items.ToList());

        }



        public ActionResult WOMEN(int categoryId = 0)
        {
            const string WOMEN = "WOMEN";
            var items = db.Items.Where(x => x.Gender == WOMEN);
            var itemofferd = db.Itemeoffereds.ToList();

            string size = Request.QueryString["Size"];
            if (!string.IsNullOrEmpty(size))
            {
                itemofferd = itemofferd.Where(x => x.Size == size).ToList();
            }

            if (categoryId != 0)
            {
                items = items.Where(x => x.CategoryId == categoryId);
            }

            ViewBag.CategoryId = categoryId;
            return View(items.ToList());
        }







        public ActionResult shopdetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Items");
            }
            else
            {
                var item = db.Items.Find(id);
                var itemoffer = db.Itemeoffereds.Where(x=>x.Item.Item_Id==id);
          
                var reviews = db.Reviews.Where(r => r.ItemId == id).ToList();
                var itemsizes = db.Itemeoffereds
                  .Where(io => io.Item.Item_Id == id)
                  .GroupBy(io => io.Size)
                  .Select(ioGroup => ioGroup.FirstOrDefault());
                dynamic all = new ExpandoObject();
                all.m = item;
                all.s = itemoffer;
                all.z = itemsizes;
                all.r = reviews;
                ViewBag.messege = "get";
                return View(all);

            }
        }



        public void AddToCart(int itemId, string userId, int quantity)
        {
           
            {
                var cart = db.Carts.SingleOrDefault(c => c.ItemId == itemId && c.UserId == userId);

                if (cart == null)
                {
                    // Create a new cart item if no cart exists for the specified item and user
                    cart = new Cart
                    {
                        ItemId = itemId,
                        UserId = userId,
                        Quantity = quantity
                    };
                    db.Carts.Add(cart);
                }
                else
                {
                    // If the cart item already exists, update its quantity
                    cart.Quantity += quantity;
                }

                // Reduce the quantity of the selected item in Itemeoffereds table
                var itemOffered = db.Itemeoffereds.SingleOrDefault(io => io.ItemId == itemId);
                if (itemOffered != null)
                {
                    itemOffered.QuantityLeft -= quantity;
                }
                else
                {
                    // Handle error: item not found in Itemeoffereds table
                    throw new Exception("Selected item not found");
                }

                db.SaveChanges();
            }
        }






        public ActionResult Shopcart()
        {
            return View();
        }



      


        [HttpPost]
        public ActionResult AddReview(int id, string description,string name)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Set the user id to the current user's id
                var userId = User.Identity.GetUserId();

                // Create a new review object
                var review = new Review
                {
                    UserId = userId,
                    ItemId = id,
                    Description = description,
                    Name = name,
                    Date = DateTime.Now,
                };

                // Add the review to the database
                db.Reviews.Add(review);
                db.SaveChanges();

                // Redirect back to the shopdetails page
                return RedirectToAction("shopdetails", new { id, name });
            }

            // If the model state is not valid, return the view with the errors
            return View();
        }





        //[HttpPost]
        //public ActionResult AddToCart(int itemId, int colorId, string size)
        //{
        //    // Check if the user is authenticated
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        // Redirect the user to the login page
        //        return RedirectToAction("Login", "Account");
        //    }

        //    // Get the user's UserId
        //    string userId = User.Identity.Name;

        //    // Find the item offered based on the item ID, color ID, and size
        //    var itemOffered = db.Itemeoffereds
        //        .SingleOrDefault(io => io.ItemId == itemId && io.ColorId == colorId && io.Size == size);

        //    // If the item offered doesn't exist or has no quantity left, return an error
        //    if (itemOffered == null || itemOffered.QuantityLeft == 0)
        //    {
        //        return RedirectToAction("Item", "Home", new { id = itemId });
        //    }

        //    // Find the cart for the current user and the item being added
        //    var cart = db.Carts
        //        .SingleOrDefault(c => c.UserId == userId && itemId == itemOffered.ColorId);

        //    // If the cart doesn't exist, create a new one
        //    if (cart == null)
        //    {
        //        cart = new Cart
        //        {
        //            ItemId = itemOffered.ColorId,
        //            UserId = userId,
        //            Quantity = 1
        //        };
        //        db.Carts.Add(cart);
        //    }
        //    // If the cart already exists, update the quantity
        //    else
        //    {
        //        cart.Quantity += 1;
        //        db.Entry(cart).State = EntityState.Modified;
        //    }

        //    // Save changes to the database
        //    db.SaveChanges();

        //    // Return the updated cart count
        //    int cartCount = db.Carts.Count(c => c.UserId == userId);
        //    return Json(new { count = cartCount });
        //}


        //[HttpPost]
        //public ActionResult AddTestimonial(TestimonialViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = User.Identity.GetUserId();
        //        var review = new Review
        //        {
        //            UserId = userId,
        //            Description = model.Description
        //        };
        //        db.Reviews.Add(review);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View("Index", model);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult AddReview(Review review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        review.UserId = User.Identity.GetUserId();
        //        db.Reviews.Add(review);
        //        db.SaveChanges();
        //        return RedirectToAction("Shopdetails", new { id = review.ItemId });
        //    }
        //    else
        //    {
        //        If the model state is not valid, redisplay the form with validation errors
        //        var item = db.Items.Find(review.ItemId);
        //        var itemoffer = db.Itemeoffereds.Where(x => x.Item.Item_Id == review.ItemId);
        //        var itemsizes = db.Itemeoffereds
        //          .Where(io => io.Item.Item_Id == review.ItemId)
        //          .GroupBy(io => io.Size)
        //          .Select(ioGroup => ioGroup.FirstOrDefault());
        //        dynamic all = new ExpandoObject();
        //        all.m = item;
        //        all.s = itemoffer;
        //        all.z = itemsizes;
        //        all.Reviews = db.Reviews.Where(r => r.ItemId == review.ItemId).Include(r => r.AspNetUser);
        //        return View("Shopdetails", all);
        //    }
        //}



        //[HttpPost]
        //public ActionResult AddToCart(int itemId, string colorId, string size)
        //{
        //    // Check if the user is authenticated
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        // Redirect the user to the login page
        //        return RedirectToAction("Login", "Account");
        //    }

        //    // Get the user's UserId
        //    string userId = User.Identity.Name;

        //    // Find the item offered based on the item ID, color ID, and size
        //    var itemOffered =db.Itemeoffereds
        //        .Where(io => io.ItemId == itemId && io.ColorId == colorId && io.Size == size)
        //        .FirstOrDefault();

        //    // If the item offered doesn't exist or has no quantity left, return an error
        //    if (itemOffered == null || itemOffered.QuantityLeft == 0)
        //    {
        //        return RedirectToAction("Item", "Home");
        //    }

        //    // Find the cart for the current user and the item being added
        //    var cart = db.Carts
        //        .Where(c => c.UserId == userId && c.ItemId == itemOffered.ColorId)
        //        .FirstOrDefault();

        //    // If the cart doesn't exist, create a new one
        //    if (cart == null)
        //    {
        //        cart = new Cart
        //        {
        //            ItemId = itemOffered.ColorId,
        //            UserId = userId,
        //            Quantity = 0
        //        };
        //       db.Carts.Add(cart);
        //    }

        //    // Update the cart's quantity and save changes to the database
        //    cart.Quantity += 1;
        //    db.SaveChanges();

        //    // Redirect to the cart page
        //    return RedirectToAction("Cart");
        //}


        //[HttpPost]
        //public ActionResult AddToCart(int itemId, string color, string size, int quantity)
        //{
        //    Check if the user is authenticated
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        Redirect the user to the login page
        //        return RedirectToAction("Login", "Account");
        //    }

        //    Get the user's UserId
        //    string userId = User.Identity.Name;

        //    Check if the item is already in the cart
        //    var cartItem = db.Carts.Include(c => c.ItemOffered)
        //                            .FirstOrDefault(c => c.UserId == userId && c.ItemId == itemId && c.ItemOffered.Color == color && c.ItemOffered.Size == size);
        //    if (cartItem != null)
        //    {
        //        Update the quantity of the existing cart item
        //        cartItem.Quantity += quantity;
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        Create a new cart item
        //       var newCartItem = new Cart
        //       {
        //           UserId = userId,
        //           ItemId = itemId,
        //           Color = color,
        //           Size = size,
        //           Quantity = quantity
        //       };
        //        db.Carts.Add(newCartItem);
        //        db.SaveChanges();
        //    }

        //    Redirect the user to the cart page
        //    return RedirectToAction("Cart");
        //}


        //public ActionResult AddToCart(int id, int quantity, string color, string size)
        //{

        //    {
        //        var item = db.Items.Find(id);
        //        var itemOffered = db.Itemeoffereds
        //            .FirstOrDefault(io => io.ItemId == id && io.ColorName == color && io.Size == size);

        //        if (itemOffered == null || quantity > itemOffered.QuantityLeft)
        //        {
        //            ViewBag.Message = "Item is not available in the selected quantity and color/size.";
        //            return RedirectToAction("ShopDetails", new { id = item.Item_Id });
        //        }

        //        // Check if the cart already contains the selected item
        //        var existingCartItem = db.OrderDetails
        //            .FirstOrDefault(ci => ci.ItemId == item.Item_Id && ci.ColorName == itemOffered.ColorName && ci.Size == itemOffered.Size);

        //        if (existingCartItem != null)
        //        {
        //            existingCartItem.Quantity += quantity;
        //            db.Entry(existingCartItem).State = EntityState.Modified;
        //        }
        //        else
        //        {
        //            var OrderDetails = new OrderDetails
        //            {
        //                ItemId = item.Item_Id,
        //                UserId = User.Identity.GetUserId(),
        //                ColorName = itemOffered.ColorName,
        //                Size = itemOffered.Size,
        //                Quantity = quantity,
        //                Price = item.ItemPrice
        //            };
        //            db.CartItems.Add(cartItem);
        //        }

        //        itemOffered.QuantityLeft -= quantity;
        //        db.Entry(itemOffered).State = EntityState.Modified;

        //        db.SaveChanges();

        //        ViewBag.Message = "Item added to cart successfully.";
        //        return RedirectToAction("ShopDetails", new { id = item.Item_Id });
        //    }
        //}




        public ActionResult Checkout()
        {
            return View();
        }


      



        [HttpPost]
        public ActionResult RemoveFromCart(int itemId)
        {
            // Remove the item from the cart and save changes to the database
            // ...

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult lubna()
        {
            return View();
        }
    }
}