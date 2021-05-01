using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nemesys.Models.UserModels
{
    public abstract class Person
    {
        protected Person()
        {
            idNum = 0;
            email = password = fName = lName = null;
            image = null;
        }

        protected Person(int idNum, string email, string password, string fName, string lName)
        {
            this.idNum = idNum;
            this.email = email;
            this.password = password;
            this.fName = fName;
            this.lName = lName;
            image = null;
        }

        [Key]
        public int idNum { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        [Required]
        public string password { get; set; }
        
        [Required]
        public string fName { get; set; }
        
        [Required]
        public string lName { get; set; }
        
        public string image { get; set; }
    }
}