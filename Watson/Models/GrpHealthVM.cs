using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watson.Models
{
    public class GrpHealthVM
    {
        public List<Group_Health> GroupHealth { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Family_Info> FamilyMembers { get; set; }

    }
}