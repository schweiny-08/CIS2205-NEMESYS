﻿using System;
using System.Collections.Generic;


namespace Nemesys.ViewModels
{
    public class ReportListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<ReportViewModel> Reports { get; set; }
    }
}