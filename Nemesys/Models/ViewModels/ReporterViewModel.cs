using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Nemesys.Models.ViewModels
{
    public class ReporterViewModel
    {
        public int idNum { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public byte[] image { get; set; }
    }
}