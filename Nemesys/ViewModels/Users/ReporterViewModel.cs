using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Nemesys.ViewModels.Users
{
    public class ReporterViewModel
    {
        public string idNum { get; set; }
        public string email { get; set; }
        //public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string imageUrl { get; set; }
        public int numOfReports { get; set; }
    }
}