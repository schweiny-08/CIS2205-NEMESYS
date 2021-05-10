using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemesys.Models.FormModels;
using Nemesys.Models.UserModels;

namespace Nemesys.Models.FormModels
{
    public class Investigation : IncidentForm
    {
        public Investigation() : base()
        {
            investigator = null;
        }

        public Investigation(int idNum, Investigator investigator, Report report) : base(idNum, investigator)
        {
            this.investigator = investigator;
            this.report = report;
        }

        public Investigator investigator { get; set; }
        public Report report { get; set; }
        //public int reportID { get; set; }

       /* public void changeReportStatus()
        {
            //Report status is changed here
        }*/

        public void notifyReporter()
        {
            //Reporter is sent email
        }
    }
}