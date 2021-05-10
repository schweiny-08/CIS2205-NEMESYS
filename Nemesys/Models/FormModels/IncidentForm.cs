using Nemesys.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nemesys.Models.FormModels
{
    public abstract class IncidentForm
    {
        public IncidentForm()
        {
            idNum = 0;
            dateTime = default;
            description = null;
        }

        public IncidentForm(int idNum, Person user)
        {
            this.idNum = idNum;
            //this.ownerID = ownerID;
            this.user = user;
            dateTime = default;
            description = "This is a description";
        }
        [Key]
        public int idNum { set; get; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        //public int ownerID { get; set; }
        Person user { get; set; }
    }
}