using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesys.Models.FormModels
{
    public class Report : IncidentForm
    {
        public enum Status {
            Open,
            Closed,
            Investigating,
            NoAction
        }

        public Report() : base()
        {    

        }

        public string location { get; set; }
        public int upvotes { get; set; }
        public byte[] image { get; set; }
        public Status status { get; set; }
    }
}