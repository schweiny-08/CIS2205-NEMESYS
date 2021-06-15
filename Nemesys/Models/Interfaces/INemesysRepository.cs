using Microsoft.AspNetCore.Identity;
using Nemesys.Models.FormModels;
//using Nemesys.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesys.Models.Interfaces
{
    public interface INemesysRepository
    {
        // Reports

        IEnumerable<Report> GetAllReports();
        IEnumerable<Report> GetReportsByOwner(string Id);
        IEnumerable<Report> GetReportsInPastYear();
        Report GetReportById(int idNum);
        //Report GetReportByOwner(int idNm,int userId);
        void AddNewReport(Report report);
        void AddInvestigationToReport(int reportId, int investigationId, int status);
        void UpdateReport(Report report);
        void ChangeReportStatus(Report report);

        void UpvoteReport(int reportId);
        void DownvoteReport(int reportId);
        void DeleteReport(Report report);

        // Investigations

        IEnumerable<Investigation> GetAllInvestigations();
        IEnumerable<Investigation> GetInvestigationsByOwner(string Id);
        Investigation GetInvestigationById(int idNum);
        void AddNewInvestigation(Investigation investigation);
        void UpdateInvestigation(Investigation investigation);
        void DeleteInvestigation(Investigation investigation);

        // Hazard Types
        IEnumerable<HazardType> GetAllHazardTypes();
        HazardType GetHazardTypeById(int idNum);
        void AddNewHazardType(HazardType hazardType);
        void DeleteHazardType(HazardType hazardType);

        // Users
        IEnumerable<ApplicationUser> GetUsersByRole(string role);
        IEnumerable<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserByEmail(string email);

        // Roles
        IEnumerable<IdentityRole> GetAllUserRoles();
        //IEnumerable<ApplicationUser> GetUsersByRoles();
        Task<IdentityResult> AddNewRole(IdentityRole role);
        string GetRoleNameById(string roleId);
        void ChangeUserRole(ApplicationUser user, string newRole);
        string GetRoleNameByUser(ApplicationUser user);
    }
}