using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Nemesys.Models.FormModels;
//using Nemesys.Models.UserModels;

namespace Nemesys.Models.FormModels
{
    public class Investigation : IncidentForm
    {
        public Investigation() : base()
        {
        }

        public int reportId { get; set; }
        public Report report { get; set; }
    }
}