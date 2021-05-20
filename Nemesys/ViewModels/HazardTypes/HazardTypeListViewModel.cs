using System;
using System.Collections.Generic;
using System.Linq;

namespace Nemesys.ViewModels.HazardTypes
{
    public class HazardTypeListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<HazardTypeViewModel> HazardTypes { get; set; }
    }
}