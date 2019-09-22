using Watson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Watson.ViewModels
{
    public class EmployeeAndInsuranceVM
    {
        //Employee class
        public Employee employee { get; set; }

        //Group_Health class
        public Group_Health grpHealth { get; set; }
    }
}