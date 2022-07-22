using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilitiesServiceProvider.Models;

namespace UtilitiesServiceProvider.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var db = new UtilitiesEntities();
            var services=db.Services.ToList();
            return View(services);
        }
    }
}