using Microsoft.AspNetCore.Http;
using Nemesys.Models.FormModels;
using Nemesys.ViewModels.HazardTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemesys.ViewModels.HazardTypes
{
    public class CreateEditHazardTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A hazard type name is required!")]
        [StringLength(50, ErrorMessage = "Hazard type name is too long!")]
        [Display(Name = "Hazard Type Name")]
        public string hazardTypeName { get; set; }
    }
}