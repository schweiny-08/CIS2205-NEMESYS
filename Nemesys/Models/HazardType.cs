using Nemesys.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Nemesys.Models
{
    public class HazardType
    {

        public int Id { get; set; }
        public string hazardTypeName { get; set; }

        public List<Report> Reports { get; set; }
    }
}