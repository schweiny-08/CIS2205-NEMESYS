using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nemesys.ViewModels.Admin
{
    public class AddUserToRoleViewModel
    {
        [Required]
        [Display(Name = "User Email")]
        public string userEmail { get; set; }

        [Required]
        [Display(Name = "User Role")]
        public string RoleId { get; set; }

        public List<RoleViewModel> UserRoleList { get; set; }
    }
}