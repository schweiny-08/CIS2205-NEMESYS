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

        // Reporters

        public IEnumerable<Reporter> GetAllReporters()
        {
            return _nemesysContext.Reporters.Include(r => r.fName + r.lName).OrderBy(r => r.idNum);
        }

        public Reporter GetReporterById(int userId)
        {
            return _nemesysContext.Reporters.Include(r => r.reports).FirstOrDefault(r => r.idNum == userId);
        }

        public Reporter GetReporterByEmail(string email) {
            return _nemesysContext.Reporters.FirstOrDefault(r => r.email == email);
        }

        public void AddNewReporter(Reporter reporter)
        {
            _nemesysContext.Reporters.Add(reporter);
            _nemesysContext.SaveChanges();
        }

        public void UpdateReporter(Reporter reporter)
        {
            var existingReporter = _nemesysContext.Reporters.SingleOrDefault(r => r.idNum == reporter.idNum);
            if (existingReporter != null){ // Reporter exists 
                existingReporter.email = reporter.email;
                existingReporter.password = reporter.password;
                existingReporter.fName = reporter.fName;
                existingReporter.lName = reporter.lName;
                existingReporter.image = reporter.image;

                _nemesysContext.Entry(existingReporter).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }

        public void DeleteReporter(Reporter reporter)
        {
            _nemesysContext.Reporters.Remove(reporter);
            _nemesysContext.SaveChanges();
            //throw new NotImplementedException();
        }

        // Investigators

        public IEnumerable<Investigator> GetAllInvestigators()
        {
            return _nemesysContext.Investigators.Include(i => i.fName + i.lName).OrderBy(i => i.idNum);
        }

        public Investigator GetInvestigatorById(int userId)
        {
            throw new NotImplementedException();
        }

        public void AddNewInvestigator(Investigator investigator)
        {
            _nemesysContext.Investigators.Add(investigator);
            _nemesysContext.SaveChanges();
        } 

        public void UpdateInvestigator(Investigator investigator)
        {
            var existingInvestigator = _nemesysContext.Investigators.SingleOrDefault(i => i.idNum == investigator.idNum);
            if (existingInvestigator != null)
            { // Reporter exists 
                existingInvestigator.email = investigator.email;
                existingInvestigator.password = investigator.password;
                existingInvestigator.fName = investigator.fName;
                existingInvestigator.lName = investigator.lName;
                existingInvestigator.image = investigator.image;
                existingInvestigator.deptNum = investigator.deptNum;

                _nemesysContext.Entry(existingInvestigator).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }
        public void DeleteInvestigator(Investigator investigator)
        {
            _nemesysContext.Investigators.Remove(investigator);
            _nemesysContext.SaveChanges();
        }
    }
}