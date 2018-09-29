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
    
    public partial class Group_Health
    {
        public int GroupHealthInsurance_id { get; set; }
        public int User_id { get; set; }
        public string Provider { get; set; }
        public string InsuranceCoverageType { get; set; }
        public string EEelectionPre_tax { get; set; }
        public string PremiumPre_tax { get; set; }
        public string EEelectionPost_tax { get; set; }
        public string PremiumPost_tax { get; set; }
        public string TotalPre_tax { get; set; }
        public string TotalPost_tax { get; set; }
        public string InsuranceCarrier { get; set; }
        public bool GroupHealthOptions { get; set; }
        public bool DropSpouse { get; set; }
        public bool DropDependent { get; set; }
        public System.DateTime PlacementDateOfAdoptedChild { get; set; }
        public string CoverageRefusal { get; set; }
        public string PolicyNumber { get; set; }
        public string EmployeeSignature { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
