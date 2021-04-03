using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemesys.Models.FormModels;
using Nemesys.Models.UserModels;

namespace Nemesys.DAL
{
    public class DbInitializer
    {
        public static void Initialize(NemesysContext context)
        {
            context.Database.EnsureCreated();

            if (context.Reporters.Any())
                return;

            var reporters = new Reporter[] {
            new Reporter{fName = "Joe", lName = "Borg", email = "joe@borg.com", password = "pass"},
            new Reporter{fName = "Andy", lName = "Grech", email = "andy@grech.com", password = "pass"},
            new Reporter{fName = "Aldo", lName = "Preca", email = "aldo@preca.com", password = "pass"}
            };
            foreach (Reporter r in reporters)
            {
                context.Reporters.Add(r);
            }
            context.SaveChanges();

            var investigators = new Investigator[] {
            new Investigator{fName = "Paul", lName = "Borg", email = "paul@borg.com", password = "pass", deptNum = 1},
            new Investigator{fName = "Gorg", lName = "Pisani", email = "gorg@pisani.com", password = "pass", deptNum = 2},
            new Investigator{fName = "Aldo", lName = "Borg", email = "aldo@borg.com", password = "pass", deptNum = 3}
            };
            foreach (Investigator i in investigators)
            {
                context.Investigators.Add(i);
            }
            context.SaveChanges();

            var reports = new Report[] {
            new Report{dateTime = DateTime.Parse("2005-09-01"), description = "This is a description", status = Report.Status.Open, upvotes = 1},
            new Report{dateTime = DateTime.Parse("2020-1-21"), description = "This is also a description", status = Report.Status.Investigating, upvotes = 10},
            new Report{dateTime = DateTime.Parse("2015-11-10"), description = "This is another description", status = Report.Status.Closed, upvotes = 21}
            };
            foreach (Report r in reports)
            {
                context.Reports.Add(r);
            }
            context.SaveChanges();

            var investigations = new Investigation[] {
            new Investigation{dateTime = DateTime.Parse("2006-09-01"), description = "This is a description"},
            new Investigation{dateTime = DateTime.Parse("2007-02-04"), description = "This is a different description"},
            new Investigation{dateTime = DateTime.Parse("2012-10-22"), description = "This is a bloody description"}
            };
            foreach (Investigation i in investigations)
            {
                context.Investigations.Add(i);
            }
            context.SaveChanges();
        }
    }
}