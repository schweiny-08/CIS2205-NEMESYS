using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemesys.Models.FormModels;

namespace Nemesys.Models.UserModels
{
    public class Investigator : Reporter
    {
        public Investigator() : base()
        {
            deptNum = 0;
        }

        public Investigator(int idNum, string email, string password, string fName, string lName, int deptNum) : base(idNum, email, password, fName, lName)
        {
            this.deptNum = deptNum;
        }

        public int deptNum { set; get; }

        public virtual ICollection<Investigation> investigations { get; set; }

       /* public void addInvestigation(Investigation inv)
        {
            investigations.Add(inv);
        }*/
    }
}