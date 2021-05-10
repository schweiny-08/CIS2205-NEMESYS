using System;
using System.Collections.Generic;


namespace Nemesys.ViewModels
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        //public int ownerId { get; set; }
        public string location { get; set; }
        public int upvotes { get; set; }
        public string image { get; set; }
        public string status { get; set; }
        //ReporterViewModel Reporter { get; set; }
    }
}