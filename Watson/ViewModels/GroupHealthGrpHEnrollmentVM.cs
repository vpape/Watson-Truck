using Watson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Watson.ViewModels
{
    public class GroupHealthGrpHEnrollmentVM
    {
        //Employee class
        public Employee employee { get; set; }
        public int Employee_id { get; set; }
        public string EmployeeRole { get; set; }
        public string CurrentEmployer { get; set; }
        public string PreviousEmployer { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string SSN { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string MailingAddress { get; set; }
        public string PhysicalAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string County { get; set; }
        public string CityLimits { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public string isActive { get; set; }
        public Nullable<System.DateTime> EligibilityDate { get; set; }
        public string WorkStatus { get; set; }
        public Nullable<int> HoursWorkedPerWeek { get; set; }
        public string JobTitle { get; set; }
        public Nullable<decimal> AnnualSalary { get; set; }
        public string Department { get; set; }
        public string EnrollmentType { get; set; }
        public Nullable<int> Payroll_id { get; set; }
        public string Class { get; set; }
        public string PObox { get; set; }
        public string CityTwo { get; set; }
        public string StateTwo { get; set; }
        public string ZipCodeTwo { get; set; }

        //Family_Info class
        public Family_Info family { get; set; }
        public int FamilyMember_id { get; set; }
        public string Employer { get; set; }
        public string RelationshipToInsured { get; set; }
        public string EmployerMailingAddress { get; set; }
        public string EmployerCity { get; set; }
        public string EmployerState { get; set; }
        public string EmployerZipCode { get; set; }
        public string EmployerPhoneNumber { get; set; }
        public string EmployerPObox { get; set; }

        //Group_Health class
        public Group_Health grpHealth { get; set; }
        public int GroupHealthInsurance_id { get; set; }
        public string InsuranceCarrier { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupName { get; set; }
        public string IMSGroupNumber { get; set; }
        public string ReasonForGrpCoverageRefusal { get; set; }
        public string OtherCoverage { get; set; }
        public string OtherReason { get; set; }
        public string Myself { get; set; }
        public string Spouse { get; set; }
        public string Dependent { get; set; }
        public string OtherInsuranceCoverage { get; set; }
        public Nullable<System.DateTime> CafeteriaPlanYear { get; set; }
        public string NoMedicalPlan { get; set; }
        public string EmployeeOnly { get; set; }
        public string EmployeeAndSpouse { get; set; }
        public string EmployeeAndDependent { get; set; }
        public string EmployeeAndFamily { get; set; }
        public string EmployeeSignature { get; set; }
        public Nullable<System.DateTime> EmployeeSignatureDate { get; set; }
        public string EmployeeInitials { get; set; }
        public string OtherSignature { get; set; }
        public Nullable<System.DateTime> OtherSignatureDate { get; set; }
        public string NameOfPersonToReleaseInfoTo { get; set; }
        public string Relationship { get; set; }

        //Other_Insurance class
        public Other_Insurance otherIns { get; set; }
        public int OtherInsurance_id { get; set; }
        public string CoveredByOtherInsurance { get; set; }
        //public string PObox { get; set; }
        //public string InsuranceCarrier { get; set; }
        public string Medical { get; set; }
        public string Dental { get; set; }
        public string Vision { get; set; }
        public string Indemnity { get; set; }

    }
}