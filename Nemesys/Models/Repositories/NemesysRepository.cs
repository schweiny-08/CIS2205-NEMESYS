using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nemesys.DAL;
using Nemesys.Models.FormModels;
using Nemesys.Models.Interfaces;
//using Nemesys.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nemesys.Models.Repositories
{
    public class NemesysRepository : INemesysRepository
    {
        private readonly NemesysContext _nemesysContext;
        private readonly ILogger _logger;

        public NemesysRepository(NemesysContext nemesysContext, ILogger<NemesysRepository> logger)
        {
            try
            {
                _nemesysContext = nemesysContext;
                _logger = logger;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        // Reports

        public IEnumerable<Report> GetAllReports()
        {
            try
            {
                return _nemesysContext.Reports.Include(r => r.hazardType).OrderBy(r => r.dateTime); // include reporter!
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public IEnumerable<Report> GetReportsByOwner(IdentityUser reporter)
        {
            try
            {
                return _nemesysContext.Reports.Include(r => r.hazardType).Include(r => r.investigation).Where(r => r.User == reporter);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public Report GetReportById(int idNum)
        {
            try
            {
                return _nemesysContext.Reports.Include(r => r.hazardType).Include(r => r.investigation).FirstOrDefault(r => r.idNum == idNum);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void AddNewReport(Report report)
        {
            try
            {
                _nemesysContext.Reports.Add(report);
                _nemesysContext.SaveChanges();
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void AddInvestigationToReport(int reportId, int investigationId, int status) {
            try
            {
                var reportToUpdate = this.GetReportById(reportId);
                if (reportToUpdate != null)
                {
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
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void UpdateReport(Report report)
        {
            try
            {
                var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == report.idNum);
                if (existingReport != null)
                {
                    existingReport.latitude = report.latitude;
                    existingReport.longitude = report.longitude;
                    existingReport.description = report.description;
                    existingReport.image = report.image;
                    existingReport.hazardTypeId = report.hazardTypeId;

                    _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                    _nemesysContext.SaveChanges();
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void ChangeReportStatus(Report report)
        {
            try
            {
                var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == report.idNum);
                if (existingReport != null)
                {
                    existingReport.status = report.status;

                    _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                    _nemesysContext.SaveChanges();
                }
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void UpvoteReport(int reportId) {
            try
            {
                var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == reportId);
                if (existingReport != null)
                {
                    existingReport.upvotes++;

                    _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                    _nemesysContext.SaveChanges();
                }
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }
        
        public void DownvoteReport(int reportId)
        {
            try
            {
                var existingReport = _nemesysContext.Reports.SingleOrDefault(r => r.idNum == reportId);
                if (existingReport != null)
                {
                    existingReport.upvotes--;

                    _nemesysContext.Entry(existingReport).State = EntityState.Modified;
                    _nemesysContext.SaveChanges();
                }
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void DeleteReport(Report report)
        {
            try
            {
                _nemesysContext.Reports.Remove(report);
                _nemesysContext.SaveChanges();
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        // Investigations

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            try
            {
                return _nemesysContext.Investigations.Include(i => i.report).OrderBy(i => i.dateTime);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public IEnumerable<Investigation> GetInvestigationsByOwner(IdentityUser investigator)
        {
            try
            {
                return _nemesysContext.Investigations.Include(i => i.report).Where(i => i.User == investigator);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public Investigation GetInvestigationById(int idNum)
        {
            try
            {
                return _nemesysContext.Investigations.Include(i => i.report).FirstOrDefault(i => i.idNum == idNum); //
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void AddNewInvestigation(Investigation investigation)
        {
            try
            {
                _nemesysContext.Investigations.Add(investigation);
                _nemesysContext.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void UpdateInvestigation(Investigation investigation)
        {
            try
            {
                var existingInvestigation = _nemesysContext.Investigations.SingleOrDefault(i => i.idNum == investigation.idNum);
                if (existingInvestigation != null)
                {
                    existingInvestigation.dateTime = investigation.dateTime;
                    existingInvestigation.description = investigation.description;
                    existingInvestigation.report = investigation.report;

                    _nemesysContext.Entry(existingInvestigation).State = EntityState.Modified;
                    _nemesysContext.SaveChanges();
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void DeleteInvestigation(Investigation investigation)
        {
            try
            {
                _nemesysContext.Investigations.Remove(investigation);
                _nemesysContext.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        // Hazard Types

        public IEnumerable<HazardType> GetAllHazardTypes()
        {
            try
            {
                return _nemesysContext.HazardTypes.OrderBy(h => h.Id);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public HazardType GetHazardTypeById(int idNum)
        {
            try
            {
                return _nemesysContext.HazardTypes.FirstOrDefault(h => h.Id == idNum);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void AddNewHazardType(HazardType hazardType)
        {
            try
            {
                _nemesysContext.HazardTypes.Add(hazardType);
                _nemesysContext.SaveChanges();
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void DeleteHazardType(HazardType hazardType)
        {
            try
            {
                _nemesysContext.HazardTypes.Remove(hazardType);
                _nemesysContext.SaveChanges();
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}