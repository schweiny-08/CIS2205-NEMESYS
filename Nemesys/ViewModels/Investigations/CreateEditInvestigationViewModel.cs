using Microsoft.AspNetCore.Http;
using Nemesys.Models.FormModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemesys.ViewModels.Investigations 
{ 
    public class CreateEditInvestigationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date and time are required!")]
        [Display(Name = "Investigation Date and Time")]
        public DateTime dateTime { get; set; }

        [Required(ErrorMessage = "Investigation description is required!")]
        [StringLength(1500, ErrorMessage = "Description is too long!")]
        [Display(Name = "Investigation Description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please choose a report status")]
        [Display(Name = "Change report status to:")]
        public string reportStatus { get; set; }

        //public int repoortId { get; set; }
    }
}