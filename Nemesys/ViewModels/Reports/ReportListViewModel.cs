using System;
using System.Collections.Generic;


namespace Nemesys.ViewModels.Reports
{
    public class ReportListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<ReportViewModel> Reports { get; set; }
    }
}