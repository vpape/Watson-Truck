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
        public Employee employee { get; set; }
        public List<Family_Info> family { get; set; }
        public Group_Health grpHealth { get; set; }
        public List<Other_Insurance> otherIns { get; set; }
        public Deduction deduction { get; set; }

    }
}