using System;
using System.Collections.Generic;
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

        protected Person(int idNum, string email, string password, string fName, string lName) {
            this.idNum = idNum;
            this.email = email;
            this.password = password;
            this.fName = fName;
            this.lName = lName;
            image = null;
        }

        public int idNum { get; set; }
        public string email { get; set; }
        public string password { get; set; } 
        public string fName { get; set; }
        public string lName { get; set; }
        public byte[] image { get; set; }

    }
}