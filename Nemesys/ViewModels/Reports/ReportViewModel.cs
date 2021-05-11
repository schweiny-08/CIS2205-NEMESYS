using Nemesys.Models.FormModels;
using Nemesys.ViewModels.Investigations;
using System;
using System.Collections.Generic;


namespace Nemesys.ViewModels.Reports
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        //public int ownerId { get; set; }
        public string location { get; set; }
        public int upvotes { get; set; }
        public string image { get; set; }
        public string status { get; set; }
        /*public int investigationId { get; set; }*/
        public InvestigationViewModel investigation{ get; set; }

        //ReporterViewModel Reporter { get; set; }
    }
}