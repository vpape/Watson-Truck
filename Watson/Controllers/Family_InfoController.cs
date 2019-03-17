using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Watson.Models;


namespace Watson.Controllers
{
    public class Family_InfoController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        private static List<Family_Info> familyMember = new List<Family_Info>();

        public Family_InfoController()
        {

        }

        public ActionResult FamilyMemberOverview(int fm_id)
        {
            Family_Info family = db.Family_Info.Find(fm_id);

            return View(family);
        }

        public JsonResult GetFamilyMember(int fm_id, int e_id)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.OtherInsurance_id,
                             f.FirstName,
                             f.MiddleName,
                             f.LastName,
                             f.SSN,
                             f.DateOfBirth,
                             f.MailingAddress,
                             f.PhysicalAddress,
                             f.City,
                             f.State,
                             f.ZipCode,
                             f.EmailAddress,
                             f.PhoneNumber,
                             f.CellPhone,
                             f.County,
                             f.Gender,
                             f.Employer,
                             f.RelationshipToInsured,
                             f.EmployerMailingAddress,
                             f.EmployerCity,
                             f.EmployerState,
                             f.EmployerZipCode,
                             f.EmployerPhoneNumber,
                             f.Medical,
                             f.Dental,
                             f.Vision,
                             f.Indemnity,
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FamilyMemberEnrollment()
        {
            return View();
        }

        public ActionResult SpouseEnrollment()
        {
            return View();
        }

