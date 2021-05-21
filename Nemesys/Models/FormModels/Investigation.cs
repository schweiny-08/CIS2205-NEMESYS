using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Nemesys.Models.FormModels;
//using Nemesys.Models.UserModels;

namespace Nemesys.Models.FormModels
{
    public class Investigation : IncidentForm
    {
        public Investigation() : base()
        {
            //investigator = null;
        }

       /* public Investigation(int idNum, Investigator investigator, Report report) : base(idNum)
        {
            this.investigator = investigator;
            this.report = report;
        }*/

       /* public string investigatorId { get; set; }
        public IdentityUser investigator { get; set; }*/

        public int reportId { get; set; }
        public Report report { get; set; }
    }
}