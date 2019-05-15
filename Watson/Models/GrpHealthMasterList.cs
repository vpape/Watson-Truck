using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watson.Models
{
    public class GrpHealthMasterList
    {

        //groupHealth info table
        public int GroupHealthInsurance_id { get; set; }
        public int Employee_id { get; set; }
        public int FamilyMember_id { get; set; }
        public int OtherInsurance_id { get; set; }
        public string InsuranceCarrier { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupName { get; set; }
        public string IMSGroupNumber { get; set; }
        public string PhoneNumber { get; set; }
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
        public virtual Employee Employee { get; set; }

        //Employee info table
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


        //family_info table- redundant info commented out--added sp to differentiate :
        public string spRelationshipToInsured { get; set; }
        public string spMaritalStatus { get; set; }
        public string spFirstName { get; set; }
        public string spMiddleName { get; set; }
        public string spLastName { get; set; }
        public string spSSN { get; set; }
        public Nullable<System.DateTime> spDateOfBirth { get; set; }
        public string spMailingAddress { get; set; }
        public string spPObox { get; set; }
        public string spPhysicalAddress { get; set; }
        public string spCity { get; set; }
        public string spState { get; set; }
        public string spZipCode { get; set; }
        public string spEmailAddress { get; set; }
        public string spPhoneNumber { get; set; }
        public string spCellPhone { get; set; }
        public string spCounty { get; set; }
        public string spGender { get; set; }
        public string spEmployer { get; set; }
        public string spEmployerMailingAddress { get; set; }
        public string spEmployerPObox { get; set; }
        public string spEmployerCity { get; set; }
        public string spEmployerState { get; set; }
        public string spEmployerZipCode { get; set; }
        public string spEmployerPhoneNumber { get; set; }
        
        //family_info table
        public string depRelationshipToInsured { get; set; }
        public string depFirstName { get; set; }
        public string depMiddleName { get; set; }
        public string depLastName { get; set; }
        public string depSSN { get; set; }
        public Nullable<System.DateTime> depDateOfBirth { get; set; }
        public string depGender { get; set; }

        //other insurance table
        //public int FamilyMember_id { get; set; }
        //public int OtherInsurance_id { get; set; }
        //public int Employee_id { get; set; }
        public string InsPolicyNumber { get; set; }
        public string InsMailingAddress { get; set; }
        public string InsCity { get; set; }
        public string InsState { get; set; }
        public string InsZipCode { get; set; }
        public string InsPhoneNumber { get; set; }
        public string CoveredByOtherInsurance { get; set; }
        public string InsPObox { get; set; }
        public string InsCarrier { get; set; }

        //add to db.OtherInsurance and delete from db.FamilyInfo
        public string spMedical { get; set; }
        public string spDental { get; set; }
        public string spVision { get; set; }
        public string spIndemnity { get; set; }
    }
}