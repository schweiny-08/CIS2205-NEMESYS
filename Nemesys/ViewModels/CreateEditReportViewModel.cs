using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemesys.ViewModels
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
        //public int ownerId { get; set; }
        /*[Required(ErrorMessage = "location is required!")]*/
        [Display(Name = "Report Location")]
        public string location { get; set; }
        
        //public int upvotes { get; set; }

        [Display(Name = "Image of hazard")]
        [NotMapped]
        public IFormFile image { get; set; }
        public string imageUrl { get; set; }
        //public string status { get; set; }
    }
}