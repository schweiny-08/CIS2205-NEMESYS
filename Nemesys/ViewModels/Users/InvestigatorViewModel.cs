using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Nemesys.ViewModels.Users
{
    public class InvestigatorViewModel
    {
        public int idNum { get; set; }
        public string email { get; set; }
        //public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string imageUrl { get; set; }
        public int deptNum { set; get; }
    }
}