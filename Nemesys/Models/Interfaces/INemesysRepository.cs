using Nemesys.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesys.Models.Interfaces
{
    public interface INemesysRepository
    {
        IEnumerable<Reporter> GetAllReporters();
        Reporter GetReporterById(int userId);

        IEnumerable<Investigator> GetAllInvestigators();
        Investigator GetInvestigatorById(int userId);

        Reporter AddNewReporter(Reporter reporter);
        Investigator AddNewInvestigator(Investigator investigator);

        bool UpdateReporter(Reporter reporter);
        bool UpdateInvestigator(Investigator investigator);

        bool DeleteReporter(Reporter reporter);
        bool DeleteInvestigator(Investigator investigator);
    }
}