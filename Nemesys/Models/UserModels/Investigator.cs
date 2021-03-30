using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemesys.Models.FormModels;

namespace Nemesys.Models.UserModels
{
    public class Investigator : Reporter
    {
        public List<Investigation> investigations;

        public Investigator() : base()
        {
            deptNum = 0;
            investigations = new List<Investigation>();
        }

        public int deptNum { set; get; }

        public void addInvestigation(Investigation inv) {
            investigations.Add(inv);
        }
    }
}