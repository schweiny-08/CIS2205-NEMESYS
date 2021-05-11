using Nemesys.ViewModels.Reports;
using Nemesys.ViewModels.Users;
using System;
using System.Collections.Generic;


namespace Nemesys.ViewModels.Investigations
{
    public class InvestigationListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<InvestigationViewModel> Investigations { get; set; }
    }
}