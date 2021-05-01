using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Nemesys.Models.ViewModels
{
    public class RegisterAccountViewModel
    {
        public int idNum { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Email address not valid.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, ErrorMessage = "First name is too long.")]
        public string fName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name is too long.")]
        public string lName { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile image { get; set; }
    }
}