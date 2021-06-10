//using Nemesys.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nemesys.Models.FormModels
{
    public abstract class IncidentForm
    {
        public IncidentForm(){}

      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idNum { set; get; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        //public int ownerID { get; set; }
        //Person user { get; set; }
    }
}