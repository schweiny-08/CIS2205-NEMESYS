using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nemesys.Models.FormModels;


namespace Nemesys.ViewModels.Users
{
    public class ProfilePageViewModel
    {
        public string idNum { get; set; }
        public string email { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string faculty { get; set; }
        public string role { get; set; }
        //IEnumerable<Report> Reports { get; set; }
        //IEnumerable<Investigation> Investigations { get; set; }
    }
}