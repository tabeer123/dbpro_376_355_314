using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food.Models;
using System.Net;
using System.Data.Entity;
using Food.Models;
using Food.Reports;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Food.Controllers
{
    public class AdminController : Controller
    {
        private DB26Entities5 db = new DB26Entities5();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.ListFood = db.FoodItems.ToList();
            return View();
        }

        public ActionResult Export()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/AllFoodItems.rpt")));
            rd.SetDataSource(db.FoodItems.Select(t => new
            {
                FoodID = t.FoodID,
                Name = t.Name,
                Price = t.Price,
                Quantity = t.Quantity,
                Category = t.Category,




            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListFood.pdf");
            

            
    }
        public ActionResult AllCustomer()
        {
            ViewBag.ListCustomer = db.People.ToList();
            return View();
        }
        public ActionResult Export1()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/AllCoustomer.rpt")));
            rd.SetDataSource(db.People.Select(t => new
            {
                PersonID = t.PersonID,
                First_Name = t.First_Name,
                Last_Name = t.Last_Name,
                Address = t.Address,
                Cell_No = t.Cell_No,
                Email = t.Email,





            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerReport.pdf");



        }
        public ActionResult AllOrder()
        {
            ViewBag.ListOrder = db.Orders.ToList();
            return View();
        }
        public ActionResult Export2()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/AllOrdersReport.rpt")));
            rd.SetDataSource(db.Orders.Select(t => new
            {
                OrderID = t.OrderID,
                Bill = t.Bill,
                Items= t.Items,
                Status = t.Status,
             





            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "OrderReport.pdf");



        }
        //Add Food Items
        [HttpPost]
        public ActionResult ManageFoodItems(Addfooditem obj,HttpPostedFileBase image1)
        {
            try
            {
                var db = new DB26Entities5();


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
        // Delete Food Items
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