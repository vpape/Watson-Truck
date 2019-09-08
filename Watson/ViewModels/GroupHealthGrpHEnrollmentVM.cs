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
        //Employee class
        public Employee employee { get; set; }
       
        //Family_Info class
        public List<Family_Info> family { get; set; }
        
        //Group_Health class
        public Group_Health grpHealth { get; set; }

        //Other_Insurance class
        public List<Other_Insurance> otherIns { get; set; }

    }
}