        public JsonResult GetSpouseEnrollment(int fm_id, int e_id, string MaritalStatus)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.RelationshipToInsured,
                             f.SSN,
                             f.FirstName,
                             f.LastName,
                             f.DateOfBirth,
                             f.Gender,
                         };

            ViewBag.Employee_id = e_id;
            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;

            Employee employee = db.Employees.Find(e_id);

            if (employee.MaritalStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (employee.MaritalStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }


        //[System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult SpouseEnrollmentUpdate(int fm_id, int e_id)
        //{
        //    Family_Info f = db.Family_Info
        //        .Where(i => i.FamilyMember_id == fm_id)
        //        .Where(i => i.Employee_id == e_id)
        //        .SingleOrDefault();

        //    db.Family_Info.Add(f);
        //    db.SaveChanges();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        //}

        //----------------------------------------------------------------------------------------

        public ActionResult SpouseContact()
        {
            return View();
        }

        public JsonResult GetSpouseContact(int fm_id, int e_id)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.MailingAddress,
                             f.PhysicalAddress,
                             f.PObox,
                             f.City,
                             f.State,
                             f.ZipCode,
                             f.County,
                             f.EmailAddress,
                             f.PhoneNumber,
                             f.CellPhone,
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SpouseContactUpdate(int fm_id, int e_id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == fm_id)
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            db.Family_Info.Add(f);
            db.SaveChanges();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        //GET: api/Family_Info/5
        //public Family_Info SpouseContact(int id)
        //{
        //    return familyMember.Where(i => i.FamilyMember_id == id).FirstOrDefault();
        //}

        //POST: api/Family_Info
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public void SpouseContact([Bind(Include = "")]Family_Info spouseContact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Family_Infoes.Add(spouseContact);
        //        db.SaveChanges();
        //    }
        //}
        //----------------------------------------------------------------------------------------

        public ActionResult SpouseEmployment()
        {
            return View();
        }

        public JsonResult GetSpouseEmployment(int fm_id, int e_id)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.Employer,
                             f.MailingAddress,
                             f.PhysicalAddress,
                             f.PObox,
                             f.EmployerCity,
                             f.EmployerState,
                             f.EmployerZipCode,
                             f.EmployerPhoneNumber,
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SpouseEmploymentUpdate(int fm_id, int e_id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == fm_id)
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EditSpouse(int? fm_id, int e_id)
        {
            if (fm_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info family = db.Family_Info.Find(fm_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee_id = e_id;

            return View(family);
        }

        public JsonResult GetEditSpouse(int fm_id, int e_id, string MaritalStatus)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.RelationshipToInsured,
                             f.SSN,
                             f.FirstName,
                             f.LastName,
                             f.DateOfBirth,
                             f.Gender,
                             f.MailingAddress,
                             f.PhysicalAddress,
                             f.PObox,
                             f.City,
                             f.State,
                             f.ZipCode,
                             f.County,
                             f.EmailAddress,
                             f.PhoneNumber,
                             f.CellPhone,
                             f.Employer,
                             f.EmployerMailingAddress,
                             f.EmployerCity,
                             f.EmployerState,
                             f.EmployerZipCode,
                             f.EmployerPhoneNumber,
                         };

            ViewBag.Employee_id = e_id;
            ViewBag.MaritalStatus = MaritalStatus;

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //[System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult EditSpouseUpdate(int fm_id, int e_id)
        //{
        //    Family_Info f = db.Family_Info
        //        .Where(i => i.FamilyMember_id == fm_id)
        //        .Where(i => i.Employee_id == e_id)
        //        .SingleOrDefault();

        //    db.Family_Info.Add(f);
        //    db.SaveChanges();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        //}

        //----------------------------------------------------------------------------------------

        public ActionResult SpouseDetail(int? fm_id)
        {
            if (fm_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info family = db.Family_Info.Find(fm_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            return View(family);
        }

        public JsonResult GetSpouseDetail(int fm_id, int e_id)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.RelationshipToInsured,
                             f.SSN,
                             f.FirstName,
                             f.LastName,
                             f.DateOfBirth,
                             f.Gender,
                             f.MailingAddress,
                             f.PhysicalAddress,
                             f.PObox,
                             f.City,
                             f.State,
                             f.ZipCode,
                             f.County,
                             f.EmailAddress,
                             f.PhoneNumber,
                             f.CellPhone,
                             f.Employer,
                             f.EmployerMailingAddress,
                             f.EmployerCity,
                             f.EmployerState,
                             f.EmployerZipCode,
                             f.EmployerPhoneNumber,
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //[System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult SpouseDetailUpdate(int fm_id, int e_id)
        //{
        //    Family_Info f = db.Family_Info
        //        .Where(i => i.FamilyMember_id == fm_id)
        //        .Where(i => i.Employee_id == e_id)
        //        .SingleOrDefault();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);

        //}

        //----------------------------------------------------------------------------------------

        public ActionResult DeleteSp(int? fm_id)
        {
            if (fm_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info family = db.Family_Info.Find(fm_id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteSp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int fm_id, int e_id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == fm_id)
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            db.DeleteEmployeeAndDependents(fm_id);
            db.DeleteEmployeeAndDependents(e_id);
            db.Family_Info.Remove(f);
            db.SaveChanges();

            return RedirectToAction("FamilyMemberOverview", new { f.Employee_id });
        }

        //[System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Family_Info family = db.Family_Info.Find(id);
        //    db.Family_Info.Remove(family);
        //    db.SaveChanges();
        //    return RedirectToAction("FamilyMemberOverview");
        //}

        //----------------------------------------------------------------------------------------

        public ActionResult DependentEnrollment()
        {
            return View();
        }

        public JsonResult GetDependentEnrollment(int fm_id, int e_id, int oi_id)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         where f.OtherInsurance_id == oi_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.OtherInsurance_id,
                             f.RelationshipToInsured,
                             f.SSN,
                             f.FirstName,
                             f.LastName,
                             f.DateOfBirth,
                             f.Gender,
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DependentEnrollmentUpdate(int fm_id, int e_id, int oi_id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == fm_id)
                .Where(i => i.Employee_id == e_id)
                .Where(i => i.OtherInsurance_id == oi_id)
                .SingleOrDefault();

            db.Family_Info.Add(f);
            db.SaveChanges();

            return Json(new { output = "success" }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EditDep()
        {
            return View();
        }

        public JsonResult DepEditUpdate(int fm_id, int e_id, int oi_id)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         where f.OtherInsurance_id == oi_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.OtherInsurance_id,
                             f.RelationshipToInsured,
                             f.FirstName,
                             f.LastName,
                             f.DateOfBirth,
                             f.Gender,
                             //f.CoveredByOtherInsurance
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult DependentEditUpdate(int fm_id, int e_id, int oi_id)
        //{
        //    Family_Info f = db.Family_Info
        //        .Where(i => i.FamilyMember_id == fm_id)
        //        .Where(i => i.Employee_id == e_id)
        //        .Where(i => i.OtherInsurance_id == oi_id)
        //        .SingleOrDefault();

        //    db.Family_Info.Add(f);
        //    db.SaveChanges();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult DepDetail(int? fm_id)
        {
            if (fm_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info family = db.Family_Info.Find(fm_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            return View(family);
        }

        public JsonResult GetDepDetail(int fm_id, int e_id, int oi_id)
        {
            var output = from f in db.Family_Info
                         where f.FamilyMember_id == fm_id
                         where f.Employee_id == e_id
                         where f.OtherInsurance_id == oi_id
                         select new
                         {
                             f.FamilyMember_id,
                             f.Employee_id,
                             f.OtherInsurance_id,
                             f.FirstName,
                             f.LastName,
                             f.DateOfBirth,
                             f.Gender,
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult DependentDetailUpdate(int fm_id, int e_id, int oi_id)
        //{
        //    Family_Info f = db.Family_Info
        //        .Where(i => i.FamilyMember_id == fm_id)
        //        .Where(i => i.Employee_id == e_id)
        //        .Where(i => i.OtherInsurance_id == oi_id)
        //        .SingleOrDefault();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            
        //}

        public ActionResult DeleteDep(int? fm_id)
        {
            if (fm_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info family = db.Family_Info.Find(fm_id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteDep")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int fm_id, int e_id)
        {
            Family_Info family = db.Family_Info.Find(fm_id);
            //Employee employee = db.Employees.Find(e_id);
            db.Family_Info.Remove(family);
            //db.Employees.Remove(employee);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(fm_id);
            //db.DeleteEmployeeAndDependents(e_id);

            return RedirectToAction("FamilyMemberOverview", new { family.Employee_id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
