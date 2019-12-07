using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watson.Models
{
    public class SelectPdfParameters
    {
        public string key { get; set; }
        public string url { get; set; }
        public string html { get; set; }
        public string base_url { get; set; }
    }
}