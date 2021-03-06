using Nemesys.Models.FormModels;
using Nemesys.ViewModels.HazardTypes;
using Nemesys.ViewModels.Investigations;
using Nemesys.ViewModels.Users;
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
        public string latitude{ get; set; }
        public string longitude{ get; set; }
        public int upvotes { get; set; }
        public string image { get; set; }
        public string status { get; set; }
        /*public int investigationId { get; set; }*/
        public InvestigationViewModel investigation{ get; set; }
        public HazardTypeViewModel hazardType { get; set; }
        public ReporterViewModel Reporter { get; set; }
    }
}