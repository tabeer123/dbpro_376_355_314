using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food.Models;
using System.Data.SqlClient;
namespace Food.Controllers
{
    public class RegisterController : Controller
    {

        private DB26Entities3 db = new DB26Entities3();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount acc)
        {
            try
            {
                var db = new DB26Entities3();


                Person p = new Person();
                p.First_Name = acc.First_Name;
                p.Last_Name = acc.Last_Name;
                p.Cell_No = acc.Cell_No;
                p.Email = acc.Email;
                p.Password = acc.Password;
                p.Discriminator = acc.Discriminator;
                p.Address = acc.Address;


                db.People.Add(p);
                db.SaveChanges();

                ViewBag.Meassgae = acc.First_Name + " " + acc.Last_Name + "successfully registered";

                return RedirectToAction("Index");
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

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Autherize(Food.Models.UserAccount userModel)
        {
            using (DB26Entities3 db = new DB26Entities3())
            {
                var userDetails = db.People.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {

                    Session["PersonID"] = userDetails.PersonID;
                    Session["Email"] = userDetails.Email;
                    if (userDetails.Discriminator == "User")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userDetails.Discriminator == "Admin")
                    {
                        return RedirectToAction("ManageFoodItems", "Admin");
                    }
                    else
                    {
                        return View("Index", userModel);
                    }
                }
            }
        }
    }
}