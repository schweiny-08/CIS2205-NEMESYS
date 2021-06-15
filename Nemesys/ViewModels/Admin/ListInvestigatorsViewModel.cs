using Nemesys.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nemesys.ViewModels.Admin
{
    public class ListInvestigatorsViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<InvestigatorViewModel> Invetigators { get; set; }
    }
}