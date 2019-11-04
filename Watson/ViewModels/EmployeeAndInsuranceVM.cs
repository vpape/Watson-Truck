﻿using Watson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Watson.ViewModels
{
    public class EmployeeAndInsuranceVM
    {
        public Employee employee { get; set; }
        public Group_Health grpHealth { get; set; }
        public Life_Insurance lifeIns { get; set; }
        public Family_Info spouse { get; set; }
        public List<Family_Info> family { get; set; }
        public List<Beneficiary> beneficiaryInfo { get; set; }
    }
}