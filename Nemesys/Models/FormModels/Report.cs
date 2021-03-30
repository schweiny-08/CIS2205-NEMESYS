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
            location = null;
            upvotes = 0;
            image = null;
            status = Status.Open;
        }

        public Report(int idNum, int ownerID, string location, int upvotes) : base(idNum, ownerID)
        {
            this.location = location;
            this.upvotes = upvotes;
            image = null;
            status = Status.Open;
        }

        public string location { get; set; }
        public int upvotes { get; set; }
        public byte[] image { get; set; }
        public Status status { get; set; }
    }
}