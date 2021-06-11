using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nemesys.ViewModels.Admin
{
    public class ListRolesViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<RoleViewModel> UserRoles { get; set; }
    }
}