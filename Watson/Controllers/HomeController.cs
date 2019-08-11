using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        //private IEmployeeRepository _employeeRepository;

        //public HomeController(IEmployeeRepository employeeRepository)
        //{
        //    _employeeRepository = employeeRepository;
        //}

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
            
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

        public ActionResult CareerCenter()
        {
            ViewBag.Message = "Redirect to Career Cetner to apply for positions.";

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
