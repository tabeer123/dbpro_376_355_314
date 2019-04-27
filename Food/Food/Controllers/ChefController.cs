using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Food.Controllers
{
    public class ChefController : Controller
    {
        // GET: Chef
        private DB26Entities4 db = new DB26Entities4();
        public ActionResult Index()
        {
            List<Order> list = db.Orders.OrderBy(o => o.Order_Date).ToList();
            ViewBag.listProduct = list;

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
        public ActionResult notdeliver()
        {
            List<Order> po = new List<Order>();
            foreach (Order p in db.Orders)
            {
                if (p.Status == "Not-Deliver")
                {
                    Order p1 = new Order();
                    p1.OrderID = p.OrderID;
                    p1.Order_Date = p.Order_Date;
                    p1.Items = p.Items;
                    p1.Status = p.Status;
                    po.Add(p1);
                }
            }
            ViewBag.listProduct = po;
            return View();

        }
        public ActionResult changestatusList(int Id)
        {
            Order f = db.Orders.Find(Id);
            f.Status = "Deliver";
            db.SaveChanges();
            List<Order> po = new List<Order>();
            foreach (Order p in db.Orders)
            {
                if (p.Status == "Deliver")
                {
                    Order p1 = new Order();
                    p1.OrderID = p.OrderID;
                    p1.Order_Date = p.Order_Date;
                    p1.Items = p.Items;
                    p1.Status = p.Status;
                    po.Add(p1);
                }
            }
            ViewBag.listProduct = po;
            return View();
        }

        public ActionResult DeliveredOrders()
        {
            List<Order> po = new List<Order>();
            foreach (Order p in db.Orders)
            {
                if (p.Status == "Deliver")
                {
                    Order p1 = new Order();
                    p1.OrderID = p.OrderID;
                    p1.Order_Date = p.Order_Date;
                    p1.Items = p.Items;
                    p1.Status = p.Status;
                    po.Add(p1);
                }
            }
            ViewBag.listProduct = po;
            return View();

        }
    }
}