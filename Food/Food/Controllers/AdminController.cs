using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food.Models;
using System.Net;
using System.Data.Entity;
namespace Food.Controllers
{
    public class AdminController : Controller
    {
        private DB26Entities4 db = new DB26Entities4();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ManageFoodItems(Addfooditem obj,HttpPostedFileBase image1)
        {
            try
            {
                var db = new DB26Entities4();


                FoodItem food = new FoodItem();
                food.Name = obj.Name;
                food.Price = obj.Price;
                food.Category = obj.Category;
                food.Picture = new byte[image1.ContentLength];
                image1.InputStream.Read(food.Picture, 0, image1.ContentLength);


                db.FoodItems.Add(food);
                db.SaveChanges();
                return RedirectToAction("ManageFoodItems");
            }

            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
        public ActionResult ManageFoodItems()
        {
           
            List<FoodItem> list = db.FoodItems.OrderBy(o => o.Name).ToList();
            ViewBag.listProduct = list;

            return View();
        }
        public ActionResult DeleteFoodItems(int id)
        {
            FoodItem f = db.FoodItems.Find(id);
            db.FoodItems.Remove(f);
            db.SaveChanges();
            ViewBag.listProduct = db.FoodItems.ToList();

            return View();

        }
        [HttpPost]
        public ActionResult EditFoodItems(int id)

        {
            
           

            return View(db.FoodItems.Find(id));

        }
        [HttpPost]
        public ActionResult EditFoodItems(Addfooditem obj, int id)
        {
            FoodItem f = db.FoodItems.Find(id);
            if (ModelState.IsValid)
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageFoodItems");
            }
            else
            {
                return RedirectToAction("ManageFoodItems");
            }
        }

        public ActionResult OrdersReport()
        {
            ViewBag.listProduct = db.Orders.ToList();

            return View();
        }

        public ActionResult DetailsOrder(int Id)
        {
            List<OrderFood> list = new List<OrderFood>();
            foreach (OrderFood p in db.OrderFoods)
            {
                if (p.OrderID == Id)
                {
                    OrderFood p1 = new OrderFood();
                    p1.OrderID = p.OrderID;
                    p1.ProductID = p.ProductID;
                    p1.ProductName = p.ProductName;
                    p1.Price = p.Price;
                    p1.Quantity = p.Quantity;

                    list.Add(p1);

                }
            }
            ViewBag.ListProduct = list;

            return View();


        }

    }
}