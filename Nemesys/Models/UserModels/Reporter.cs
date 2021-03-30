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
            reports = new List<Report>(); 
        }

        public List<Report> reports;
    }
}