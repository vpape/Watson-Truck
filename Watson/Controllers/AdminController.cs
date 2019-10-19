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
    /// <summary>
    /// This is where I give you all the information about my employees
    /// </summary>
    //[Authorize]
    public class AdminController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        private static List<Employee> employee = new List<Employee>();
        private static List<Family_Info> family = new List<Family_Info>();
        private static List<Other_Insurance> otherins = new List<Other_Insurance>();
        private static List<Group_Health> groupIns = new List<Group_Health>();

        public AdminController()
        {
           
        }
        //public ActionResult EmpOverview(Employee employee)
        //{
        //    Employee e = db.Employees.Find(employee);

        //    e = employee;

        //    return View(employee);

        //}

        //EmpOverview
        public ActionResult EmpOverview(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return View(db.Employees.ToList());
            }
            else
            {
                return View(db.Employees.Find(Employee_id));
            }

        }

        //GetEmployee
        public JsonResult GetEmployee(int Employee_id, string empNumber, string FirstName, string LastName, string EmailAddress)
        {
            var e = db.Employees.Find();

            e.SSN = empNumber;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.EmailAddress = EmailAddress;

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //----------------------------------------------------------------------------------------

        public ActionResult EmpEnrollmentSelection(string MaritalStatus)
        {
            var empMaritalStatus = new Employee()
            {
                MaritalStatus = "Single"
            };
            ViewBag.MaritalStatus = empMaritalStatus;

            return View(empMaritalStatus);
        }

        public ActionResult NewEmployeeEnrollment()
        {
            return View();
        }


        //Create-EmpEnrollment
        public JsonResult EmployeeEnrollmentNew(string Role, string CurrentEmployer, string JobTitle, string EmpNumber,
            DateTime HireDate, string MaritalStatus, string FirstName, string LastName, DateTime DateOfBirth, string Gender)
        {
            Employee e = new Employee();

            e.EmployeeRole = Role;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
            e.HireDate = HireDate;
            e.MaritalStatus = MaritalStatus;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;

            db.Employees.Add(e);
            db.SaveChanges();

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.DenyGet);
        }

        //EmpContact Method
        public ActionResult EmpContact()
        {
            return View();
        }

        //Create-EmpContact
        public JsonResult EmpEnrollmentContact(int Employee_id, string MaritalStatus, string MailingAddress, string PObox,
            string City, string State, string ZipCode, string County, string CityLimits, string PhysicalAddress,
            string City2, string State2, string ZipCode2, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            Employee e = db.Employees
               .Where(i => i.Employee_id == Employee_id)
               .Single();

            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.CityLimits = CityLimits;
            e.PhysicalAddress = PhysicalAddress;
            e.CityTwo = City2;
            e.StateTwo = State2;
            e.ZipCodeTwo = ZipCode2;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            ViewBag.MaritalStatus = e.MaritalStatus;
            ViewBag.e = e;

            if (ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();

                    //if (e.MaritalStatus == "Married")
                    //{
                    //     RedirectToAction("SpEnrollment", "Employee", new { e.Employee_id, e.MaritalStatus });
                    //     RedirectToAction("SpEnrollment");
                    //}
                    //else if (e.MaritalStatus == "MarriedwDep")
                    //{
                    //     RedirectToAction("SpEnrollment", new { Employee_id = e.Employee_id, MaritalStatus = e.MaritalStatus });
                    //     RedirectToAction("SpEnrollment", "Employee", new { Employee_id = e.Employee_id, MaritalStatus = e.MaritalStatus });
                    //}
                    //else if (e.MaritalStatus == "SinglewDep")
                    //{
                    //     RedirectToAction("SpEnrollment", "Employee");
                    //     RedirectToAction("DepEnrollment", "Employee", new { e.Employee_id, e.MaritalStatus });
                    //}
                    //else
                    //{
                    //     RedirectToAction("EnrollmentSelection", "Employee", new { e.Employee_id, e.MaritalStatus });
                    //}
                }

                catch (Exception emp)
                {
                    Console.WriteLine(emp);
                }
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Edit-Emp
        public ActionResult EditEmp(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee = e.Employee_id;

            return View(e);
        }

        //EditUpdate-Emp
        public JsonResult EmployeeEditUpdate(int Employee_id, string EmpRole, string CurrentEmployer, string JobTitle,
            string EmpNumber, DateTime HireDate, string FirstName, string LastName, DateTime DateOfBirth, string Gender,
            string MaritalStatus, string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string City2, string State2, string ZipCode2, string CityLimits, string EmailAddress,
            string PhoneNumber, string CellPhone)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.EmployeeRole = EmpRole;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
            e.HireDate = HireDate;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;
            e.MaritalStatus = MaritalStatus;
            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.PhysicalAddress = PhysicalAddress;
            e.CityTwo = City2;
            e.StateTwo = State2;
            e.ZipCodeTwo = ZipCode2;
            e.CityLimits = CityLimits;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            ViewBag.e = e;

            if (ModelState.IsValid)
            {
                db.Entry(e).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { e.Employee_id });
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Get-EmpDetail
        public ActionResult EmpDetail(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee = e;

            return View(e);
        }

        //Get-EmpDetail
        public JsonResult GetEmpDetail(int Employee_id, string City2, string State2, string ZipCode2)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2;

            ViewBag.e = e;

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //DeleteEmp Method
        public ActionResult DeleteEmp(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        //DeleteEmp Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Employee_id)
        {
            Employee e = db.Employees.Find(Employee_id);
            db.Employees.Remove(e);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(Employee_id);

            return RedirectToAction("EmpOverview", new { e.Employee_id });
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EmpFamilyOverview(int? Employee_id)
        {
            var familyInfo = (from fi in db.Family_Info
                              where fi.Employee_id == Employee_id
                              select fi).ToList();
            //Family_Info f = db.Family_Info.Find(family);

            //f = family;

            //return View(db.Family_Info.ToList());
            return View(familyInfo);
        }

        public ActionResult EmpFamilyEnrollment()
        {
            return View();
        }

        //GetFamilyMember
        public JsonResult GetFamilyMember(int Employee_id, int FamilyMember_id, string FirstName, string LastName,
            string RelationshipToInsured, string EmpLastName, string EmpNumber, DateTime DateOfBirth, string MailingAddress,
            string PObox, string City, string State, string County, string ZipCode, string EmailAddress, string PhoneNumber,
            string CellPhone, string Gender, string Employer, string EmployerMailingAddress, string EmployerPObox,
            string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            var f = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            f.FirstName = FirstName;
            f.LastName = LastName;
            f.RelationshipToInsured = RelationshipToInsured;
            f.LastName = EmpLastName;
            f.SSN = EmpNumber;
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
            //f.SSN = SSN;
            f.Employer = Employer;
            f.EmployerMailingAddress = EmployerMailingAddress;
            f.EmployerPObox = EmployerPObox;
            f.EmployerCity = EmployerCity;
            f.EmployerState = EmployerState;
            f.EmployerZipCode = EmployerZipCode;
            f.EmployerPhoneNumber = EmployerPhoneNumber;

            int result = f.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //SpEnrollment Method
        public ActionResult EmpSpEnrollment()
        {
            return View();
        }

        //Create-SpouseEnrollment
        public JsonResult EmpSpEnrollmentNew(int Employee_id, string FirstName, string LastName, DateTime DateOfBirth, string Gender)
        {
            Family_Info sp = new Family_Info();

            //sp.RelationshipToInsured = RelationshipToInsured;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;

            //ViewBag.RelationshipToInsured = RelationshipToInsured;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            int result = sp.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //SpContact Method
        public ActionResult EmpSpContact()
        {
            return View();
        }

        //Create-SpouseContact
        public JsonResult EmpSpEnrollmentContact(int Employee_id, string MailingAddress, string PObox, string City, string State,
            string ZipCode, string County, string PhysicalAddress, string City2, string State2, string ZipCode2,
            string EmailAddress, string PhoneNumber, string CellPhone)
        {
            Family_Info sp = new Family_Info();

            sp.MailingAddress = MailingAddress;
            sp.PObox = PObox;
            sp.City = City;
            sp.State = State;
            sp.ZipCode = ZipCode;
            sp.County = County;
            sp.PhysicalAddress = PhysicalAddress;
            sp.CityTwo = City2;
            sp.StateTwo = State2;
            sp.ZipCodeTwo = ZipCode2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            int result = sp.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //SpEmployment Method
        public ActionResult EmpSpEmployment()
        {
            return View();
        }

        //Create-SpouseEmployment
        public JsonResult SpEmployment(int Employee_id, string MaritalStatus, string Employer, string EmployerAddress,
            string EmployerPObox, string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            Family_Info sp = new Family_Info();

            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerPObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            int result = sp.Employee_id;

            //if (MaritalStatus == "MarriedwDep")
            //{
            //    return RedirectToAction("DepEnrollment", "Family_info", new { sp.Employee_id, sp.MaritalStatus });
            //}

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //EditSp Method
        public ActionResult EditEmpSp(int Employee_id, int? FamilyMember_id)
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

            ViewBag.Family_Info = f.Employee_id;

            return View(f);
        }

        //EditUpdate-Spouse
        public JsonResult SpEditUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus, string RelationshipToInsured,
            string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress, string City2,
            string State2, string ZipCode2, string EmailAddress, string PhoneNumber, string CellPhone, string Employer,
            string EmployerAddress, string EmployerPObox, string EmployerCity, string EmployerState, string EmployerZipCode,
            string EmployerPhoneNumber)
        {
            var sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.RelationshipToInsured == "Spouse")
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
            sp.CityTwo = City2;
            sp.StateTwo = State2;
            sp.ZipCodeTwo = ZipCode2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;
            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerPObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            ViewBag.Employee_id = sp.Employee_id;
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

                RedirectToAction("FamilyOverview", new { sp.Employee_id });
            }


            int result = sp.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Get-SpDetail
        public ActionResult EmpSpDetail(int Employee_id, int? FamilyMember_id)
        {
            //ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.Family_info = f.Employee_id;

            return View(f);
        }

        //Get-SpDetail
        public JsonResult GetSpDetail(int FamilyMember_id)
        {
            var sp = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            int result = sp.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //DeleteSp Method
        public ActionResult DeleteSp(int Employee_id, int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info sp = db.Family_Info.Find(FamilyMember_id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Family_Info = sp.Employee_id;
            return View(sp);
        }

        //DeleteSp Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteSp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? FamilyMember_id)
        {
            Family_Info sp = db.Family_Info.Find(FamilyMember_id);

            Other_Insurance other = db.Other_Insurance.Find(FamilyMember_id);

            //db.DeleteSpouseAndDependent(FamilyMember_id);

            db.Family_Info.Remove(sp);
            db.Other_Insurance.Remove(other);
            db.SaveChanges();

            return RedirectToAction("FamilyOverview", new { sp.Employee_id });
        }
        //----------------------------------------------------------------------------------------

        public ActionResult EmpDepEnrollment()
        {
            return View();
        }

        //Create-DepEnrollment
        public JsonResult EmpDepEnrollmentNew(int Employee_id, int FamilyMember_id, string MaritalStatus, string RelationshipToInsured,
            string DepFirstName, string DepLastName, DateTime DateOfBirth, string Gender, string EmpNumber)
        {
            Family_Info dep = new Family_Info();

            dep.RelationshipToInsured = RelationshipToInsured;
            dep.FirstName = DepFirstName;
            dep.LastName = DepLastName;
            dep.DateOfBirth = DateOfBirth;
            dep.Gender = Gender;

            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;
            ViewBag.RelationshipToInsured = "Dependent";

            Employee e = db.Employees.Find(Employee_id);
            if (e.MaritalStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (e.MaritalStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            var emp = new Employee()
            {
                SSN = EmpNumber
            };
            ViewBag.EmpNumber = emp;

            ViewBag.Family_Info = dep;
            ViewBag.Employee = emp;

            int result = dep.Employee_id;

            db.Family_Info.Add(dep);

            db.SaveChanges();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //EditDep Method
        public ActionResult EditEmpDep(int Employee_id, int? FamilyMember_id)
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

            ViewBag.Family_Info = f.Employee_id;

            return View(f);
        }

        //EditUpdate-DepEdit
        public JsonResult DepEditUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus, string RelationshipToInsured,
            string DepFirstName, string DepLastName, DateTime DateOfBirth, string Gender, string EmpNumber)
        {
            Family_Info dep = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.RelationshipToInsured == "Dependent")
                .Single();

            dep.RelationshipToInsured = RelationshipToInsured;
            dep.FirstName = DepFirstName;
            dep.LastName = DepLastName;
            dep.DateOfBirth = DateOfBirth;
            dep.Gender = Gender;

            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;

            Employee e = db.Employees.Find(Employee_id);
            if (e.MaritalStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (e.MaritalStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            var emp = new Employee()
            {
                SSN = EmpNumber
            };
            ViewBag.EmpNumber = emp;

            ViewBag.Family_Info = dep;
            ViewBag.Employee = emp;

            int result = dep.Employee_id;

            if (ModelState.IsValid)
            {
                db.Entry(dep).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }

                RedirectToAction("FamilyOverview", new { e.Employee_id });
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Get-DepDetail
        public ActionResult EmpDepDetail(int? FamilyMember_id)
        {
            //ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info dep = db.Family_Info.Find(FamilyMember_id);
            if (dep == null)
            {
                return HttpNotFound();
            }

            return View(dep);
        }

        //Get-DepDetail
        public JsonResult GetDepDetail(int FamilyMember_id)
        {
            var dep = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            int result = dep.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //DeleteDep Method
        public ActionResult DeleteDep(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info dep = db.Family_Info.Find(FamilyMember_id);
            if (dep == null)
            {
                return HttpNotFound();
            }
            ViewBag.Family_Info = dep;
            return View(dep);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteDep")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? FamilyMember_id)
        {
            Family_Info dep = db.Family_Info.Find(FamilyMember_id);

            db.DeleteEmployeeAndDependents(FamilyMember_id);

            db.Family_Info.Remove(dep);
            db.SaveChanges();

            return RedirectToAction("FamilyOverview", new { dep.Employee_id });
        }
        //End-Employee,Dep,Spouse----------------------------------------------------------------------------



        //GroupHealth-Start---------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsPremiums()
        {
            return View();
        }

        //Create-InsPrem
        public JsonResult GrpHealthInsPremiumNew(int Employee_id, int InsurancePremium_id, string EmployeeOnly,
            string EmployeeAndSpouse, string EmployeeAndDependent, string EmployeeAndFamily, decimal YearlyPremiumCost)
        {
            InsurancePremium insPremium = new InsurancePremium();

            insPremium.EmployeeOnly = EmployeeOnly;
            insPremium.EmployeeAndSpouse = EmployeeAndSpouse;
            insPremium.EmployeeAndDependent = EmployeeAndDependent;
            insPremium.EmployeeAndFamily = EmployeeAndFamily;
            insPremium.YearlyPremiumCost = YearlyPremiumCost;

            ViewBag.insPremium = insPremium;

            Employee e = db.Employees
            .Where(i => i.Employee_id == Employee_id)
            .Single();

            db.InsurancePremiums.Add(insPremium);

            db.SaveChanges();

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //Edit-InsPrem
        public ActionResult EditGrpHealthInsPremium(int? InsurancePremium_id)
        {
            if (InsurancePremium_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InsurancePremium insPremium = db.InsurancePremiums.Find(InsurancePremium_id);
            if (insPremium == null)
            {
                return HttpNotFound();
            }

            ViewBag.InsurancePremium = insPremium.InsurancePremium_id;

            return View(insPremium);
        }

        //EditUpdate-InsPrem
        public JsonResult GrpHealthInsPremiumEditUpdate(int Employee_id, int InsurancePremium_id, string EmployeeOnly,
            string EmployeeAndSpouse, string EmployeeAndDependent, string EmployeeAndFamily, decimal YearlyPremiumCost)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            InsurancePremium insPremium = db.InsurancePremiums
                .Where(i => i.InsurancePremium_id == InsurancePremium_id)
                .Single();

            insPremium.EmployeeOnly = EmployeeOnly;
            insPremium.EmployeeAndSpouse = EmployeeAndSpouse;
            insPremium.EmployeeAndDependent = EmployeeAndDependent;
            insPremium.EmployeeAndFamily = EmployeeAndFamily;
            insPremium.YearlyPremiumCost = YearlyPremiumCost;

            ViewBag.insPremium = insPremium;

            if (ModelState.IsValid)
            {
                db.Entry(insPremium).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { e.Employee_id });
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsSupplement()
        {
            return View();
        }

        //Create-InsSupplment
        //ask about the Dental and Vision Cost sheet premimums- do they need to be added to db.InsPremimum table
        public JsonResult GrpHealthInsSupplementNew(int InsurancePlanDetail_id, string CalendarYearDeductible, string WaivedForPreventive,
            string AnnualMaximum, string Preventive, string Basic, string Major, string UCRpercentage, string EndoPeridontics,
            string Orthodontia, string OrthodontiaLifetimeMax, string WaitingPeriod, string DentalNetWork, string Exams,
            string Materials, string LensesSingleVision, string BiFocal, string TriFocal, string Lenticular,
            string ContactsMedicallyNecessary, string ContactsElective, string Frames, string Network, string DentalNetwork,
            string RateGuarantee, string Item, string Detail)
        {
            InsurancePlanDetail insPlanDetail = new InsurancePlanDetail();

            //"EmpDentalCost": EmpDentalCost,
            //"EmpVisionCost": EmpVisionCost,
            //"EmpSpDentalCost": EmpSpDentalCost,
            //"EmpSpVisionCost": EmpSpVisionCost,
            //"EmpDepDentalCost": EmpDepDentalCost,
            //"EmpDepVisionCost": EmpDepVisionCost,
            //"EmpFamDentalCost": EmpFamDentalCost,
            //"EmpFamVisionCost": EmpFamVisionCost,

            insPlanDetail.CalendarYearDeductible = CalendarYearDeductible;
            insPlanDetail.WaivedForPreventive = WaivedForPreventive;
            insPlanDetail.AnnualMaximum = AnnualMaximum;
            insPlanDetail.Preventive = Preventive;
            insPlanDetail.Basic = Basic;
            insPlanDetail.Major = Major;
            insPlanDetail.UCRPercentage = UCRpercentage;
            insPlanDetail.EndodonticsOrPeriodontics = EndoPeridontics;
            insPlanDetail.Orthodontia = Orthodontia;
            insPlanDetail.OrthodontiaLifeTimeMaximum = OrthodontiaLifetimeMax;
            insPlanDetail.WaitingPeriod = WaitingPeriod;
            insPlanDetail.DentalNetwork = DentalNetWork;

            insPlanDetail.Exams = Exams;
            insPlanDetail.Materials = Materials;
            insPlanDetail.LensesSingleVision = LensesSingleVision;
            insPlanDetail.Bifocal = BiFocal;
            insPlanDetail.Trifocal = TriFocal;
            insPlanDetail.Lenticular = Lenticular;
            insPlanDetail.ContactMedicallyNecessary = ContactsMedicallyNecessary;
            insPlanDetail.ContactElective = ContactsElective;
            insPlanDetail.Frames = Frames;
            insPlanDetail.Network = Network;
            insPlanDetail.RateGuarantee = RateGuarantee;

            insPlanDetail.Item = Item;
            insPlanDetail.Detail = Detail;

            int result = insPlanDetail.InsurancePlanDetail_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-InsSupplment
        public ActionResult EditGrpHealthSupplement(int? InsurancePlanDetail_id)
        {
            if (InsurancePlanDetail_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InsurancePlanDetail insPlanDetail = db.InsurancePlanDetails.Find(InsurancePlanDetail_id);
            if (insPlanDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.InsurancePlanDetail = insPlanDetail.InsurancePlan_id;

            return View(insPlanDetail);
        }

        //EditUpdate-InsSupplment
        public JsonResult GrpHealthInsSupplementEditUpdate(int InsurancePlanDetail_id, string CalendarYearDeductible, string WaivedForPreventive,
            string AnnualMaximum, string Preventive, string Basic, string Major, string UCRpercentage, string EndoPeridontics,
            string Orthodontia, string OrthodontiaLifetimeMax, string WaitingPeriod, string DentalNetWork, string Exams,
            string Materials, string LensesSingleVision, string BiFocal, string TriFocal, string Lenticular,
            string ContactsMedicallyNecessary, string ContactsElective, string Frames, string Network, string DentalNetwork,
            string RateGuarantee, string Item, string Detail)
        {
            var insPlanDetail = db.InsurancePlanDetails
                .Where(i => i.InsurancePlanDetail_id == InsurancePlanDetail_id)
                .Single();

            //"EmpDentalCost": EmpDentalCost,
            //"EmpVisionCost": EmpVisionCost,
            //"EmpSpDentalCost": EmpSpDentalCost,
            //"EmpSpVisionCost": EmpSpVisionCost,
            //"EmpDepDentalCost": EmpDepDentalCost,
            //"EmpDepVisionCost": EmpDepVisionCost,
            //"EmpFamDentalCost": EmpFamDentalCost,
            //"EmpFamVisionCost": EmpFamVisionCost,

            insPlanDetail.CalendarYearDeductible = CalendarYearDeductible;
            insPlanDetail.WaivedForPreventive = WaivedForPreventive;
            insPlanDetail.AnnualMaximum = AnnualMaximum;
            insPlanDetail.Preventive = Preventive;
            insPlanDetail.Basic = Basic;
            insPlanDetail.Major = Major;
            insPlanDetail.UCRPercentage = UCRpercentage;
            insPlanDetail.EndodonticsOrPeriodontics = EndoPeridontics;
            insPlanDetail.Orthodontia = Orthodontia;
            insPlanDetail.OrthodontiaLifeTimeMaximum = OrthodontiaLifetimeMax;
            insPlanDetail.WaitingPeriod = WaitingPeriod;
            insPlanDetail.DentalNetwork = DentalNetWork;

            insPlanDetail.Exams = Exams;
            insPlanDetail.Materials = Materials;
            insPlanDetail.LensesSingleVision = LensesSingleVision;
            insPlanDetail.Bifocal = BiFocal;
            insPlanDetail.Trifocal = TriFocal;
            insPlanDetail.Lenticular = Lenticular;
            insPlanDetail.ContactMedicallyNecessary = ContactsMedicallyNecessary;
            insPlanDetail.ContactElective = ContactsElective;
            insPlanDetail.Frames = Frames;
            insPlanDetail.Network = Network;
            insPlanDetail.RateGuarantee = RateGuarantee;

            insPlanDetail.Item = Item;
            insPlanDetail.Detail = Detail;

            ViewBag.insPlanDetail = insPlanDetail;

            if (ModelState.IsValid)
            {
                db.Entry(insPlanDetail).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { insPlanDetail.InsurancePlan_id });
            }

            int result = insPlanDetail.InsurancePlan_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthEnrollment()
        {
            return View();
        }

        //Create-GrpHealthEnrollment
        public JsonResult GrpHealthEnrollmentNew(int GrpHealthIns_id, int Employee_id, int InsurancePlan_id, int FamilyMember_id,
            int OtherInsurance_id, string empInsuranceCarrier, string empInsPolicyNumber, string GroupName, string IMSGroupNumber,
            string PhoneNumber, string ReasonForGrpCoverageRefusal, string OtherCoverage, string OtherReason,
            string Myself, string Spouse, string Dependent, string empOtherInsuranceCoverage, DateTime CafeteriaPlanYear,
            string NoneGroupHealthOption, string empOnlyGroupHealthOption, string empSpGroupHealthOption,
            string empDepGroupHealthOption, string empFamGroupHealthOption, string empSignature, DateTime empSignatureDate,
            string empDepartment, string empEnrollmentType, string empPayroll_id, string empClass, string empJobTitle,
            DateTime empHireDate, string empAnnualSalary, DateTime empEffectiveDate, string empHrsWkPerWk,
            string InsMECPlan, string InsStndPlan, string InsBuyUpPlan, string DentalPlan, string VisionPlan,
            string spOtherInsCoverage, string spInsCarrier, string spInsPolicyNumber, string spInsPhoneNumber,
            string spInsMailingAddress, string spInsPObox, string spInsCity, string spInsState, string spInsZipCode,
            string depOtherInsCoverage, string depInsCarrier, string depInsPolicyNumber, string depInsPhoneNumber)
        {
            Group_Health g = new Group_Health();

            g.InsuranceCarrier = empInsuranceCarrier;
            g.PolicyNumber = empInsPolicyNumber;
            g.GroupName = GroupName;
            g.IMSGroupNumber = IMSGroupNumber;
            g.PhoneNumber = PhoneNumber;
            g.ReasonForGrpCoverageRefusal = ReasonForGrpCoverageRefusal;
            g.OtherCoverage = OtherCoverage;
            g.OtherReason = OtherReason;
            g.Myself = Myself;
            g.Spouse = Spouse;
            g.Dependent = Dependent;
            g.OtherInsuranceCoverage = empOtherInsuranceCoverage;
            g.CafeteriaPlanYear = CafeteriaPlanYear;
            g.NoMedicalPlan = NoneGroupHealthOption;
            g.EmployeeOnly = empOnlyGroupHealthOption;
            g.EmployeeAndSpouse = empSpGroupHealthOption;
            g.EmployeeAndDependent = empDepGroupHealthOption;
            g.EmployeeAndFamily = empFamGroupHealthOption;

            Employee emp = new Employee();

            emp.Department = empDepartment;
            emp.EnrollmentType = empEnrollmentType;
            emp.Payroll_id = empPayroll_id;
            emp.Class = empClass;
            emp.AnnualSalary = empAnnualSalary;
            emp.EffectiveDate = empEffectiveDate;
            emp.HoursWorkedPerWeek = empHrsWkPerWk;

            InsurancePlan insPlan = new InsurancePlan();

            insPlan.MECPlan = InsMECPlan;
            insPlan.StandardPlan = InsStndPlan;
            insPlan.BuyUpPlan = InsBuyUpPlan;
            insPlan.DentalPlan = DentalPlan;
            insPlan.VisionPlan = VisionPlan;

            ViewBag.insPlan = insPlan;

            Other_Insurance o = new Other_Insurance();

            o.CoveredByOtherInsurance = spOtherInsCoverage;
            o.InsuranceCarrier = spInsCarrier;
            o.PolicyNumber = spInsPolicyNumber;
            o.PhoneNumber = spInsPhoneNumber;
            o.MailingAddress = spInsMailingAddress;
            o.PObox = spInsPObox;
            o.City = spInsCity;
            o.State = spInsState;
            o.ZipCode = spInsZipCode;
            o.CoveredByOtherInsurance = depOtherInsCoverage;
            o.InsuranceCarrier = depInsCarrier;
            o.PolicyNumber = depInsPolicyNumber;
            o.PhoneNumber = depInsPhoneNumber;

            Employee e = db.Employees
             .Where(i => i.Employee_id == Employee_id)
             .Single();

            ViewBag.e = e;

            Family_Info sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            ViewBag.sp = sp;

            Family_Info dep = db.Family_Info
               .Where(i => i.FamilyMember_id == FamilyMember_id)
               .Single();

            ViewBag.dep = dep;

            if (ModelState.IsValid)
            {
                db.InsurancePlans.Add(insPlan);
                db.Other_Insurance.Add(o);
                db.Group_Health.Add(g);

                db.SaveChanges();
            }

            int result = e.Employee_id;


            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-GrpHealthEnrollment
        public ActionResult EditGrpHealthIns(int? GrpHealthIns_id)
        {
            if (GrpHealthIns_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health g = db.Group_Health.Find(GrpHealthIns_id);
            if (g == null)
            {
                return HttpNotFound();
            }

            return View(g);
        }

        //EditUpdate-GrpHealthEnrollment
        public JsonResult GrpHealthInsEditUpdate(int GrpHealthIns_id, int Employee_id, int InsurancePlan_id, int FamilyMember_id,
            int OtherInsurance_id, string empInsuranceCarrier, string empInsPolicyNumber, string GroupName, string IMSGroupNumber,
            string PhoneNumber, string ReasonForGrpCoverageRefusal, string OtherCoverage, string OtherReason,
            string Myself, string Spouse, string Dependent, string empOtherInsuranceCoverage, DateTime CafeteriaPlanYear,
            string NoneGroupHealthOption, string empOnlyGroupHealthOption, string empSpGroupHealthOption,
            string empDepGroupHealthOption, string empFamGroupHealthOption, string empSignature, DateTime empSignatureDate,
            string empDepartment, string empEnrollmentType, string empPayroll_id, string empClass, string empJobTitle,
            DateTime empHireDate, string empAnnualSalary, DateTime empEffectiveDate, string empHrsWkPerWk,
            string InsMECPlan, string InsStndPlan, string InsBuyUpPlan, string DentalPlan, string VisionPlan,
            string spOtherInsCoverage, string spInsCarrier, string spInsPolicyNumber, string spInsPhoneNumber,
            string spInsMailingAddress, string spInsPObox, string spInsCity, string spInsState, string spInsZipCode,
            string depOtherInsCoverage, string depInsCarrier, string depInsPolicyNumber, string depInsPhoneNumber)
        {
            var g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GrpHealthIns_id)
                .Single();

            g.InsuranceCarrier = empInsuranceCarrier;
            g.PolicyNumber = empInsPolicyNumber;
            g.GroupName = GroupName;
            g.IMSGroupNumber = IMSGroupNumber;
            g.PhoneNumber = PhoneNumber;
            g.ReasonForGrpCoverageRefusal = ReasonForGrpCoverageRefusal;
            g.OtherCoverage = OtherCoverage;
            g.OtherReason = OtherReason;
            g.Myself = Myself;
            g.Spouse = Spouse;
            g.Dependent = Dependent;
            g.OtherInsuranceCoverage = empOtherInsuranceCoverage;
            g.CafeteriaPlanYear = CafeteriaPlanYear;
            g.NoMedicalPlan = NoneGroupHealthOption;
            g.EmployeeOnly = empOnlyGroupHealthOption;
            g.EmployeeAndSpouse = empSpGroupHealthOption;
            g.EmployeeAndDependent = empDepGroupHealthOption;
            g.EmployeeAndFamily = empFamGroupHealthOption;

            ViewBag.g = g;

            Employee e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.Department = empDepartment;
            e.EnrollmentType = empEnrollmentType;
            e.Payroll_id = empPayroll_id;
            e.Class = empClass;
            e.AnnualSalary = empAnnualSalary;
            e.JobTitle = empJobTitle;
            e.HireDate = empHireDate;
            e.EffectiveDate = empEffectiveDate;
            e.HoursWorkedPerWeek = empHrsWkPerWk;

            ViewBag.e = e;

            InsurancePlan insPlan = db.InsurancePlans
              .Where(i => i.InsurancePlan_id == InsurancePlan_id)
              .Single();

            insPlan.MECPlan = InsMECPlan;
            insPlan.StandardPlan = InsStndPlan;
            insPlan.BuyUpPlan = InsBuyUpPlan;
            insPlan.DentalPlan = DentalPlan;
            insPlan.VisionPlan = VisionPlan;

            ViewBag.insPlan = insPlan;

            Other_Insurance o = db.Other_Insurance
               .Where(i => i.OtherInsurance_id == OtherInsurance_id)
               .Single();

            o.CoveredByOtherInsurance = spOtherInsCoverage;
            o.InsuranceCarrier = spInsCarrier;
            o.PolicyNumber = spInsPolicyNumber;
            o.PhoneNumber = spInsPhoneNumber;
            o.MailingAddress = spInsMailingAddress;
            o.PObox = spInsPObox;
            o.City = spInsCity;
            o.State = spInsState;
            o.ZipCode = spInsZipCode;
            o.CoveredByOtherInsurance = depOtherInsCoverage;
            o.InsuranceCarrier = depInsCarrier;
            o.PolicyNumber = depInsPolicyNumber;
            o.PhoneNumber = depInsPhoneNumber;

            ViewBag.o = o;

            if (ModelState.IsValid)
            {
                db.Entry(g).State = System.Data.Entity.EntityState.Modified;
                db.Entry(o).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview");
            }

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsDetail(int? GrpHealthIns_id)
        {
            if (GrpHealthIns_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group_Health g = db.Group_Health.Find(GrpHealthIns_id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        public JsonResult GetGrpHealthInsDetail(int GrpHealthIns_id, int OtherInsurance_id)
        {
            var g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GrpHealthIns_id)
                .Single();

            ViewBag.g = g;

            var o = db.Other_Insurance
            .Where(i => i.OtherInsurance_id == OtherInsurance_id)
            .Single();

            ViewBag.o = o;

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //GroupHealth-End---------------------------------------------------------------------------------------

        //SalaryRedirect-Start----------------------------------------------------------------------------------

        public ActionResult SalaryRedirectAgreement()
        {
            return View();
        }

        //Create-SalaryRedirect
        public JsonResult SalaryRedirectionUpdate(int Employee_id, int? Deductions_id, string MedicalCoverage, string MedicalInsProvider, string EEelectionPreTaxMedIns,
            string PremiumPreTaxMedIns, string EEelectionPostTaxMedIns, string PremiumPostTaxMedIns, string DentalCoverage, string DentalInsProvider,
            string EEelectionPreTaxDentalIns, string PremiumPreTaxDentalIns, string EEelectionPostTaxDentalIns, string PremiumPostTaxDentalIns, string VisionCoverage,
            string VisionInsProvider, string EEelectionPreTaxVisionIns, string PremiumPreTaxVisionIns, string EEelectionPostTaxVisionIns, string PremiumPostTaxVisionIns,
            string AccidentCoverage, string AccidentInsProvider, string EEelectionPreTaxAccidentIns, string PremiumPreTaxAccidentIns, string EEelectionPostTaxAccidentIns,
            string PremiumPostTaxAccidentIns, string CancerCoverage, string CancerInsProvider, string EEelectionPreTaxCancerIns, string PremiumPreTaxCancerIns,
            string EEelectionPostTaxCancerIns, string PremiumPostTaxCancerIns, string ShortTermDisabilityCoverage, string StDisabilityProvider,
            string EEelectionPreTaxStDisability, string PremiumPreTaxStDisability, string EEelectionPostTaxStDisability, string PremiumPostTaxStDisability,
            string HospitalIndemnityCoverage, string HospitalIndemProvider, string EEelectionPreTaxHospitalIndem, string PremiumPreTaxHospitalIndem,
            string EEelectionPostTaxHospitalIndem, string PremiumPostTaxHospitalIndem, string TermLifeCoverage, string TermLifeInsProvider,
            string EEelectionPreTaxTermLifeIns, string PremiumPreTaxTermLifeIns, string EEelectionPostTaxTermLifeIns, string PremiumPostTaxTermLifeIns,
            string WholeLifeCoverage, string WholeLifeInsProvider, string EEelectionPreTaxWholeLifeIns, string PremiumPreTaxWholeLifeIns,
            string EEelectionPostTaxWholeLifeIns, string PremiumPostTaxWholeLifeIns, string OtherCoverage, string OtherInsProvider, string EEelectionPreTaxOtherIns,
            string PremiumPreTaxOtherIns, string EEelectionPostTaxOtherIns, string PremiumPostTaxOtherIns, string TotalPreTax, string TotalPostTax,
            string empInitials1, string empSignature, DateTime empSignatureDate)
        {
            Deduction d = new Deduction();

            d.Employee_id = Employee_id;
            d.MedicalCoverage = MedicalCoverage;
            d.MedicalInsProvider = MedicalInsProvider;
            d.EEelectionPreTaxMedIns = EEelectionPreTaxMedIns;
            d.PremiumPreTaxMedIns = PremiumPreTaxMedIns;
            d.EEelectionPostTaxMedIns = EEelectionPostTaxMedIns;
            d.PremiumPostTaxMedIns = PremiumPostTaxMedIns;

            d.DentalCoverage = DentalCoverage;
            d.DentalInsProvider = DentalInsProvider;
            d.EEelectionPreTaxDentalIns = EEelectionPreTaxDentalIns;
            d.PremiumPreTaxDentalIns = PremiumPreTaxDentalIns;
            d.EEelectionPostTaxDentalIns = EEelectionPostTaxDentalIns;
            d.PremiumPostTaxDentalIns = PremiumPostTaxDentalIns;

            //d.VisionCoverage = VisionCoverage;
            //d.VisionInsProvider = VisionInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.AccidentCoverage = AccidentCoverage;
            //d.AccidentInsProvider = AccidentInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.CancerCoverage = CancerCoverage;
            //d.CancerInsProvider = CancerInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.ShortTermDisabilityCoverage = ShortTermDisabilityCoverage;
            //d.StDisabilityProvider = StDisabilityProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.HospitalIndemnityCoverage = HospitalIndemnityCoverage;
            //d.HospitalIndemProvider = HospitalIndemProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.TermLifeCoverage = TermLifeCoverage;
            //d.TermLifeInsProvider = TermLifeInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.WholeLifeCoverage = WholeLifeCoverage;
            //d.WholeLifeInsProvider = WholeLifeInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.OtherCoverage = OtherCoverage;
            //d.OtherInsProvider = OtherInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;


            d.TotalPreTax = TotalPreTax;
            d.TotalPostTax = TotalPostTax;
            d.EmployeeSignature = empSignature;
            d.EmployeeSignatureDate = empSignatureDate;
            d.EmployeeInitials = empInitials1;


            db.Deductions.Add(d);
            db.SaveChanges();

            int result = d.Deductions_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-SalaryRedirect
        public ActionResult EditSalaryRedirection(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health g = db.Group_Health.Find(Employee_id);
            if (g == null)
            {
                return HttpNotFound();
            }

            ViewBag.g = g.Employee_id;

            return View(g);
        }

        //EditUpdate-SalaryRedirect
        public JsonResult SalaryRedirectionEditUpdate(int? Employee_id, int? Deductions_id, string MedicalCoverage, string MedicalInsProvider, string EEelectionPreTaxMedIns,
            string PremiumPreTaxMedIns, string EEelectionPostTaxMedIns, string PremiumPostTaxMedIns, string DentalCoverage, string DentalInsProvider,
            string EEelectionPreTaxDentalIns, string PremiumPreTaxDentalIns, string EEelectionPostTaxDentalIns, string PremiumPostTaxDentalIns, string VisionCoverage,
            string VisionInsProvider, string EEelectionPreTaxVisionIns, string PremiumPreTaxVisionIns, string EEelectionPostTaxVisionIns, string PremiumPostTaxVisionIns,
            string AccidentCoverage, string AccidentInsProvider, string EEelectionPreTaxAccidentIns, string PremiumPreTaxAccidentIns, string EEelectionPostTaxAccidentIns,
            string PremiumPostTaxAccidentIns, string CancerCoverage, string CancerInsProvider, string EEelectionPreTaxCancerIns, string PremiumPreTaxCancerIns,
            string EEelectionPostTaxCancerIns, string PremiumPostTaxCancerIns, string ShortTermDisabilityCoverage, string StDisabilityProvider,
            string EEelectionPreTaxStDisability, string PremiumPreTaxStDisability, string EEelectionPostTaxStDisability, string PremiumPostTaxStDisability,
            string HospitalIndemnityCoverage, string HospitalIndemProvider, string EEelectionPreTaxHospitalIndem, string PremiumPreTaxHospitalIndem,
            string EEelectionPostTaxHospitalIndem, string PremiumPostTaxHospitalIndem, string TermLifeCoverage, string TermLifeInsProvider,
            string EEelectionPreTaxTermLifeIns, string PremiumPreTaxTermLifeIns, string EEelectionPostTaxTermLifeIns, string PremiumPostTaxTermLifeIns,
            string WholeLifeCoverage, string WholeLifeInsProvider, string EEelectionPreTaxWholeLifeIns, string PremiumPreTaxWholeLifeIns,
            string EEelectionPostTaxWholeLifeIns, string PremiumPostTaxWholeLifeIns, string OtherCoverage, string OtherInsProvider, string EEelectionPreTaxOtherIns,
            string PremiumPreTaxOtherIns, string EEelectionPostTaxOtherIns, string PremiumPostTaxOtherIns, string TotalPreTax, string TotalPostTax,
            string empInitials1, string empSignature, DateTime empSignatureDate)
        {

            Deduction d = db.Deductions
                .Where(i => i.Deductions_id == Deductions_id)
                .Single();

            d.MedicalCoverage = MedicalCoverage;
            d.MedicalInsProvider = MedicalInsProvider;
            d.EEelectionPreTaxMedIns = EEelectionPreTaxMedIns;
            d.PremiumPreTaxMedIns = PremiumPreTaxMedIns;
            d.EEelectionPostTaxMedIns = EEelectionPostTaxMedIns;
            d.PremiumPostTaxMedIns = PremiumPostTaxMedIns;

            d.DentalCoverage = DentalCoverage;
            d.DentalInsProvider = DentalInsProvider;
            d.EEelectionPreTaxDentalIns = EEelectionPreTaxDentalIns;
            d.PremiumPreTaxDentalIns = PremiumPreTaxDentalIns;
            d.EEelectionPostTaxDentalIns = EEelectionPostTaxDentalIns;
            d.PremiumPostTaxDentalIns = PremiumPostTaxDentalIns;

            //d.VisionCoverage = VisionCoverage;
            //d.VisionInsProvider = VisionInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.AccidentCoverage = AccidentCoverage;
            //d.AccidentInsProvider = AccidentInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.CancerCoverage = CancerCoverage;
            //d.CancerInsProvider = CancerInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.ShortTermDisabilityCoverage = ShortTermDisabilityCoverage;
            //d.StDisabilityProvider = StDisabilityProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.HospitalIndemnityCoverage = HospitalIndemnityCoverage;
            //d.HospitalIndemProvider = HospitalIndemProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.TermLifeCoverage = TermLifeCoverage;
            //d.TermLifeInsProvider = TermLifeInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.WholeLifeCoverage = WholeLifeCoverage;
            //d.WholeLifeInsProvider = WholeLifeInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            //d.OtherCoverage = OtherCoverage;
            //d.OtherInsProvider = OtherInsProvider;
            //d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            //d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            //d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            //d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;


            d.TotalPreTax = TotalPreTax;
            d.TotalPostTax = TotalPostTax;
            d.EmployeeSignature = empSignature;
            d.EmployeeSignatureDate = empSignatureDate;
            d.EmployeeInitials = empInitials1;

            ViewBag.d = d;

            if (ModelState.IsValid)
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { d.Employee_id });
            }

            int result = d.Deductions_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //SalaryRedirect-End----------------------------------------------------------------------------------

        //AuthorizationForm-Start-----------------------------------------------------------------------------

        public ActionResult AuthorizationForm()
        {
            return View();
        }

        //Create-AuthorizationForm
        public JsonResult AuthorizationFormNew(int Employee_id, string NameOfPersonOne, string PersonOneRelationship, string NameOfPersonTwo,
            string PersonTwoRelationship, string PolicyHolderSignature, DateTime PolicyHolderSignatureDate, string PersonOneSignature,
            DateTime PersonOneSignatureDate, string PersonTwoSignature, DateTime PersonTwoSignatureDate)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            ViewBag.e = e;

            Group_Health g = new Group_Health();

            g.NameOfPersonOneReleaseInfoTo = NameOfPersonOne;
            g.PersonOneRelationship = PersonOneRelationship;
            g.NameOfPersonTwoReleaseInfoTo = NameOfPersonTwo;
            g.PersonTwoRelationship = PersonTwoRelationship;
            g.AuthorizationFormPolicyHolderSignature = PolicyHolderSignature;
            g.AuthorizationFormPolicyHolderSignatureDate = PolicyHolderSignatureDate;
            g.PersonOneSignature = PersonOneSignature;
            g.PersonOneSignatureDate = PersonOneSignatureDate;
            g.PersonTwoSignature = PersonTwoSignature;
            g.PersonTwoSignatureDate = PersonTwoSignatureDate;

            ViewBag.g = g;

            db.Group_Health.Add(g);
            db.SaveChanges();

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-AuthorizationForm
        public ActionResult EditAuthorizationForm(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health g = db.Group_Health.Find(Employee_id);
            if (g == null)
            {
                return HttpNotFound();
            }

            ViewBag.g = g.Employee_id;

            return View(g);
        }

        //EditUpdate-AuthorizationForm
        public JsonResult AuthorizationFormEditUpdate(int Employee_id, int GrpHealthIns_id, string NameOfPersonOne,
            string PersonOneRelationship, string NameOfPersonTwo, string PersonTwoRelationship, string PolicyHolderSignature,
            DateTime PolicyHolderSignatureDate, string PersonOneSignature, DateTime PersonOneSignatureDate,
            string PersonTwoSignature, DateTime PersonTwoSignatureDate)
        {
            Employee e = db.Employees
               .Where(i => i.Employee_id == Employee_id)
               .Single();

            ViewBag.e = e;

            Group_Health g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GrpHealthIns_id)
                .Single();

            g.Employee_id = Employee_id;
            g.GroupHealthInsurance_id = GrpHealthIns_id;
            g.NameOfPersonOneReleaseInfoTo = NameOfPersonOne;
            g.PersonOneRelationship = PersonOneRelationship;
            g.NameOfPersonTwoReleaseInfoTo = NameOfPersonTwo;
            g.PersonTwoRelationship = PersonTwoRelationship;
            g.AuthorizationFormPolicyHolderSignature = PolicyHolderSignature;
            g.AuthorizationFormPolicyHolderSignatureDate = PolicyHolderSignatureDate;
            g.PersonOneSignature = PersonOneSignature;
            g.PersonOneSignatureDate = PersonOneSignatureDate;
            g.PersonTwoSignature = PersonTwoSignature;
            g.PersonTwoSignatureDate = PersonTwoSignatureDate;

            ViewBag.g = g;

            if (ModelState.IsValid)
            {
                db.Entry(g).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { g.Employee_id });
            }

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //AuthorizationForm-End-----------------------------------------------------------------------------

        public ActionResult DeleteGrpHealthIns(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health groupHealth = db.Group_Health.Find(id);
            if (groupHealth == null)
            {
                return HttpNotFound();
            }

            return View(groupHealth);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteGrpHealthIns")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGroupHealth(int id)
        {
            Group_Health groupHealth = db.Group_Health.Find(id);
            db.Group_Health.Remove(groupHealth);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(id);

            return RedirectToAction("GrpHealthEnrollment");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        //Haven't started lifeInsurance








        //LifeInsurance-Start-----------------------------------------------------------------------------

        public ActionResult LifeInsuranceEnrollment()
        {
            return View();
        }

        //Create-LifeIns
        public JsonResult LifeInsEnrollmentNew(int lifeIns_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                         {
                             e.LifeInsurance_id,
                             e.Employee_id,
                             e.GroupPlanNumber,
                             e.BenefitsEffectiveDate,
                             e.InitialEnrollment,
                             e.ReEnrollment,
                             e.AddEmployeeAndDependents,
                             e.DropRefuseCoverage,
                             e.InformationChange,
                             e.IncreaseAmount,
                             e.FamilyStatusChange,
                             e.MarriedOrHaveSpouse,
                             e.HaveChildrenOrHaveDependents,
                             e.DateOfMarriage,
                             e.PlacementDateOfAdoptedChild,
                             e.AddDependent,
                             e.DropDependent,
                             e.Student,
                             e.Disabled,
                             e.NonStandardDependent,
                             e.DropEmployee,
                             e.DropSpouse,
                             e.DropDependents,
                             e.LastDayOfCoverage,
                             e.TerminationOfEmployment,
                             e.Retirement,
                             e.LastDayWorked,
                             e.OtherEvent,
                             e.OtherEventDate,
                             e.EmployeeDentalDrop,
                             e.SpouseDentalDrop,
                             e.DependentDentalDrop,
                             e.EmployeeVisionDrop,
                             e.SpouseVisionDrop,
                             e.DependentVisionDrop,
                             e.DropBasicLife,
                             e.DropDental,
                             e.DropVision,
                             e.TerminationOfEmploymentDate,
                             e.Divorce,
                             e.DivorceDate,
                             e.DeathOfSpouse,
                             e.DeathOfSpouseDate,
                             e.TerminationOrExpirationOfCoverage,
                             e.TerminationOrExpirationOfCoverageDate,
                             e.DentalCoverageLost,
                             e.VisionCoverageLost,
                             e.CoveredUnderOtherInsurance,
                             e.CoveredUnderOtherInsReason,
                             e.EmployeeOnly,
                             e.EmployeeAndSpouse,
                             e.EmployeeAndDependent,
                             e.EmployeeAndFamily,
                             e.DoNotWantDentalCoverage,
                             e.EmployeeCoveredUnderOtherDentalPlan,
                             e.SpouseCoveredUnderOtherDentalPlan,
                             e.DependentsCoveredUnderOtherDentalPlan,
                             e.DoNotWantVisionCoverage,
                             e.EmployeeCoveredUnderOtherVisionPlan,
                             e.SpouseCoveredUnderOtherVisionPlan,
                             e.DependentsCoveredUnderOtherVisionPlan,
                             e.OwnerBasicLifeWithADandDPolicyAmount,
                             e.ManagerBasicLifeWithADandDPolicyAmount,
                             e.EmployeeBasicLifeWithADandDPolicyAmount,
                             e.SpouseBasicLifeWithADandDPolicyAmount,
                             e.DoNotWantBasicLifeCoverageWithADandD,
                             e.AmountOfPreviousPolicy,
                             e.EmployeeSignature,
                             e.EmployeeSignatureDate
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-LifeIns
        public ActionResult EditLifeInsurance(int? lifeIns_id)
        {
            if (lifeIns_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Life_Insurance lifeIns = db.Life_Insurance.Find(lifeIns_id);
            if (lifeIns_id == null)
            {
                return HttpNotFound();
            }

            return View(lifeIns_id);
        }

        //EditUpdate-LifeIns
        public JsonResult EditLifeInsUpdate(int lifeIns_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                         {
                             e.LifeInsurance_id,
                             e.Employee_id,

                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //LifeInsurance-End-----------------------------------------------------------------------------
    }
}