using Watson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Watson.ViewModels
{
    public class SpouseAndDependentInsVM
    {
        public Employee employee { get; set; }
        public Family_Info family { get; set; }
        public Other_Insurance otherIns { get; set; }
    }
}