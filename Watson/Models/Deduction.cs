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
    
    public partial class Deduction
    {
        public int Deductions_id { get; set; }
        public int Employee_id { get; set; }
        public string Coverage { get; set; }
        public string Provider { get; set; }
        public string EEelectionPreTax { get; set; }
        public decimal PremiumPreTax { get; set; }
        public string EEelectionPostTax { get; set; }
        public decimal PremiumPostTax { get; set; }
        public decimal TotalPreTax { get; set; }
        public decimal TotalPostTax { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
