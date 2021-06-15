using Nemesys.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nemesys.ViewModels.Admin
{
    public class ListReportersViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<ReporterViewModel> Reporters { get; set; }
    }
}