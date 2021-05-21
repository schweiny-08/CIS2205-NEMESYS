//using Nemesys.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesys.Models.FormModels
{
    public class Report : IncidentForm
    {
        public enum Status
        {
            Open,
            Closed,
            Investigating,
            NoAction
        }

        public Report() : base()
        {
            title = "";
            //location = null;
            upvotes = 0;
            image = null;
            status = Status.Open;
        }

       /* public Report(int idNum, Reporter reporter, string location, int upvotes) : base(idNum)
        {
            //this.location = location;
            this.upvotes = upvotes;
            image = null;
            status = Status.Open;
        }*/

        public string title { get; set; }
        //public string location { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int upvotes { get; set; }
        public string image { get; set; }
        public Status status { get; set; }
       /* public string reporteridNum { get; set; }
        public IdentityUser reporter { get; set; }*/
        public int investidationId { get; set; }
        public Investigation investigation { get; set; }
        public int hazardTypeId { get; set; }
        public HazardType hazardType { get; set; }
    }
}