using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilitiesServiceProvider.Models;
using System.Data;
using System.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;

namespace UtilitiesServiceProvider.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User obj)
        {
            var db = new UtilitiesEntities();

            var admin = (from i in db.Users
                         where (i.Email == obj.Email && i.Password == obj.Password)
                         select i).FirstOrDefault();

            if (admin != null)
            {
                Session["AdminId"] = admin.Id;
                return RedirectToAction("Information");


            }
            else
            {
                TempData["msg"] = "Wrong Email or Password";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Update()
        {
            // var adminId = Int32.Parse(Session["AdminId"].ToString());
            var adminId = Session["AdminId"].ToString();
            var id =int.Parse(adminId);
            var db = new UtilitiesEntities();
            var admin = (from i in db.Users where i.Id == id select i).FirstOrDefault();

            return View(admin);
        }
        [HttpPost]

        public ActionResult Update(User obj)
        {
            var db=new UtilitiesEntities();
            var adminId = Session["AdminId"].ToString();
            var id = int.Parse(adminId);
            var admin = (from i in db.Users where i.Id == id select i).FirstOrDefault();

            admin.Name = obj.Name;
            admin.Email = obj.Email;    

        admin.Password = obj.Password;  
            admin.Address=obj.Address;
            admin.Phone = obj.Phone;
            db.SaveChanges();

            return RedirectToAction("Information");

        }


        public ActionResult Information()
        {
            var adminId = Session["AdminId"].ToString();
            var id = int.Parse(adminId);
            var db = new UtilitiesEntities();
            var admin = (from i in db.Users where i.Id == id select i).FirstOrDefault();

            return View(admin);


        }


        public ActionResult InformationDownload(int id)
        {

            var db = new UtilitiesEntities();
            var adminName = (from p in db.Users     where p.Id == id select p.Name).FirstOrDefault();
            var adminAddress = (from p in db.Users where p.Id == id select p.Address).FirstOrDefault();
            var adminEmail = (from p in db.Users where p.Id == id select p.Email).FirstOrDefault();
            var adminPhonenumber = (from p in db.Users where p.Id == id select p.Phone).FirstOrDefault();
            var adminPassword = (from p in db.Users where p.Id == id select p.Password).FirstOrDefault();
            var dateime = DateTime.Now.ToString("dddd, dd MMMM yyyy").ToString();


            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            System.Data.DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("particular:");
            dataTable.Columns.Add("Details");
            // dataTable.Columns.Add("Name");
            //Add rows to the DataTable.
            dataTable.Rows.Add(new object[] { "Print Time", dateime });
            dataTable.Rows.Add(new object[] { " MentorId", id });
            dataTable.Rows.Add(new object[] { "MentorName", adminName });
            dataTable.Rows.Add(new object[] { "MentorAddress", adminAddress });
            dataTable.Rows.Add(new object[] { "MentorEmail", adminEmail });
            dataTable.Rows.Add(new object[] { "MentorPhonenumber", adminPhonenumber });
             dataTable.Rows.Add(new object[] { "MentorPassword", adminPassword });



            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));
            // Open the document in browser after saving it

            doc.Save(adminName + dateime + ".pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            //close the document
            doc.Close(true);
            return View();


        }






        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn");  
        }


















    }
}