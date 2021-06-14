using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Nemesys.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string faculty { get; set; }
        [PersonalData]
        [Required(ErrorMessage = "You must enter a name!")]
        [Display(Name = "First Name")]
        public string fName { get; set; }
        [PersonalData]
        [Required(ErrorMessage = "You must enter a name!")]
        [Display(Name = "First Name")]
        public string lName { get; set; }
    }
}