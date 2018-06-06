using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PdfToDb.Models
{
    public class EmpDataViewModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string WorkHours { get; set; }
        public string OverTime { get; set; }
    }
}