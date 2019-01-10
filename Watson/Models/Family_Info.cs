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
    
    public partial class Family_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Family_Info()
        {
            this.Other_Insurance = new HashSet<Other_Insurance>();
        }
    
        public int FamilyMember_id { get; set; }
        public Nullable<int> OtherInsurance_id { get; set; }
        public int Employee_id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string MailingAddress { get; set; }
        public string PhysicalAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string County { get; set; }
        public string Gender { get; set; }
        public string Employer { get; set; }
        public string RelationshipToInsured { get; set; }
        public string EmployerMailingAddress { get; set; }
        public string EmployerCity { get; set; }
        public string EmployerState { get; set; }
        public string EmployerZipCode { get; set; }
        public string EmployerPhoneNumber { get; set; }
        public Nullable<bool> Medical { get; set; }
        public Nullable<bool> Dental { get; set; }
        public Nullable<bool> Vision { get; set; }
        public Nullable<bool> Indemnity { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Other_Insurance> Other_Insurance { get; set; }
    }
}
