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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListJobs()
        {
            return View();
        }

        public ActionResult JobDescription()
        {
            return View();
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
        public JsonResult JobApplication(int JobApplicant_id, int Employee_id, string PositionApplyingFor, DateTime StartDateAvailability,
            string ApplicantSignature, string ApplicantSignatureDate, string FirstName, string LastName, string Address, string PObox,
            string City, string State, string ZipCode, string YearsResidedAtPresentAddress, string PreviousAddress, string PreviousPObox,
            string PreviousCity, string PreviousState, string PreviousZipCode, string YearsResidedAtPreviousAddr, string PhoneNumber, string OverEighteen,
            string PreviouslyEmployedAtWatson, DateTime EmploymentDatesAtWatsonFrom, DateTime EmploymentDatesAtWatsonTo, string DriversLicense,
            string LicenseState, string LicenseNumber, DateTime LicenseExpirationDate, string ElementaryInstitution, string YearsCompletedElementary,
            string FieldOfStudyElementary, string GraduateElementary, string HighSchoolInstitution, string YearsCompletedHighSchool,
            string FieldOfStudyHighSchool, string GraduateHighSchool, string CollegeInstitution, string YearsCompletedCollege,
            string FieldOfStudyCollege, string GraduateOrDegreeCollege, string TechnicalInstitution, string YearsCompletedTechnical,
            string FieldOfStudyTechnical, string GraduateOrDegreeTechnical, string AdditionalInstitution, string YearsCompletedAtAdditional,
            string FieldOfStudyAtAdditional, string GraduateOrDegreeAtAdditional, string Veteran, string MOSorSpecializedTraining,
            string PresentOrLastEmployer, string EmployerPhoneNumber, string EmployerAddress, string EmployerCity, string EmployerState,
            string EmployerZipCode, DateTime EmployedFromDate, DateTime EmployedToDate, string TitleOfPositionHeld, string TypeOfWorkedPerformed,
            string NameOfSupervisor, string PresentOrLastEmployerTwo, string EmployerPhoneNumberTwo, string EmployerAddressTwo,
            string EmployerCityTwo, string EmployerStateTwo, string EmployerZipCodeTwo, DateTime EmployedFromDateTwo, DateTime EmployedToDateTwo,
            string TitleOfPositionHeldTwo, string TypeOfWorkedPerformedTwo, string NameOfSupervisorTwo, string PresentOrLastEmployerThree,
            string EmployerPhoneNumberThree, string EmployerAddressThree, string EmployerCityThree, string EmployerStateThree,
            string EmployerZipCodeThree, DateTime EmployedFromDateThree, DateTime EmployedToDateThree, string TitleOfPositionHeldThree,
            string TypeOfWorkedPerformedThree, string NameOfSupervisorThree, string TerminatedOrResigned, string ReasonForTerminationOrResignation,
            string ReasonForEmploymentGaps, string ReferenceFirstName, string ReferenceLastName, string ReferencePhoneNumber,
            string ReferenceAddress, string ReferenceCity, string ReferenceState, string ReferenceZipCode, string ReferenceOccupation,
            string YearsKnownReference, string ReferenceFirstNameTwo, string ReferenceLastNameTwo, string ReferenceAddressTwo,
            string ReferenceCityTwo, string ReferenceStateTwo, string ReferenceZipCodeTwo, string ReferenceOccupationTwo, string YearsKnownReferenceTwo,
            string ReferencePhoneNumberTwo, string ReferenceFirstNameThree, string ReferenceLastNameThree, string ReferenceAddressThree,
            string ReferenceCityThree, string ReferenceStateThree, string ReferenceZipCodeThree, string ReferenceOccupationThree,
            string YearsKnownReferenceThree, string ReferencePhoneNumberThree, string Experience, string ListOtherExperience, string Certification,
            string ApplicantSignatureTwo, DateTime ApplicantSignatureDateTwo)
        {
            JobApplicant a = new JobApplicant();

            a.JobApplicant_id = JobApplicant_id;
            a.Employee_id = Employee_id;
            a.PositionApplyingFor = PositionApplyingFor;
            a.StartDateAvailability = StartDateAvailability;
            a.ApplicantSignature = ApplicantSignature;
            a.FirstName = FirstName;
            a.LastName = LastName;
            a.Address = Address;
            a.City = City;
            a.State = State;
            a.ZipCode = ZipCode;
            a.YearsResidedAtPresentAddr = YearsResidedAtPresentAddress;
            a.PreviousAddress = PreviousAddress;
            a.PreviousCity = PreviousCity;
            a.PreviousState = PreviousState;
            a.PreviousZipCode = PreviousZipCode;
            a.YearsResidedAtPreviousAddr = YearsResidedAtPreviousAddr;
            a.PhoneNumber = PhoneNumber;
            //a.Age = Age;
            a.OverEighteen = OverEighteen;
            a.PreviouslyEmployedAtWatson = PreviouslyEmployedAtWatson;
            a.EmploymentDatesAtWatsonFrom = EmploymentDatesAtWatsonFrom;
            a.EmploymentDatesAtWatsonTo = EmploymentDatesAtWatsonTo;
            a.DriversLicense = DriversLicense;
            a.LicenseState = LicenseState;
            a.LicenseNumber = LicenseNumber;
            a.LicenseExpirationDate = LicenseExpirationDate;
            a.ElementaryInstitution = ElementaryInstitution;
            a.YearsCompletedElementary = YearsCompletedElementary;
            a.FieldOfStudyElementary = FieldOfStudyElementary;
            a.GraduateElementary = GraduateElementary;
            a.HighSchoolInstitution = HighSchoolInstitution;
            a.YearsCompletedHighSchool = YearsCompletedHighSchool;
            a.FieldOfStudyHighSchool = FieldOfStudyHighSchool;
            a.GraduateHighSchool = GraduateHighSchool;
            a.CollegeInstitution = CollegeInstitution;
            a.YearsCompletedCollege = YearsCompletedCollege;
            a.FieldOfStudyCollege = FieldOfStudyCollege;
            a.GraduateOrDegreeCollege = GraduateOrDegreeCollege;
            a.TechnicalInstitution = TechnicalInstitution;
            a.YearsCompletedTechnical = YearsCompletedTechnical;
            a.FieldOfStudyTechnical = FieldOfStudyTechnical;
            a.GraduateOrDegreeTechnical = GraduateOrDegreeTechnical;
            a.AdditionalInstitution = AdditionalInstitution;
            a.YearsCompletedAtAdditional = YearsCompletedAtAdditional;
            a.FieldOfStudyAtAdditional = FieldOfStudyAtAdditional;
            a.GraduateOrDegreeAtAdditional = GraduateOrDegreeAtAdditional;
            a.Veteran = Veteran;
            a.MOSorSpecializedTraining = MOSorSpecializedTraining;
            a.PresentOrLastEmployer = PresentOrLastEmployer;
            a.EmployerPhoneNumber = EmployerPhoneNumber;
            a.EmployerAddress = EmployerAddress;
            a.EmployerCity = EmployerCity;
            a.EmployerState = EmployerState;
            a.EmployerZipCode = EmployerZipCode;
            a.EmployedFromDate = EmployedFromDate;
            a.EmployedToDate = EmployedToDate;
            a.TitleOfPositionHeld = TitleOfPositionHeld;
            a.TypeOfWorkedPerformed = TypeOfWorkedPerformed;
            a.NameOfSupervisor = NameOfSupervisor;
            a.PresentOrLastEmployerTwo = PresentOrLastEmployerTwo;
            a.EmployerPhoneNumberTwo = EmployerPhoneNumberTwo;
            a.EmployerAddressTwo = EmployerAddressTwo;
            a.EmployerCityTwo = EmployerCityTwo;
            a.EmployerStateTwo = EmployerStateTwo;
            a.EmployerZipCodeTwo = EmployerZipCodeTwo;
            a.EmployedFromDateTwo = EmployedFromDateTwo;
            a.EmployedToDateTwo = EmployedToDateTwo;
            a.TitleOfPositionHeldTwo = TitleOfPositionHeldTwo;
            a.TypeOfWorkedPerformedTwo = TypeOfWorkedPerformedTwo;
            a.NameOfSupervisorTwo = NameOfSupervisorTwo;
            a.PresentOrLastEmployerThree = PresentOrLastEmployerThree;
            a.EmployerPhoneNumberThree = EmployerPhoneNumberThree;
            a.EmployerAddressThree = EmployerAddressThree;
            a.EmployerCityThree = EmployerCityThree;
            a.EmployerStateThree = EmployerStateThree;
            a.EmployerZipCodeThree = EmployerZipCodeThree;
            a.EmployedFromDateThree = EmployedFromDateThree;
            a.EmployedToDateThree = EmployedToDateThree;
            a.TitleOfPositionHeldThree = TitleOfPositionHeldThree;
            a.TypeOfWorkedPerformedThree = TypeOfWorkedPerformedThree;
            a.NameOfSupervisorThree = NameOfSupervisorThree;
            a.TerminatedOrResigned = TerminatedOrResigned;
            a.ReasonForTerminationOrResignation = ReasonForTerminationOrResignation;
            a.ReasonForEmploymentGaps = ReasonForEmploymentGaps;
            a.ReferenceFirstName = ReferenceFirstName;
            a.ReferenceLastName = ReferenceLastName;
            a.ReferencePhoneNumber = ReferencePhoneNumber;
            a.ReferenceAddress = ReferenceAddress;
            a.ReferenceCity = ReferenceCity;
            a.ReferenceState = ReferenceState;
            a.ReferenceZipCode = ReferenceZipCode;
            a.ReferenceOccupation = ReferenceOccupation;
            a.YearsKnownReference = YearsKnownReference;
            a.ReferenceFirstNameTwo = ReferenceFirstNameTwo;
            a.ReferenceLastNameTwo = ReferenceLastNameTwo;
            a.ReferenceAddressTwo = ReferenceAddressTwo;
            a.ReferenceCityTwo = ReferenceCityTwo;
            a.ReferenceStateTwo = ReferenceStateTwo;
            a.ReferenceZipCodeTwo = ReferenceZipCodeTwo;
            a.ReferenceOccupationTwo = ReferenceOccupationTwo;
            a.YearsKnownReferenceTwo = YearsKnownReferenceTwo;
            a.ReferencePhoneNumberTwo = ReferencePhoneNumberTwo;
            a.ReferenceFirstNameThree = ReferenceFirstNameThree;
            a.ReferenceLastNameThree = ReferenceLastNameThree;
            a.ReferenceAddressThree = ReferenceAddressThree;
            a.ReferenceCityThree = ReferenceCityThree;
            a.ReferenceStateThree = ReferenceStateThree;
            a.ReferenceZipCodeThree = ReferenceZipCodeThree;
            a.ReferenceOccupationThree = ReferenceOccupationThree;
            a.YearsKnownReferenceThree = YearsKnownReferenceThree;
            a.ReferencePhoneNumberThree = ReferencePhoneNumberThree;
            a.Experience = Experience;
            a.ListOtherExperience = ListOtherExperience;
            a.Certification = Certification;
            a.ApplicantSignatureTwo = ApplicantSignatureTwo;
            a.ApplicantSignatureDateTwo = ApplicantSignatureDateTwo;

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