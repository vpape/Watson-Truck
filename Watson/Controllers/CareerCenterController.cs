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
    //Need to add JobApplicant code to Admin Controller and View
    public class CareerCenterController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();
        private static List<Employee> employee = new List<Employee>();
        private static List<JobApplicant> applicant = new List<JobApplicant>();

        // GET: CareerCenter
        public ActionResult Index(int? JobApplicant_id)
        {
           
                ViewBag.JobApplicant_id = JobApplicant_id;

                if (JobApplicant_id == null)
                {
                    //return View(db.Employees.Find(Employee_id));
                    return View(db.JobApplicants.ToList());
                }
                else
                {
                    return View(db.JobApplicants.Find(JobApplicant_id));
                }
            
        }

        // GET: CareerCenter/JobApplicantDetails/id
        public ActionResult JobApplicantDetails(int? id)
        {
            return View();
        }

        // GET: Create
        public ActionResult CreateJobApplicant()
        {
            return View();
        }

        // POST: Create
        [System.Web.Mvc.HttpPost]
        public JsonResult CreateJobApplicant(int JobApplicant_id, int Employee_id, string PositionApplyingFor, DateTime StartDateAvailability,
            DateTime Date, string ApplicantSignature, string ApplicantFirstName, string ApplicantLastName, string ApplicantSSN,
            string ApplicantPresentAddress, string ApplicantPresentPObox, string ApplicantCity, string ApplicantState, string ApplicantZipCode,
            string YearsResidedAtPresentAddress, string ApplicantPreviouseAddress, string ApplicantPreviousPObox, string ApplicantCityTwo,
            string ApplicantStateTwo, string ApplicantZipCodeTwo, string ApplicantPhoneNumber, string ApplicantAge, string PreviouslyEmployedAtWatson,
            DateTime EmploymentDatesAtWatson, string DriversLicense, string DriversLicenseState, string DriversLicenseNumber,
            DateTime DriversLicenseExpirationDate, string InstitutionName, string YearsCompleted, string FieldOfStudy, string GraduateOrDegree, 
            string Veteran, string MOSorSpecializedTraining, string PresentOrCurrentEmployer, string EmployerPhoneNumber, string EmployerAddress,
            string EmployerPObox, string EmployerCity, string EmployerState, string EmployerZipCode, DateTime EmployedFromDate,
            DateTime EmployedToDate, string TitleOfPositionHeld, string TypeOfWorkedPerformed, string NameOfSupervisor, string TerminatedOrResiged,
            string ReasonForTerminationOrResignation, string ReasonForEmploymentGaps, string ReferenceFirstName, string ReferenceLastName,
            string ReferencePhoneNumber, string ReferenceAddress, string ReferencePObox, string ReferenceCity, string ReferenceState,
            string ReferenceZipCode, string ReferenceOccupation, string YearsKnownReference, string Experience, string ListOtherExperience)
        {
            JobApplicant a = new JobApplicant();

            a.JobApplicant_id = JobApplicant_id;
            a.Employee_id = Employee_id;
            a.PositionApplyingFor = PositionApplyingFor;
            a.StartDateAvailability = StartDateAvailability;
            a.Date = Date;
            a.ApplicantSignature = ApplicantSignature;
            a.ApplicantFirstName = ApplicantFirstName;
            a.ApplicantLastName = ApplicantLastName;
            a.ApplicantSSN = ApplicantSSN;
            a.ApplicantPresentAddress = ApplicantPresentAddress;
            a.ApplicantPresentPObox = ApplicantPresentPObox;
            a.ApplicantCity = ApplicantCity;
            a.ApplicantState = ApplicantState;
            a.ApplicantZipCode = ApplicantZipCode;
            a.YearsResidedAtPresentAddr = YearsResidedAtPresentAddress;
            a.ApplicantPreviouseAddress = ApplicantPreviouseAddress;
            a.ApplicantPreviousPObox = ApplicantPreviousPObox;
            a.ApplicantCityTwo = ApplicantCityTwo;
            a.ApplicantStateTwo = ApplicantStateTwo;
            a.ApplicantZipCodeTwo = ApplicantZipCodeTwo;
            a.ApplicantPhoneNumber = ApplicantPhoneNumber;
            a.ApplicantAge = ApplicantAge;
            a.PreviouslyEmployedAtWatson = PreviouslyEmployedAtWatson;
            a.EmploymentDatesAtWatson = EmploymentDatesAtWatson;
            a.DriversLicense = DriversLicense;
            a.DriversLicenseState = DriversLicenseState;
            a.DriversLicenseNumber = DriversLicenseNumber;
            a.DriversLicenseExpirationDate = DriversLicenseExpirationDate;
            a.InstitutionName = InstitutionName;
            a.YearsCompleted = YearsCompleted;
            a.FieldOfStudy = FieldOfStudy;
            a.GraduateOrDegree = GraduateOrDegree;
            a.Veteran = Veteran;
            a.MOSorSpecializedTraining = MOSorSpecializedTraining;
            a.PresentOrCurrentEmployer = PresentOrCurrentEmployer;
            a.EmployerPhoneNumber = EmployerPhoneNumber;
            a.EmployerAddress = EmployerAddress;
            a.EmployerPObox = EmployerPObox;
            a.EmployerCity = EmployerCity;
            a.EmployerState = EmployerState;
            a.EmployerZipCode = EmployerZipCode;
            a.EmployedFromDate = EmployedFromDate;
            a.EmployedToDate = EmployedToDate;
            a.TitleOfPositionHeld = TitleOfPositionHeld;
            a.TypeOfWorkedPerformed = TypeOfWorkedPerformed;
            a.NameOfSupervisor = NameOfSupervisor;
            a.TerminatedOrResiged = TerminatedOrResiged;
            a.ReasonForTerminationOrResignation = ReasonForTerminationOrResignation;
            a.ReasonForEmploymentGaps = ReasonForEmploymentGaps;
            a.ReferenceFirstName = ReferenceFirstName;
            a.ReferenceLastName = ReferenceLastName;
            a.ReferencePhoneNumber = ReferencePhoneNumber;
            a.ReferenceAddress = ReferenceAddress;
            a.ReferencePObox = ReferencePObox;
            a.ReferenceCity = ReferenceCity;
            a.ReferenceState = ReferenceState;
            a.ReferenceZipCode = ReferenceZipCode;
            a.ReferenceOccupation = ReferenceOccupation;
            a.YearsKnownReference = YearsKnownReference;
            a.Experience = Experience;
            a.ListOtherExperience = ListOtherExperience;

            ViewBag.JobApplicant_id = a.JobApplicant_id;

            db.JobApplicants.Add(a);
            db.SaveChanges();

            int result = a.JobApplicant_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        // GET: CareerCenter/EditJobApplicant/id
        public ActionResult EditJobApplicant(int? id)
        {
            return View();
        }

        // POST: CareerCenter/EditJobApplicant/id
        [System.Web.Mvc.HttpPost]
        public ActionResult EditJobApplicant(FormCollection collection)
        {
            return View();
        }


        // GET: CareerCenter/DeleteJobApplicant/id
        public ActionResult DeleteJobApplicant(int? id)
        {
            return View();
        }

        // POST: CareerCenter/DeleteJobApplicant/id
        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteJobApplicant(FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}