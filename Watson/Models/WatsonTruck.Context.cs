﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WatsonTruckEntities : DbContext
    {
        public WatsonTruckEntities()
            : base("name=WatsonTruckEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<Deduction> Deductions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Family_Info> Family_Info { get; set; }
        public virtual DbSet<Group_Health> Group_Health { get; set; }
        public virtual DbSet<InsurancePlan> InsurancePlans { get; set; }
        public virtual DbSet<InsurancePlanDetail> InsurancePlanDetails { get; set; }
        public virtual DbSet<InsurancePremium> InsurancePremiums { get; set; }
        public virtual DbSet<Life_Insurance> Life_Insurance { get; set; }
        public virtual DbSet<Other_Insurance> Other_Insurance { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<JobApplicant> JobApplicants { get; set; }
    
        public virtual int DeleteEmployeeAndDependents(Nullable<int> empid)
        {
            var empidParameter = empid.HasValue ?
                new ObjectParameter("empid", empid) :
                new ObjectParameter("empid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteEmployeeAndDependents", empidParameter);
        }
    }
}
