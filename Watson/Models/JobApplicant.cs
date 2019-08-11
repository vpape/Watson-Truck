//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Watson.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobApplicant
    {
        public int JobApplicant_id { get; set; }
        public int Employee_id { get; set; }
        public string PositionApplyingFor { get; set; }
        public Nullable<System.DateTime> StartDateAvailability { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string ApplicantSignature { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public string ApplicantSSN { get; set; }
        public string ApplicantPresentAddress { get; set; }
        public string ApplicantPresentPObox { get; set; }
        public string ApplicantCity { get; set; }
        public string ApplicantState { get; set; }
        public string ApplicantZipCode { get; set; }
        public string YearsResidedAtPresentAddr { get; set; }
        public string ApplicantPreviouseAddress { get; set; }
        public string ApplicantPreviousPObox { get; set; }
        public string ApplicantCityTwo { get; set; }
        public string ApplicantStateTwo { get; set; }
        public string ApplicantZipCodeTwo { get; set; }
        public string ApplicantPhoneNumber { get; set; }
        public string ApplicantAge { get; set; }
        public string PreviouslyEmployedAtWatson { get; set; }
        public Nullable<System.DateTime> EmploymentDatesAtWatson { get; set; }
        public string DriversLicense { get; set; }
        public string DriversLicenseState { get; set; }
        public string DriversLicenseNumber { get; set; }
        public Nullable<System.DateTime> DriversLicenseExpirationDate { get; set; }
        public string InstitutionName { get; set; }
        public string YearsCompleted { get; set; }
        public string FieldOfStudy { get; set; }
        public string GraduateOrDegree { get; set; }
        public string Veteran { get; set; }
        public string MOSorSpecializedTraining { get; set; }
        public string PresentOrCurrentEmployer { get; set; }
        public string EmployerPhoneNumber { get; set; }
        public string EmployerAddress { get; set; }
        public string EmployerPObox { get; set; }
        public string EmployerCity { get; set; }
        public string EmployerState { get; set; }
        public string EmployerZipCode { get; set; }
        public Nullable<System.DateTime> EmployedFromDate { get; set; }
        public Nullable<System.DateTime> EmployedToDate { get; set; }
        public string TitleOfPositionHeld { get; set; }
        public string TypeOfWorkedPerformed { get; set; }
        public string NameOfSupervisor { get; set; }
        public string TerminatedOrResiged { get; set; }
        public string ReasonForTerminationOrResignation { get; set; }
        public string ReasonForEmploymentGaps { get; set; }
        public string ReferenceFirstName { get; set; }
        public string ReferenceLastName { get; set; }
        public string ReferencePhoneNumber { get; set; }
        public string ReferenceAddress { get; set; }
        public string ReferencePObox { get; set; }
        public string ReferenceCity { get; set; }
        public string ReferenceState { get; set; }
        public string ReferenceZipCode { get; set; }
        public string ReferenceOccupation { get; set; }
        public string YearsKnownReference { get; set; }
        public string Experience { get; set; }
        public string ListOtherExperience { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
