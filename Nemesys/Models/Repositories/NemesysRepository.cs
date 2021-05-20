using Microsoft.EntityFrameworkCore;
using Nemesys.DAL;
using Nemesys.Models.FormModels;
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
            return _nemesysContext.Reporters.OrderBy(r => r.idNum);
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
            return _nemesysContext.Investigators.OrderBy(i => i.idNum);
        }

        public Investigator GetInvestigatorById(int userId)
        {
            return _nemesysContext.Investigators.Include(i => i.investigations).FirstOrDefault(i => i.idNum == userId);
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

        // Reports

        public IEnumerable<Report> GetAllReports()
        {
            return _nemesysContext.Reports.Include(r => r.reporter).OrderBy(r => r.dateTime);
        }

        public IEnumerable<Report> GetReportsByOwner(Reporter reporter)
        {
            return _nemesysContext.Reports.Include(r => r.reporter).Include(r => r.investigation).Where(r => r.reporter == reporter);
        }

        public Report GetReportById(int idNum)
        {
            return _nemesysContext.Reports.Include(r => r.reporter).Include(r => r.investigation).FirstOrDefault(r => r.idNum == idNum);
        }

        public void AddNewReport(Report report)
        {
            _nemesysContext.Reports.Add(report);
            _nemesysContext.SaveChanges();
        }

        public void AddInvestigationToReport(int reportId, int investigationId, int status) {
            var reportToUpdate = this.GetReportById(reportId);
            if (reportToUpdate != null) {
                reportToUpdate.investidationId = investigationId;

                if (status == 1)
                    reportToUpdate.status = Report.Status.Investigating;
                else if (status == 2)
                    reportToUpdate.status = Report.Status.NoAction;
                else if (status == 3)
                    reportToUpdate.status = Report.Status.Closed;

                _nemesysContext.Entry(reportToUpdate).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }

        public void UpdateReport(Report report)
        {
            var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == report.idNum);
            if(existingReport != null){
                existingReport.latitude = report.latitude;
                existingReport.longitude = report.longitude;
                existingReport.description = report.description;
                existingReport.image = report.image;

                _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }

        public void ChangeReportStatus(Report report)
        {
            var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == report.idNum);
            if (existingReport != null)
            {
                existingReport.status = report.status;

                _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }

        public void UpvoteReport(int reportId) { 
            var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == reportId);
            if (existingReport != null) {
                existingReport.upvotes++;

                _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }
        
        public void DownvoteReport(int reportId) { 
            var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == reportId);
            if (existingReport != null) {
                existingReport.upvotes--;

                _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }

        public void DeleteReport(Report report)
        {
            _nemesysContext.Reports.Remove(report);
            _nemesysContext.SaveChanges();
        }

        // Investigations

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            return _nemesysContext.Investigations.Include(i => i.investigator).Include(i => i.report).OrderBy(i => i.dateTime);
        }

        public IEnumerable<Investigation> GetInvestigationsByOwner(Investigator investigator)
        {
            return _nemesysContext.Investigations.Include(i => i.investigator).Include(i => i.report).Where(i => i.investigator == investigator);
        }

        public Investigation GetInvestigationById(int idNum)
        {
            return _nemesysContext.Investigations.Include(i => i.investigator).Include(i => i.report).FirstOrDefault(i => i.idNum == idNum);
        }

        public void AddNewInvestigation(Investigation investigation)
        {
            _nemesysContext.Investigations.Add(investigation);

            //Send email
            _nemesysContext.SaveChanges();
        }

        public void UpdateInvestigation(Investigation investigation)
        {
            var existingInvestigation = _nemesysContext.Investigations.SingleOrDefault(i => i.idNum == investigation.idNum);
            if (existingInvestigation != null) {
                existingInvestigation.dateTime = investigation.dateTime;
                existingInvestigation.description = investigation.description;
                existingInvestigation.report = investigation.report;

                _nemesysContext.Entry(existingInvestigation).State = EntityState.Modified;
                _nemesysContext.SaveChanges();
            }
        }

        public void DeleteInvestigation(Investigation investigation)
        {
            _nemesysContext.Investigations.Remove(investigation);
            _nemesysContext.SaveChanges();
        }

        // Hazard Types

        public IEnumerable<HazardType> GetAllHazardTypes()
        {
            return _nemesysContext.HazardTypes.OrderBy(h => h.Id);
        }

        public HazardType GetHazardTypeById(int idNum)
        {
            return _nemesysContext.HazardTypes.FirstOrDefault(h => h.Id == idNum);
        }

        public void AddNewHazardType(HazardType hazardType)
        {
            _nemesysContext.HazardTypes.Add(hazardType);

            _nemesysContext.SaveChanges();
        }

        public void DeleteHazardType(HazardType hazardType)
        {
            _nemesysContext.HazardTypes.Remove(hazardType);
            _nemesysContext.SaveChanges();
        }
    }
}