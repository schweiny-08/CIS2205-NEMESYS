using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemesys.Models.FormModels;
namespace Nemesys.Models.FormModels
{
    public class Investigation : IncidentForm
    {
        public Investigation() : base()
        {
            reportID = 0;
        }

        public Investigation(int idNum, int ownerID, int reportID) : base(idNum, ownerID)
        {
            this.reportID = reportID;
        }

        public int reportID { get; set; }

        public void changeReportStatus()
        {
            //Report status is changed here
        }

        public void notifyReporter()
        {
            //Reporter is sent email
        }
    }
}