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
    //[Authorize]
    public class EmployeeController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();
        private static List<Employee> employee = new List<Employee>();
        private static List<Family_Info> family = new List<Family_Info>();
        private static List<Other_Insurance> otherins = new List<Other_Insurance>();

        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeController()
        {
     
        }

        //EmpOverview
        public ActionResult EmpOverview(int? Employee_id)
        {
            ViewBag.Employee_id = Employee_id;

            if (Employee_id == null)
            {
                //return View(db.Employees.Find(Employee_id));
                return View(db.Employees.ToList());
            }
            else
            {
                return View(db.Employees.Find(Employee_id));
            }
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EnrollmentSelection()
        {
            return View();
        }

        public ActionResult NewEmployeeEnrollment()
        {
            return View();
        }
 
        //Create-EmpEnrollment
        public JsonResult EmployeeEnrollmentNew(string Role, string CurrentEmployer, string JobTitle, string EmpNumber, DateTime HireDate, string MaritalStatus,
            string FirstName, string LastName, DateTime DateOfBirth, string Gender, string Active, string Retired, string CobraState)
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
            e.Active = Active;
            e.Retired = Retired;
            e.CobraStateContinuation = CobraState;

            ViewBag.Employee_id = e.Employee_id;

            db.Employees.Add(e);
            db.SaveChanges();

            int result = e.Employee_id;

            return Json( new { data = result }, JsonRequestBehavior.AllowGet);
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
            ViewBag.Employee_id = e.Employee_id;

            if (ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();

                    if (e.MaritalStatus == "Married")
                    {
                        RedirectToAction("SpEnrollment", "Employee", new { e.Employee_id, e.MaritalStatus });
                        RedirectToAction("SpEnrollment");
                    }
                    else if (e.MaritalStatus == "MarriedwDep")
                    {
                        RedirectToAction("SpEnrollment", new { e.Employee_id, e.MaritalStatus });
                        RedirectToAction("SpEnrollment", "Employee", new { e.Employee_id, e.MaritalStatus });
                    }
                    else if (e.MaritalStatus == "SinglewDep")
                    {
                        RedirectToAction("SpEnrollment", "Employee");
                        RedirectToAction("DepEnrollment", "Employee", new { e.Employee_id, e.MaritalStatus });
                    }
                    else
                    {
                        RedirectToAction("EnrollmentSelection", "Employee", new { e.Employee_id, e.MaritalStatus });
                    }
                }

                catch (Exception emp)
                {
                    Console.WriteLine(emp);
                }
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);             
        }

        public JsonResult OtherInsuranceNew(int Employee_id, int? GroupHealthInsurance_id, string empOtherGrpHinsCoverage, string empInsCarrier,
           string empInsPolicyNumber, string empInsPhoneNumber)
        {
            Group_Health grp = new Group_Health();

            grp.Employee_id = Employee_id;
            grp.OtherInsuranceCoverage = empOtherGrpHinsCoverage;
            grp.InsuranceCarrier = empInsCarrier;
            grp.PolicyNumber = empInsPolicyNumber;
            grp.PhoneNumber = empInsPhoneNumber;

            ViewBag.GroupHealthInsurance_id = grp.GroupHealthInsurance_id;

            int result = grp.GroupHealthInsurance_id;

            db.Group_Health.Add(grp);
            db.SaveChanges();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Edit-Emp
        public ActionResult EditEmployee(int? Employee_id, string MaritalStatus)
        {
            EmployeeAndInsuranceVM employeeAndInsuranceVM = new EmployeeAndInsuranceVM();

            employeeAndInsuranceVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            employeeAndInsuranceVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);

            ViewBag.MaritalStatus = employeeAndInsuranceVM.employee.MaritalStatus;

            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

           
            ViewBag.Employee_id = e.Employee_id;

            return View(employeeAndInsuranceVM);
        }
       
        //EditUpdate-Emp
        public JsonResult EmployeeEditUpdate(int? Employee_id, string EmpRole, string CurrentEmployer, string JobTitle, 
            string EmpNumber, /*DateTime HireDate,*/ string FirstName, string LastName, /*DateTime DateOfBirth,*/ string Gender,
            string MaritalStatus, string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string City2, string State2, string ZipCode2, string CityLimits, string EmailAddress, 
            string PhoneNumber, string CellPhone, string OtherGrpHinsCoverage, string InsCarrier,
            string InsPolicyNumber, string InsPhoneNumber)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.EmployeeRole = EmpRole;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
            //e.HireDate = HireDate;
            e.FirstName = FirstName;
            e.LastName = LastName;
            //e.DateOfBirth = DateOfBirth;
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

            var grph = db.Group_Health
              .Where(i => i.Employee_id == Employee_id)
              .Single();

            grph.OtherInsuranceCoverage = OtherGrpHinsCoverage;
            grph.InsuranceCarrier = InsCarrier;
            grph.PolicyNumber = InsPolicyNumber;
            grph.PhoneNumber = InsPhoneNumber;

            ViewBag.Employee_id = e.Employee_id;

            if (ModelState.IsValid)
            {
                db.Entry(e).State = System.Data.Entity.EntityState.Modified;
                db.Entry(grph).State = System.Data.Entity.EntityState.Modified;

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
        public ActionResult EmployeeDetail(int? Employee_id)
        {
            EmployeeAndInsuranceVM employeeAndInsuranceVM = new EmployeeAndInsuranceVM();

            employeeAndInsuranceVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            employeeAndInsuranceVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);

            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee_id = Employee_id;

            return View(employeeAndInsuranceVM);
        }

        //Get-EmpDetail
        //public JsonResult GetEmpDetail(int? Employee_id, string CurrentEmployer, string JobTitle, string EmpNumber, /*DateTime HireDate,*/
        //    string FirstName, string LastName, /*DateTime DateOfBirth,*/ string MaritalStatus, string Gender, string MailingAddress, string PObox,
        //    string City, string State, string ZipCode, string PhysicalAddress, string CityTwo, string StateTwo, string ZipCodeTwo,
        //    string EmailAddress, string PhoneNumber, string CellPhone, string CityLimits, string empOtherGrpHinsCoverage,
        //    string empInsuranceCarrier, string empInsPolicyNumber, string empInsPhoneNumber)
        //{
        //    Employee e = db.Employees
        //    .Where(i => i.Employee_id == Employee_id)
        //    .Single();    

        //    int result = e.Employee_id;

        //    return Json(new { data = result }, JsonRequestBehavior.AllowGet);    
        //}
        //----------------------------------------------------------------------------------------

        //DeleteEmp Method
        public ActionResult DeleteEmp(int? Employee_id)
        {
            EmployeeAndInsuranceVM employeeAndInsuranceVM = new EmployeeAndInsuranceVM();

            employeeAndInsuranceVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            employeeAndInsuranceVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);

            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(employeeAndInsuranceVM);
        }

        //DeleteEmp Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Employee_id)
        {
            Employee e = db.Employees.Find(Employee_id);

            db.DeleteEmployeeAndDependents(Employee_id);

            db.Employees.Remove(e);
            //db.SaveChanges();

            

            return RedirectToAction("EmpOverview", new { e.Employee_id });
        }

        //----------------------------------------------------------------------------------------

        public ActionResult FamilyOverview(int? Employee_id, int? FamilyMember_id)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.FamilyMember_id = FamilyMember_id;

            var familyInfo = (from fi in db.Family_Info
                              where fi.Employee_id == Employee_id
                              select fi).ToList();

            return View(familyInfo);
        }

        //show/hide family enrollment code block based on martial status
        public ActionResult FamilyEnrollment(int? Employee_id, int? FamilyMember_id, string MaritalStatus)
        {
            
            Employee e = db.Employees.Find(Employee_id);

            ViewBag.Employee_id = Employee_id;
            ViewBag.FamilyMember_id = FamilyMember_id;
            //ViewBag.MaritalStatus = e.MaritalStatus;

            //if (e.MaritalStatus == "Single")
            //{
            //    ViewBag.spouseExist = false;
            //    ViewBag.MaritalStatus = "Single";
            //}
            //else if (e.MaritalStatus == "SinglewDep")
            //{
            //    ViewBag.spouseExist = false;
            //    ViewBag.MaritalStatus = "SinglewDep";
            //}
            //else if (e.MaritalStatus == "Married")
            //{
            //    ViewBag.spouseExist = true;
            //    ViewBag.MaritalStatus = "Married";
            //}
            //else
            //{
            //    ViewBag.spouseExist = true;
            //    ViewBag.MaritalStatus = "MarriedwDep";
            //}


            return View();
        }
        
        //----------------------------------------------------------------------------------------

        //SpEnrollment Method
        public ActionResult SpouseEnrollment(int Employee_id, int? FamilyMember_id, string MaritalStatus, string RelationshipToInsured)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.FamilyMember_id = FamilyMember_id;
            ViewBag.RelationshipToInsured = RelationshipToInsured = "Spouse";

            Employee e = db.Employees.Find(Employee_id);
            ViewBag.MaritalStatus = e.MaritalStatus;
            
            return View();
        }

        //Create-SpouseEnrollment
        public JsonResult SpEnrollmentNew(int Employee_id, int? FamilyMember_id, string RelationshipToInsured, string MaritalStatus, string SSN, 
            string FirstName, string LastName, DateTime DateOfBirth, string Gender)
        {

            //int record = (from fi in db.Family_Info
            //              where fi.FamilyMember_id == FamilyMember_id
            //              where fi.RelationshipToInsured == "Spouse"
            //              select fi).Count();

            //if (record > 0)
            //{
            //    ViewBag.Message = "Record already exists.";
            //    RedirectToAction("FamilyOverview", new { sp.Employee_id });
            //}
            //else
            //{
            //    RedirectToAction("DepEnrollment", new { sp.Employee_id });

            //}

            Family_Info sp = new Family_Info();

            sp.Employee_id = Employee_id;
            sp.RelationshipToInsured = RelationshipToInsured;
            sp.MaritalStatus = MaritalStatus;
            sp.SSN = SSN;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;

            db.Family_Info.Add(sp);
            db.SaveChanges();

            int result = sp.FamilyMember_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //Create-SpouseContact
        public JsonResult SpEnrollmentContact(int? Employee_id, int? FamilyMember_id, string MailingAddress, string PObox, string City,
            string State, string ZipCode, string County, string PhysicalAddress, string City2, string State2, string ZipCode2, 
            string EmailAddress, string PhoneNumber, string CellPhone)
        {
            Family_Info sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

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

            db.SaveChanges();
            
            int result = sp.FamilyMember_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Create-SpouseEmployment
        public JsonResult SpEnrollmentEmployment(int? FamilyMember_id, int Employee_id, string Employer, string EmployerAddress,
            string EmployerPObox, string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber,
            string spOtherInsuranceCoverage, string spOtherMedicalCoverage, string spOtherDentalCoverage, string spOtherVisionCoverage,
            string spIndemnityCoverage)
        {
            
            var sp = (from fi in db.Family_Info
                      where fi.FamilyMember_id == FamilyMember_id
                      select fi).SingleOrDefault();

            ViewBag.Employee_id = Employee_id;
            
            sp.Employee_id = Employee_id;
            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerPObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            sp.OtherInsuranceCoverage = spOtherInsuranceCoverage;
            sp.Medical = spOtherMedicalCoverage;
            sp.Dental = spOtherDentalCoverage;
            sp.Vision = spOtherVisionCoverage;
            sp.Indemnity = spIndemnityCoverage;


            db.SaveChanges();
           
            int result = sp.FamilyMember_id;

            //redirection is not working
            //if (MaritalStatus == "MarriedwDep")
            //{
            //    RedirectToAction("DepEnrollment", "Family_info", new { sp.Employee_id, e.MaritalStatus  });
            //}

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SpOtherInsuranceNew(int Employee_id, int FamilyMember_id, string spOtherInsuranceCoverage, string spInsCarrier, 
            string spInsMailingAddress, string spInsCity, string spInsState, string spInsZipCode, string spInsPhoneNumber, string spInsPolicyNumber)
        {
            Other_Insurance other = new Other_Insurance();

            other.Employee_id = Employee_id;
            other.FamilyMember_id = FamilyMember_id;
            other.CoveredByOtherInsurance = spOtherInsuranceCoverage;
            other.InsuranceCarrier = spInsCarrier;
            other.MailingAddress = spInsMailingAddress;
            other.City = spInsCity;
            other.State = spInsState;
            other.ZipCode = spInsZipCode;
            other.PhoneNumber = spInsPhoneNumber;
            other.PolicyNumber = spInsPolicyNumber;

            ViewBag.Employee_id = other.Employee_id;
            ViewBag.FamilyMember_id = other.FamilyMember_id;

            int result = other.OtherInsurance_id;

            db.Other_Insurance.Add(other);
            db.SaveChanges();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //EditSp Method
        public ActionResult EditSpouse(int? FamilyMember_id, int? Employee_id, int? OtherInsurance_id, string MaritalStatus)
        {
            SpouseAndDependentInsVM spAndDepInsVM = new SpouseAndDependentInsVM();

            //ViewBag.MaritalStatus = spAndDepInsVM.employee.MaritalStatus;

            spAndDepInsVM.family = db.Family_Info.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);
            spAndDepInsVM.otherIns = db.Other_Insurance.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);


            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (f == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee_id = f.Employee_id;
            ViewBag.FamilyMember_id = f.FamilyMember_id;
            

            return View(spAndDepInsVM);
        }

        //EditUpdate-Spouse
        public JsonResult SpEditUpdate(int? Employee_id, int? FamilyMember_id, string RelationshipToInsured, string MaritalStatus, 
            string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress, string City2, 
            string State2, string ZipCode2, string EmailAddress, string PhoneNumber, string CellPhone, string Employer, 
            string EmployerAddress, string EmployerPObox, string EmployerCity, string EmployerState, string EmployerZipCode, 
            string EmployerPhoneNumber, string spOtherInsuranceCoverage, string spOtherMedicalCoverage, string spOtherDentalCoverage,
            string spOtherVisionCoverage, string spIndemnityCoverage)
        {
            var sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.RelationshipToInsured == "Spouse")
                .Single();

            //int record = (from fi in db.Family_Info
            //              where fi.FamilyMember_id == FamilyMember_id
            //              where fi.RelationshipToInsured == "Spouse"
            //              select fi).Count();

            //if (record > 0)
            //{
            //    ViewBag.Message = "Record already exists.";
            //    RedirectToAction("FamilyOverview", new { sp.Employee_id });
            //}
            //else
            //{
            //    RedirectToAction("DepEnrollment", new { sp.Employee_id });

            //}

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.MaritalStatus = MaritalStatus;
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

            sp.OtherInsuranceCoverage = spOtherInsuranceCoverage;
            sp.Medical = spOtherMedicalCoverage;
            sp.Dental = spOtherDentalCoverage;
            sp.Vision = spOtherVisionCoverage;
            sp.Indemnity = spIndemnityCoverage;


            ViewBag.FamilyMember_id = sp.FamilyMember_id;
            ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;
            ViewBag.RelationshipToInsured = "Spouse";

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

            if (ModelState.IsValid)
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("FamilyOverview", new { sp.Employee_id });
            }

            int result = sp.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SpOtherInsuranceUpdate(int? Employee_id, int? FamilyMember_id, int? OtherInsurance_id, string spCoveredByOtherInsurance,
            string spInsCarrier, string spInsMailingAddress, string spInsCity, string spInsState, string spInsZipCode, string spInsPhoneNumber,
            string spInsPolicyNumber)
        {
            var other = db.Other_Insurance
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            ViewBag.FamilyMember_id = other.FamilyMember_id;
            ViewBag.RelationshipToInsured = "Spouse";

            //other.Employee_id = Employee_id;
            //other.FamilyMember_id = FamilyMember_id;
            other.CoveredByOtherInsurance = spCoveredByOtherInsurance;
            other.InsuranceCarrier = spInsCarrier;
            other.InsuranceCarrier = spInsMailingAddress;
            other.InsuranceCarrier = spInsCity;
            other.InsuranceCarrier = spInsState;
            other.InsuranceCarrier = spInsZipCode;
            other.InsuranceCarrier = spInsPhoneNumber;
            other.InsuranceCarrier = spInsPolicyNumber;

            if (ModelState.IsValid)
            {
                db.Entry(other).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("FamilyOverview", new { other.Employee_id });
            }

            int result = other.OtherInsurance_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
   

        //Get-SpDetail
        public ActionResult SpouseDetail(int? Employee_id, int? FamilyMember_id, string MaritalStatus)
        {
            //ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            SpouseAndDependentInsVM spAndDepInsVM = new SpouseAndDependentInsVM();

            spAndDepInsVM.family = db.Family_Info.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);
            spAndDepInsVM.otherIns = db.Other_Insurance.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.FamilyMember_id = spAndDepInsVM.family.FamilyMember_id;
            ViewBag.Employee_id = spAndDepInsVM.family.Employee_id;
            ViewBag.RelationshipToInsured = spAndDepInsVM.family.RelationshipToInsured;

            return View(spAndDepInsVM);
        }

        //Get-SpDetail
        //public JsonResult GetSpDetail(int? FamilyMember_id)
        //{
        //    var sp = db.Family_Info
        //         .Where(i => i.FamilyMember_id == FamilyMember_id)
        //         .Single();

        //    ViewBag.FamilyMember_id = sp.FamilyMember_id;

        //    int result = sp.FamilyMember_id;

        //    return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        //}
        //----------------------------------------------------------------------------------------

        //DeleteSp Method
        public ActionResult DeleteSp(int? Employee_id, int? FamilyMember_id)
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
            ViewBag.sp = sp.FamilyMember_id;
            return View(sp);
        }
        
        //DeleteSp Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteSp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? FamilyMember_id)
        {
            Family_Info sp = db.Family_Info.Find(FamilyMember_id);

            Other_Insurance other = db.Other_Insurance.Find(FamilyMember_id);

            db.DeleteEmployeeAndDependents(FamilyMember_id);

            db.Family_Info.Remove(sp);
            db.Other_Insurance.Remove(other);
            db.SaveChanges();

            return RedirectToAction("FamilyOverview", new { sp.Employee_id });
        }

      
        //----------------------------------------------------------------------------------------

        public ActionResult AddDependent(int Employee_id, int? FamilyMember_id, string RelationshipToInsured)
        {

            SpouseAndDependentInsVM spAndDepInsVM = new SpouseAndDependentInsVM();

            spAndDepInsVM.family = db.Family_Info.FirstOrDefault(i => i.Employee_id == Employee_id);
            spAndDepInsVM.otherIns = db.Other_Insurance.FirstOrDefault(i => i.Employee_id == Employee_id);

            ViewBag.Employee_id = Employee_id;
            ViewBag.FamilyMember_id = FamilyMember_id;
            ViewBag.RelationshipToInsured = RelationshipToInsured = "Dependent";

            return View();
        }

        //Create-DepEnrollment
        public JsonResult DepEnrollmentNew(int Employee_id, string RelationshipToInsured, string SSN, string DepFirstName,
            string DepLastName, DateTime DateOfBirth, string Gender)
        {
            Family_Info dep = new Family_Info();

            dep.Employee_id = Employee_id;
            dep.RelationshipToInsured = RelationshipToInsured;
            dep.SSN = SSN;
            dep.FirstName = DepFirstName;
            dep.LastName = DepLastName;
            dep.DateOfBirth = DateOfBirth;
            dep.Gender = Gender;

            db.Family_Info.Add(dep);
            db.SaveChanges();

            int result = dep.FamilyMember_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DepOtherInsuranceNew(int Employee_id, int FamilyMember_id, string depCoveredByOtherInsurance,
           string depInsCarrier, string depInsPolicyNumber, string depInsPhoneNumber)
        {
            Other_Insurance other = new Other_Insurance();

            other.FamilyMember_id = FamilyMember_id;
            other.Employee_id = Employee_id;
            other.CoveredByOtherInsurance = depCoveredByOtherInsurance;
            other.InsuranceCarrier = depInsCarrier;
            other.PolicyNumber = depInsPolicyNumber;
            other.PhoneNumber = depInsPhoneNumber;

            db.Other_Insurance.Add(other);
            db.SaveChanges();

            int result = other.OtherInsurance_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //EditDep Method
        public ActionResult EditDependent(int? Employee_id, int? FamilyMember_id)
        {
            SpouseAndDependentInsVM spAndDepInsVM = new SpouseAndDependentInsVM();

            spAndDepInsVM.family = db.Family_Info.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);
            spAndDepInsVM.otherIns = db.Other_Insurance.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (f == null)
            {
                return HttpNotFound();
            }

            ViewBag.FamilyMember_id = spAndDepInsVM.family.FamilyMember_id;
            ViewBag.Employee_id = spAndDepInsVM.family.Employee_id;


            return View(spAndDepInsVM);
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
        public ActionResult DependentDetail(int? Employee_id, int? FamilyMember_id, string MaritalStatus, string RelationshipToInsured)
        {
            //ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");
            SpouseAndDependentInsVM spAndDepInsVM = new SpouseAndDependentInsVM();

            spAndDepInsVM.family = db.Family_Info.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);
            spAndDepInsVM.otherIns = db.Other_Insurance.FirstOrDefault(i => i.FamilyMember_id == FamilyMember_id);

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.FamilyMember_id = f.FamilyMember_id;
            ViewBag.Employee_id = f.Employee_id;
            ViewBag.RelationshipToInsured = f.RelationshipToInsured;

            return View(spAndDepInsVM);
        }

        //Get-DepDetail
        public JsonResult GetDepDetail(int FamilyMember_id)
        {
            var sp = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            ViewBag.FamilyMember_id = sp.FamilyMember_id;

            int result = sp.FamilyMember_id;

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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //----------------------------------------------------------------------------------------
    }
}
