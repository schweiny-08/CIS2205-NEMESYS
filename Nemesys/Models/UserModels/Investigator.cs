using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nemesys.Models.UserModels
{
    public class Investigator : Reporter
    {
        /*public List<Investigations> investigations;*/

        public Investigator() : base()
        {
            deptNum = 0;
            /*investigations = new List<Investigations>;*/
        }

        public int deptNum { set; get; }
    }
}