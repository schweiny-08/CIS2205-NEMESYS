using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemesys.Models.FormModels;


namespace Nemesys.Models.UserModels
{
    public class Reporter : Person
    {

        public Reporter() : base()
        {
        }

        public Reporter(int idNum, string email, string password, string fName, string lName) : base(idNum, email, password, fName, lName)
        {

        }

        public void addReport(Report report)
        {
            reports.Add(report);
        }

        public virtual ICollection<Report> reports { get; set; }
    }
}