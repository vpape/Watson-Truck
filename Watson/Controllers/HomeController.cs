using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelectPdf;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Dynamic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Watson.Models;
using Watson.ViewModels;



namespace Watson.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();  
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactUs()
        {
            ViewBag.Message = "Contact Watson Truck & Supply";

            return View();
        }

        public ActionResult OurTeam()
        {
            ViewBag.Message = "Meet our team!";

            return View();
        }

        public ActionResult AdminLogin()
        {
            ViewBag.Message = "Once Admin is logged in, it will redirect them to EmpOverview page for all employees.";

            return View();
        }

        public ActionResult EmployeeLogin()
        {
            ViewBag.Message = "Once employee is logged in, it will redirect them to their profile overview page.";

            return View();
        }
    }
}
