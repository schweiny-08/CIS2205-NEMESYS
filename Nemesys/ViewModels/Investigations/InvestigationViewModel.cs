using Nemesys.ViewModels.Reports;
using Nemesys.ViewModels.Users;
using System;
using System.Collections.Generic;


namespace Nemesys.ViewModels.Investigations
{
    public class InvestigationViewModel
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        public InvestigatorViewModel investigator { get; set; }
        public ReportViewModel report { get; set; }
    }
}