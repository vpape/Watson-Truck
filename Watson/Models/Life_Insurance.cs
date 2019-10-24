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
    
    public partial class Life_Insurance
    {
        public int LifeInsurance_id { get; set; }
        public int Employee_id { get; set; }
        public string GroupPlanNumber { get; set; }
        public Nullable<System.DateTime> BenefitsEffectiveDate { get; set; }
        public string InitialEnrollment { get; set; }
        public string ReEnrollment { get; set; }
        public string AddEmployeeAndDependents { get; set; }
        public string DropRefuseCoverage { get; set; }
        public string InformationChange { get; set; }
        public string IncreaseAmount { get; set; }
        public string FamilyStatusChange { get; set; }
        public string MarriedOrHaveSpouse { get; set; }
        public string HaveChildrenOrHaveDependents { get; set; }
        public Nullable<System.DateTime> DateOfMarriage { get; set; }
        public Nullable<System.DateTime> PlacementDateOfAdoptedChild { get; set; }
        public string AddDependent { get; set; }
        public string DropDependent { get; set; }
        public string Student { get; set; }
        public string Disabled { get; set; }
        public string NonStandardDependent { get; set; }
        public string DropEmployee { get; set; }
        public string DropSpouse { get; set; }
        public string DropDependents { get; set; }
        public Nullable<System.DateTime> LastDayOfCoverage { get; set; }
        public string Retirement { get; set; }
        public Nullable<System.DateTime> LastDayWorked { get; set; }
        public string OtherEvent { get; set; }
        public string OtherEventReason { get; set; }
        public Nullable<System.DateTime> OtherEventDate { get; set; }
        public string EmployeeDentalDrop { get; set; }
        public string SpouseDentalDrop { get; set; }
        public string DependentDentalDrop { get; set; }
        public string EmployeeVisionDrop { get; set; }
        public string SpouseVisionDrop { get; set; }
        public string DependentVisionDrop { get; set; }
        public string DropBasicLife { get; set; }
        public string DropDental { get; set; }
        public string DropVision { get; set; }
        public string Divorce { get; set; }
        public Nullable<System.DateTime> DivorceDate { get; set; }
        public string DeathOfSpouse { get; set; }
        public Nullable<System.DateTime> DeathOfSpouseDate { get; set; }
        public string TerminationOrExpirationOfCoverage { get; set; }
        public System.DateTime TerminationOrExpirationOfCoverageDate { get; set; }
        public string DentalCoverageLost { get; set; }
        public string VisionCoverageLost { get; set; }
        public string CoveredUnderOtherInsurance { get; set; }
        public string DoNotWantDentalCoverage { get; set; }
        public string EmployeeCoveredUnderOtherDentalPlan { get; set; }
        public string SpouseCoveredUnderOtherDentalPlan { get; set; }
        public string DependentsCoveredUnderOtherDentalPlan { get; set; }
        public string DoNotWantVisionCoverage { get; set; }
        public string EmployeeCoveredUnderOtherVisionPlan { get; set; }
        public string SpouseCoveredUnderOtherVisionPlan { get; set; }
        public string DependentsCoveredUnderOtherVisionPlan { get; set; }
        public string OwnerBasicLifeWithADandDPolicyAmount { get; set; }
        public string ManagerBasicLifeWithADandDPolicyAmount { get; set; }
        public string EmployeeBasicLifeWithADandDPolicyAmount { get; set; }
        public string SpouseBasicLifeWithADandDPolicyAmount { get; set; }
        public string DoNotWantBasicLifeCoverageWithADandD { get; set; }
        public string AmountOfPreviousPolicy { get; set; }
        public string EmployeeSignature { get; set; }
        public Nullable<System.DateTime> EmployeeSignatureDate { get; set; }
        public string SubTotalCode { get; set; }
        public string TerminationEmploymentOfDropCoverage { get; set; }
        public Nullable<System.DateTime> TerminationEmploymentDateOfDropCoverage { get; set; }
        public string TerminationEmploymentLossOfOtherCoverage { get; set; }
        public Nullable<System.DateTime> TerminationEmploymentDateLossOfOtherCoverage { get; set; }
        public string Other { get; set; }
        public string OtherReason { get; set; }
        public string EmployeeOnlyDental { get; set; }
        public string EEAndSpouseDental { get; set; }
        public string EEAndDependentsDental { get; set; }
        public string EEAndFamilyDental { get; set; }
        public string EmployeeOnlyVision { get; set; }
        public string EEAndSpouseVision { get; set; }
        public string EEAndDependentsVision { get; set; }
        public string EEAndFamilyVision { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
