using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSProject.Models
{
    public class OrderDateGroup
    {
        public DateTime? OrderDate { get; set; }

        public int OrderCount { get; set; }
    }
}