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

        private static List<Family_Info> family = new List<Family_Info>();

        public Family_InfoController()
        {

        }

        public ActionResult FamilyOverview(Family_Info family)
        {
            Family_Info f = db.Family_Info.Find(family);

            f = family;

            return View(family);
        }

        public JsonResult GetFamilyMember(int FamilyMember_id, string FirstName, string LastName,
            string RelationshipToInsured, DateTime DateOfBirth, string MailingAddress, string PObox,
            string City, string State, string County, string ZipCode, string EmailAddress, string PhoneNumber, 
            string CellPhone, string Gender, string Employer, string EmployerMailingAddress, string EmployerCity, 
            string EmployerState, string EmployerZipCode, string EmployerPhoneNumber, bool Medical,
            bool Dental, bool Vision, bool Indemnity)
        {
            var f = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            f.FirstName = FirstName;
            f.LastName = LastName;
            f.RelationshipToInsured = RelationshipToInsured;
            f.DateOfBirth = DateOfBirth;
            f.MailingAddress = MailingAddress;
            f.PObox = PObox;
            f.City = City;
            f.State = State;
            f.ZipCode = ZipCode;
            f.EmailAddress = EmailAddress;
            f.PhoneNumber = PhoneNumber;
            f.CellPhone = CellPhone;
            f.Gender = Gender;
            //f.SSN = 
            f.Employer = Employer;
            f.EmployerMailingAddress = EmployerMailingAddress;
            f.EmployerCity = EmployerCity;
            f.EmployerState = EmployerState;
            f.EmployerZipCode = EmployerZipCode;
            f.EmployerPhoneNumber = EmployerPhoneNumber;
            f.Medical = Medical;
            f.Dental = Dental;
            f.Vision = Vision;
            f.Indemnity = Indemnity;


            return Json(new { data = f }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult FamilyEnrollment()
        //{
        //    return View();
        //}

        //public ActionResult SpEnrollment(string Employee_id, string MaritalStatus)
        //{
        //    ViewBag.Employee_id = Employee_id;
        //    ViewBag.MartialStatus = MaritalStatus;

        //    return View();
        //}

        //public JsonResult SpEnrollmentNew(int Employee_id, string MaritalStatus, string RelationshipToInsured,
        //    string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, string Gender)
        //{
        //    Family_Info sp = new Family_Info();

        //    sp.RelationshipToInsured = RelationshipToInsured;
        //    sp.SSN = EmpNumber;
        //    sp.FirstName = FirstName;
        //    sp.LastName = LastName;
        //    sp.DateOfBirth = DateOfBirth;
        //    sp.Gender = Gender;
        //    sp.Employee_id = Employee_id;

        //    int result = Employee_id;

        //    if (ModelState.IsValid)
        //    {
        //        db.Family_Info.Add(sp);
        //        db.SaveChanges();
        //    }

        //    return Json(new { data = sp, Employee_id }, JsonRequestBehavior.AllowGet);

        //}

        //----------------------------------------------------------------------------------------

        public ActionResult SpContact()
        {
            return View();
        }

        public JsonResult SpEnrollmentContact(int Employee_id, string MailingAddress, string PObox, string City, string State, 
            string ZipCode, string County, string PhysicalAddress, string PObox2, string City2, string State2,
            string ZipCode2, string County2, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            Family_Info sp = new Family_Info();
       
            sp.MailingAddress = MailingAddress;
            sp.PObox = PObox;
            sp.City = City;
            sp.State = State;
            sp.ZipCode = ZipCode;
            sp.County = County;
            sp.PhysicalAddress = PhysicalAddress;
            sp.PObox = PObox2;
            sp.City = City2;
            sp.State = State2;
            sp.ZipCode = ZipCode2;
            sp.County = County2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;

            int result = Employee_id;

            Employee emp = db.Employees.Find();

            emp.MailingAddress = MailingAddress;
            emp.PObox = PObox;
            emp.City = City;
            emp.State = State;
            emp.ZipCode = ZipCode;
            emp.County = County;
            emp.PhysicalAddress = PhysicalAddress;
            emp.PObox = PObox2;
            emp.City = City2;
            emp.State = State2;
            emp.ZipCode = ZipCode2;
            emp.County = County2;
            emp.EmailAddress = EmailAddress;
            emp.PhoneNumber = PhoneNumber;
            emp.CellPhone = CellPhone;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            return Json(new { data = sp, emp }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult SpEmployment()
        {
            return View();
        }

        //add maritalstatus and employee id to client side JSOn
        public JsonResult SpEmploymentUpdate(int Employee_id, string MaritalStatus, string Employer,
            string EmployerAddress, string EmployerCity, string EmployerState, string EmployerZipCode, 
            string EmployerPhoneNumber)
        {
            Family_Info sp = new Family_Info();

            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            //if (MaritalStatus == "MarriedwDep")
            //{
            //    return RedirectToAction("DepEnrollment", "Family_info", new {sp.Employee_id, sp.MaritalStatus });
            //}            
                       
            return Json(new { data = sp }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EditSp(int Employee_id, int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info family = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee_id = Employee_id;

            return View(family);
        }

        public JsonResult SpEditUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus, string RelationshipToInsured,
            string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress, string PObox2, 
            string City2, string State2, string ZipCode2, string County2, string EmailAddress, string PhoneNumber, 
            string CellPhone, string Employer, string EmployerAddress, string EmployerPObox, string EmployerCity, 
            string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            var sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.SSN = EmpNumber;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;
            sp.MailingAddress = MailingAddress;
            sp.PObox = PObox;
            sp.City = City;
            sp.State = State;
            sp.ZipCode = ZipCode;
            sp.County = County;
            sp.PhysicalAddress = PhysicalAddress;
            sp.PObox = PObox2;
            sp.City = City2;
            sp.State = State2;
            sp.ZipCode = ZipCode2;
            sp.County = County2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;
            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.PObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            int result = Employee_id;

            ViewBag.Employee_id = Employee_id;
            ViewBag.MaritalStatus = MaritalStatus;

            if (ModelState.IsValid)
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                RedirectToAction("FamilyOverview");
            }

            return Json(new { data = sp }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult SpDetail(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info family = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            return View(family);
        }

        public JsonResult GetSpDetail(int FamilyMember_id)
        {
            var sp = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            return Json(new { data = sp }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult DeleteSp(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info family = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteSp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int FamilyMember_id)
        {
            Family_Info family = db.Family_Info.Find(FamilyMember_id);

            db.DeleteEmployeeAndDependents(FamilyMember_id);
            
            db.Family_Info.Remove(family);
            db.SaveChanges();

            return RedirectToAction("FamilyMemberOverview");
        }

        //----------------------------------------------------------------------------------------

        //public ActionResult DepEnrollment()
        //{
        //    return View();
        //}

        //public JsonResult DepEnrollmentNew(int Employee_id, string MaritalStatus, string EmpNumber, string RelationshipToInsured, 
        //    string DepFirstName, string DepLastName, DateTime DateOfBirth, string Gender, string CoveredByOtherIns, 
        //    string InsCompany, string PolicyNumber, string InsPhoneNumber, string InsMailingAddress, string InsCity,
        //    string InsState, string InsZipCode)
        //{
        //    Family_Info dep = new Family_Info();

        //    dep.RelationshipToInsured = RelationshipToInsured;
        //    dep.FirstName = DepFirstName;
        //    dep.LastName = DepLastName;
        //    dep.DateOfBirth = DateOfBirth;
        //    dep.Gender = Gender;

        //    ViewBag.Employee_id = Employee_id;
        //    ViewBag.spouseExist = true;
        //    ViewBag.MartialStatus = MaritalStatus;

        //    Employee employee = db.Employees.Find(Employee_id);

        //    if (employee.MaritalStatus == "Single")
        //    {
        //        ViewBag.spouseExist = false;
        //        ViewBag.RelationshipToInsured = "Single";
        //    }
        //    else if (employee.MaritalStatus == "SinglewDep")
        //    {
        //        ViewBag.spouseExist = false;
        //        ViewBag.RelationshipToInsured = "Spouse";
        //    }
        //    else
        //    {
        //        ViewBag.RelationshipToInsured = "Dependent";
        //    }

        //    Employee emp = new Employee();

        //    emp.SSN = EmpNumber;

        //    Other_Insurance o = new Other_Insurance();

        //    o.CoveredByOtherInsurance = CoveredByOtherIns;
        //    o.InsuranceCompany = InsCompany;
        //    o.PolicyNumber = PolicyNumber;
        //    o.PhoneNumber = InsPhoneNumber;
        //    o.MailingAddress = InsMailingAddress;
        //    o.City = InsCity;
        //    o.State = InsState;
        //    o.ZipCode = InsZipCode;

        //    int result = Employee_id;

        //    db.Family_Info.Add(dep);
        //    db.Other_Insurance.Add(o);
        //    db.SaveChanges();

        //    return Json(new { data = dep, emp, o }, JsonRequestBehavior.AllowGet);
        //}

        //----------------------------------------------------------------------------------------
     
        public ActionResult EditDep()
        {
            return View();
        }

        public JsonResult DepEditUpdate(int Employee_id, int FamilyMember_id, int OtherInsurance_id, string EmpNumber,
            string RelationshipToInsured, string DepFirstName, string DepLastName, DateTime DateOfBirth, string Gender,
            string CoveredByOtherIns, string InsCompany, string PolicyNumber, string InsPhoneNumber, 
            string InsMailingAddress, string InsCity, string InsState, string InsZipCode)
        {
            var dep = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            dep.SSN = EmpNumber;
            dep.RelationshipToInsured = RelationshipToInsured;
            dep.FirstName = DepFirstName;
            dep.LastName = DepLastName;
            dep.DateOfBirth = DateOfBirth;
            dep.Gender = Gender;
            
            var o = db.Other_Insurance
                .Where(i => i.OtherInsurance_id == OtherInsurance_id)
                .Single();

            o.CoveredByOtherInsurance = CoveredByOtherIns;
            o.InsuranceCompany = InsCompany;
            o.PolicyNumber = PolicyNumber;
            o.PhoneNumber = InsPhoneNumber;
            o.MailingAddress = InsMailingAddress;
            o.City = InsCity;
            o.State = InsState;
            o.ZipCode = InsZipCode;

            int result = Employee_id;

            if (ModelState.IsValid)
            {
                db.Entry(dep).State = System.Data.Entity.EntityState.Modified;
                db.Entry(o).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                RedirectToAction("FamilyMemberOverview");
            }

            return Json(new { data = dep, o }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult DepDetail(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (f == null)
            {
                return HttpNotFound();
            }

            return View(f);
        }

        public JsonResult GetDepDetail(int FamilyMember_id, int OtherInsurance_id)
        {
            var dep = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            var o = db.Other_Insurance
            .Where(i => i.OtherInsurance_id == OtherInsurance_id)
            .Single();

            return Json(new { data = dep, o }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult DeleteDep(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (f == null)
            {
                return HttpNotFound();
            }
            return View(f);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteDep")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int FamilyMember_id)
        {
            Family_Info family = db.Family_Info.Find(FamilyMember_id);

            db.DeleteEmployeeAndDependents(FamilyMember_id);
            db.Family_Info.Remove(family);
            db.SaveChanges();

            return RedirectToAction("FamilyMemberOverview");
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
