using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Nemesys.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string faculty { get; set; }
    }
}