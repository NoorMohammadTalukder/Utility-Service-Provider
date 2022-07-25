using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilitiesServiceProvider.Models;

namespace UtilitiesServiceProvider.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin (User obj)
        {
            var db = new UtilitiesEntities() ;
            var user = (from i in db.Users
                         where (i.Email == obj.Email && i.Password == obj.Password)
                         select i).FirstOrDefault();
            if (user != null)
            {
                Session["userId"] = user.Id;
                if (user.Type == "Customer")
                {
                    return RedirectToAction("Index","Home");
                }
                else if (user.Type == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.Type == "Service Provider")
                {
                    return RedirectToAction("Index", "ServiceProvider");
                }



            }
            
                TempData["msg"] = "Wrong Email or Password";
                return View();
            
        }

        [HttpGet]
        public ActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(User obj)
        {
            var db = new UtilitiesEntities();
           // if (ModelState.IsValid)
          //  {
                var user = new User()
                {
                    Name = obj.Name,
                    Address = obj.Address,
                    Email = obj.Email,
                    Phone = obj.Phone,
                    Password = obj.Password,
                   Type= "Customer"


                };
                db.Users.Add(user);
                db.SaveChanges();
                //return RedirectToAction("Index", "Home"); //---action name, controller name
          //  }
            return View();
        }
    }
}