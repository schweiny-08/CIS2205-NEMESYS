using Microsoft.EntityFrameworkCore;
using Nemesys.DAL;
using Nemesys.Models.Interfaces;
using Nemesys.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nemesys.Models.Repositories
{
    public class NemesysRepository : INemesysRepository
    {
        private readonly NemesysContext _nemesysContext;

        public NemesysRepository(NemesysContext nemesysContext)
        {
            _nemesysContext = nemesysContext;
        }

        public Investigator AddNewInvestigator(Investigator investigator)
        {
            throw new NotImplementedException();
        }

        public Reporter AddNewReporter(Reporter reporter)
        {
            throw new NotImplementedException();
        }
        public bool DeleteInvestigator(Investigator investigator)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReporter(Reporter reporter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Investigator> GetAllInvestigators()
        {
            return _nemesysContext.Investigators.Include(i => i.fName + i.lName).OrderBy(i => i.idNum);
        }

        public IEnumerable<Reporter> GetAllReporters()
        {
            return _nemesysContext.Reporters.Include(r => r.fName + r.lName).OrderBy(r => r.idNum);
        }

        public Investigator GetInvestigatorById(int userId)
        {
            throw new NotImplementedException();
        }

        public Reporter GetReporterById(int userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateInvestigator(Investigator investigator)
        {
            throw new NotImplementedException();
        }

        public bool UpdateReporter(Reporter reporter)
        {
            throw new NotImplementedException();
        }
    }
}