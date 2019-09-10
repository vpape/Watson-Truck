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
    public class Other_InsuranceController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        private static List<Family_Info> family = new List<Family_Info>();
        private static List<Other_Insurance> otherIns = new List<Other_Insurance>();

        public Other_InsuranceController()
        {

        }

        public ActionResult AddOtherInsurance(int Employee_id, int OtherInsurance_id, string RelationshipToInsured)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.OtherInsurance_id = OtherInsurance_id;
            ViewBag.RelationshipToInsured = RelationshipToInsured;

            return View();
        }

        //Post:
        public JsonResult OtherInsuranceNew(int Employee_id, int OtherInsurance_id, string CoveredByOtherInsurance,
            string InsuranceCarrier, string PolicyNumber, string InsPhoneNumber)
        {
            Other_Insurance other = new Other_Insurance();

            other.CoveredByOtherInsurance = CoveredByOtherInsurance;
            other.InsuranceCarrier = InsuranceCarrier;
            other.PolicyNumber = PolicyNumber;
            other.PhoneNumber = InsPhoneNumber;

            int result = other.Employee_id;

            db.Other_Insurance.Add(other);
            db.SaveChanges();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditOtherInsurance()
        {
            return View();
        }

        //Post:
        public ActionResult OtherInsuranceEditUpdate()
        {
            return View();
        }

    public ActionResult OtherInsuranceDetail()
        {
            return View();
        }

        public ActionResult GetOtherInsuranceDetail()
        {
            return View();
        }

        //DeleteOtherInsurance Method
        public ActionResult DeleteOtherInsurance(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Other_Insurance o = db.Other_Insurance.Find(Employee_id);
            if (o == null)
            {
                return HttpNotFound();
            }

            return View(o);
        }

        //DeleteOtherInsurance Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteOtherInsurance")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Employee_id)
        {
            Other_Insurance o = db.Other_Insurance.Find(Employee_id);
            db.Other_Insurance.Remove(o);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(Employee_id);

            return RedirectToAction("EmpOverview", new { o.Employee_id });
        }
    }
}
