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
    }
}