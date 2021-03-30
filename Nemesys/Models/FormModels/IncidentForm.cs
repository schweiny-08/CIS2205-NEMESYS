﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesys.Models.FormModels
{
    public abstract class IncidentForm{ 
        public IncidentForm()
        {
            idNum = ownerID = 0;
            dateTime = default;
            description = null;
        }

        public int idNum { set; get; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        public int ownerID { get; set; }
    }
}