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

        public ActionResult FamilyMemberOverview(Family_Info family)
        {
            Family_Info f = db.Family_Info.Find(family);

            f = family;

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

        public ActionResult SpEnrollment()
        {
            return View();
        }

        public JsonResult SpEnrollmentNew(int Employee_id, int FamilyMember_id, int OtherInsurance_id, string MaritalStatus,
            string RelationshipToInsured, string EmpNumber, string EmpFirstName, string EmpLastName, string FirstName,
            string LastName, DateTime DateOfBirth, string Gender)
        {
            var sp = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.OtherInsurance_id == OtherInsurance_id)
                .Single();

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.SSN = EmpNumber;
            sp.FirstName = EmpFirstName;
            sp.LastName = EmpLastName;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;

            int result = Employee_id;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            ViewBag.Employee_id = Employee_id;
            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;

            Employee employee = db.Employees.Find(Employee_id);

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

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //----------------------------------------------------------------------------------------

        public ActionResult SpContact()
        {
            return View();
        }

        public JsonResult SpEnrollmentContact(int Employee_id, int FamilyMember_id, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress, 
            string PObox2, string City2, string State2, string ZipCode2, string County2, string EmailAddress,
            string PhoneNumber, string CellPhone)
        {
            var sp = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

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

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult SpEmployment()
        {
            return View();
        }

        public JsonResult SpEmploymentUpdate(int Employee_id, int FamilyMember_id, string CurrentEmployer, 
            string EmployerAddress, string EmployerCity, string EmployerState, string EmployerZipCode, 
            string EmployerPhoneNumber)
        {
            var sp = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            sp.Employer = CurrentEmployer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            int result = Employee_id;
                       

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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
            string EmpNumber, string EmpFirstName, string EmpLastName, string FirstName, string LastName, DateTime DateOfBirth,
            string Gender, string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string PObox2, string City2, string State2, string ZipCode2, string County2,
            string EmailAddress, string PhoneNumber, string CellPhone, string CurrentEmployer, string EmployerAddress,
            string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            var sp = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.SSN = EmpNumber;
            sp.FirstName = EmpFirstName;
            sp.LastName = EmpLastName;
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
            sp.Employer = CurrentEmployer;
            sp.EmployerMailingAddress = EmployerAddress;
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

                RedirectToAction("FamilyMemberOverview");
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetSpDetail(int Employee_id, int FamilyMember_id, string RelationshipToInsured, string EmpNumber, string EmpFirstName,
            string EmpLastName, string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MailingAddress, string PObox,
            string City, string State, string ZipCode, string County, string PhysicalAddress, string PObox2, string City2, string State2,
            string ZipCode2, string County2, string EmailAddress, string PhoneNumber, string CellPhone, string CurrentEmployer,
            string EmployerMailingAddress, string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            var sp = db.Family_Info
                 .Where(i => i.Employee_id == Employee_id)
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.SSN = EmpNumber;
            sp.FirstName = EmpFirstName;
            sp.LastName = EmpLastName;
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
            sp.Employer = CurrentEmployer;
            sp.EmployerMailingAddress = EmployerMailingAddress;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            int result = Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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
                //.Where(i => i.FamilyMember_id == FamilyMember_id)
                //.Where(i => i.Employee_id == Employee_id)
                //.SingleOrDefault();

            db.DeleteEmployeeAndDependents(FamilyMember_id);
            
            db.Family_Info.Remove(family);
            db.SaveChanges();

            return RedirectToAction("FamilyMemberOverview");
        }

        //----------------------------------------------------------------------------------------
        //check DepEnrollment Methods
        public ActionResult DepEnrollment()
        {
            return View();
        }

        public JsonResult DepEnrollmentNew(int Employee_id, int FamilyMember_id, int OtherInsurance_id,
            string EmpNumber, string RelationshipToInsured, string DepFirstName, string DepLastName,
            DateTime DateOfBirth, string Gender, string CoveredByOtherIns, string InsCompany,
            string PolicyNumber, string InsPhoneNumber, string InsMailingAddress,
            string InsCity, string InsState, string InsZipCode)
        {
            var f = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.OtherInsurance_id == OtherInsurance_id)
                .Single();

            f.SSN = EmpNumber;
            f.RelationshipToInsured = RelationshipToInsured;
            f.FirstName = DepFirstName;
            f.LastName = DepLastName;
            f.DateOfBirth = DateOfBirth;
            f.Gender = Gender;
            
            var o = db.Other_Insurance
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
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

            db.Family_Info.Add(f);
            db.Other_Insurance.Add(o);
            db.SaveChanges();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        //check Edit Methods
        public ActionResult EditDep()
        {
            return View();
        }

        public JsonResult DepEditUpdate(int Employee_id, int FamilyMember_id, int Other_Insurance_id,
            string EmpNumber, string RelationshipToInsured, string DepFirstName, string DepLastName,
             DateTime DateOfBirth, string Gender, string CoveredByOtherIns, string InsCompany, 
             string PolicyNumber, string InsPhoneNumber, string InsMailingAddress,
            string InsCity, string InsState, string InsZipCode)
        {
            var f = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.OtherInsurance_id == Other_Insurance_id)
                .Single();

            f.SSN = EmpNumber;
            f.RelationshipToInsured = RelationshipToInsured;
            f.FirstName = DepFirstName;
            f.LastName = DepLastName;
            f.DateOfBirth = DateOfBirth;
            f.Gender = Gender;
            
            var o = db.Other_Insurance
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.OtherInsurance_id == Other_Insurance_id)
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
                db.Entry(f).State = System.Data.Entity.EntityState.Modified;
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

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetDepDetail(int Employee_id, int FamilyMember_id, int Other_Insurance_id,
            string EmpNumber, string EmpFirstName, string EmpLastName, string DepFirstName,string DepLastName, 
            string RelationshipToInsured, DateTime DateOfBirth, string Gender, string CoveredByOtherIns,
            string InsCompany, string PolicyNumber, string InsPhoneNumber, string InsMailingAddress,
            string InsCity, string InsState, string InsZipCode)
        {
            var f = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.OtherInsurance_id == Other_Insurance_id)
                .Single();

            f.Employee_id = Employee_id;
            f.FamilyMember_id = FamilyMember_id;
            f.OtherInsurance_id = Other_Insurance_id;
            f.SSN = EmpNumber;
            f.FirstName = EmpFirstName;
            f.LastName = EmpLastName;
            f.FirstName = DepFirstName;
            f.LastName = DepLastName;
            f.RelationshipToInsured = RelationshipToInsured;
            f.DateOfBirth = DateOfBirth;
            f.Gender = Gender;
            
            var o = db.Other_Insurance
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.OtherInsurance_id == Other_Insurance_id)
                .Single();

            o.Employee_id = Employee_id;
            o.FamilyMember_id = FamilyMember_id;
            o.OtherInsurance_id = Other_Insurance_id;
            o.CoveredByOtherInsurance = CoveredByOtherIns;
            o.InsuranceCompany = InsCompany;
            o.PolicyNumber = PolicyNumber;
            o.PhoneNumber = InsPhoneNumber;
            o.MailingAddress = InsMailingAddress;
            o.City = InsCity;
            o.State = InsState;
            o.ZipCode = InsZipCode;

            int result = Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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
