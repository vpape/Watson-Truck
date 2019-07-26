using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watson.Models
{
    public class GrpHealthVM
    {
        public Group_Health groupHealthVM { get; set; }
        public Employee employeeVM { get; set; }
        public Family_Info familyVM { get; set; }

    }
}