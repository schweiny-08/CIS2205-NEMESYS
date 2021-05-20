using Microsoft.AspNetCore.Http;
using Nemesys.Models.FormModels;
using Nemesys.ViewModels.HazardTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemesys.ViewModels.Reports
{
    public class CreateEditReportViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A title is required!")]
        [StringLength(50, ErrorMessage = "Title is too long!")]
        [Display(Name = "Report Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Date and time are required!")]
        [Display(Name = "Report Date and Time")]
        public DateTime dateTime { get; set; }

        [Required(ErrorMessage = "Report description is required!")]
        [StringLength(1500, ErrorMessage = "Description is too long!")]
        [Display(Name = "Report Description")]
        public string description { get; set; }
     
        [Display(Name = "Report Location")]
        public string latitude{ get; set; }
        [Display(Name = "Report Location")]
        public string longitude { get; set; }

        [Display(Name = "Image of hazard")]
        [NotMapped]
        public IFormFile image { get; set; }
        public string imageUrl { get; set; }
        
        [Required(ErrorMessage ="Hazard type is required!")]

        public int hazardTypeId { get; set; }

        public List<HazardTypeViewModel> HazardTypeList { get; set; }
    }
}