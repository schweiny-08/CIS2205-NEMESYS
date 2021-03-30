using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemesys.Models.FormModels;


namespace Nemesys.Models.UserModels
{
    public class Reporter : Person
    {
        public List<Report> reports;

        public Reporter() : base()
        {
            reports = new List<Report>(); 
        }

        public void addReport(Report report) {
            reports.Add(report);
        }
    }
}