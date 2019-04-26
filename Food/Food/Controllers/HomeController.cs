using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food.Models;

namespace Food.Controllers
{
    public class HomeController : Controller
    {
        private DB26Entities3 db = new DB26Entities3();
        int CurrentOrderTrackID;
        // GET: Home
        public ActionResult Index()
        {
            List<FoodItem> list = db.FoodItems.OrderBy(o => o.Name).ToList();
            ViewBag.listProduct = list;
            return View(db.FoodItems.ToList());
        }
        public ActionResult Index1()
        {
            var items = (from d in db.FoodItems
                         select d).ToList();
            return View(items);
        }
        public ActionResult check()
        {
            return View();
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult TrackOrder()
        {
            ViewBag.Filterthroughid = "";
            return View();

        }
        [HttpPost]
        public ActionResult TrackOrder(string id)
        {
            try
            {
                if (id == "")
                {
                    return HttpNotFound();

                }
                else
                {
                    CurrentOrderTrackID = Convert.ToInt32(id);
                    Order order = new Order();
                    List<OrderFood> productlist = new List<OrderFood>();
                    foreach (OrderFood Order in db.OrderFoods)
                    {
                        if (Order.OrderID == Convert.ToInt32(id))
                        {
                            OrderFood Oproduct = new OrderFood();
                            //Oproduct.Order.Status = Order.Order.Status;
                            Oproduct.OrderID = Order.OrderID;
                            Oproduct.ProductID = Order.ProductID;
                            Oproduct.Price = Order.Price;
                            Oproduct.Category = Order.Category;
                            Oproduct.ProductName = Order.ProductName;
                            Oproduct.Quantity = Order.Quantity;
                            productlist.Add(Oproduct);


                        }

                    }
                    int r = Convert.ToInt32(id);
                    order = db.Orders.Find(Convert.ToInt32(id));
                    if (order == null)
                    {
                        ViewBag.NULL = true;
                    }
                    else
                    {
                        ViewBag.orderStatus = order.Status;
                    }
                    ViewBag.Filterthroughid = productlist;
                    return View(productlist.ToList());
                }

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
        [HttpPost]
        public ActionResult CancelOrder(string id)
        {
           
           
                    foreach (OrderFood OP in db.OrderFoods)
            {
                    if (OP.OrderID == Convert.ToInt32(id))
                    {
                        try
                    {
                        db.OrderFoods.Remove(OP);
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
            }
        
            db.SaveChanges();
            db.Orders.Remove(db.Orders.Find(id));
            db.SaveChanges();
            ViewBag.listProduct = db.FoodItems.ToList();
            return View("CheckOut");
        }
        public ActionResult CheckOut()
        {
            try
            {
                ViewBag.listProduct = db.FoodItems.ToList();
                Order order = new Order();
               
                order.Order_Date = DateTime.Now;
                order.Status = "Not-Deliver";
                order.Bill = 1000;
                List<Cart> cart = (List<Cart>)Session["cart"];
                order.Items = cart.ToArray().Length;
                db.Orders.Add(order);
                db.SaveChanges();
                int r = order.OrderID;
                
                for (int j = 0; j < cart.Count; j++)
                {

                    FoodItem F = db.FoodItems.Find(cart[j].NewFood1.FoodID);
                    OrderFood orderProduct = new OrderFood();
                    orderProduct.OrderID = r;
                    orderProduct.ProductName = F.Name;
                    orderProduct.Quantity = cart[j].Quantity;
                    orderProduct.Price = F.Price;
                    orderProduct.Category = F.Category;
                    db.OrderFoods.Add(orderProduct);
                    db.SaveChanges();
                }
                Session["cart"] = null;
                return View();
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
        private int isPresent(int id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            for (int j = 0; j < cart.Count; j++)
            {
                if (cart[j].NewFood1.FoodID == id)
                {
                    return j;
                }

            }
            return -1;

        }
        public ActionResult Cart(int id, Cart cartB)


        {
            try
            {
                if (id == null && cartB == null)
                {
                    return View();
                }
                else
                {
                    if (Session["cart"] == null)
                    {
                        List<Cart> cart = new List<Cart>();
                        cart.Add(new Cart(db.FoodItems.Find(id), 1));
                        Session["cart"] = cart;
                    }
                    else
                    {
                        List<Cart> cart = (List<Cart>)Session["cart"];
                        int index = isPresent(id);
                        if (index == -1)
                        {
                            cart.Add(new Cart(db.FoodItems.Find(id), 1));

                        }
                        else
                        {
                            cart[index].Quantity++;
                        }

                        Session["cart"] = cart;
                    }

                    return View("Cart");
                }
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
    }
}