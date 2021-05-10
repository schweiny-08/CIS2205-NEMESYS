using Nemesys.Models.FormModels;
using Nemesys.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesys.Models.Interfaces
{
    public interface INemesysRepository
    {
        // Reporters

        IEnumerable<Reporter> GetAllReporters();
        Reporter GetReporterById(int userId);
        Reporter GetReporterByEmail(string email);
        void AddNewReporter(Reporter reporter);
        void UpdateReporter(Reporter reporter);
        void DeleteReporter(Reporter reporter);

        // Investigators

        IEnumerable<Investigator> GetAllInvestigators();
        Investigator GetInvestigatorById(int userId);
        void AddNewInvestigator(Investigator investigator);
        void UpdateInvestigator(Investigator investigator);
        void DeleteInvestigator(Investigator investigator);

        // Reports

        IEnumerable<Report> GetAllReports();
        IEnumerable<Report> GetReportsByOwner(Reporter reporter);
        Report GetReportById(int idNum);
        //Report GetReportByOwner(int idNm,int userId);
        void AddNewReport(Report report);
        void UpdateReport(Report report);
        void ChangeReportStatus(Report report);

        void UpvoteReport(int reportId);
        void DownvoteReport(int reportId);
        void DeleteReport(Report report);

        // Investigations

        IEnumerable<Investigation> GetAllInvestigations();
        IEnumerable<Investigation> GetInvestigationsByOwner(Investigator investigator);
        Investigation GetInvestigationById(int idNum);
        void AddNewInvestigation(Investigation investigation);
        void UpdateInvestigation(Investigation investigation);
        void DeleteInvestigation(Investigation investigation);
    }
}