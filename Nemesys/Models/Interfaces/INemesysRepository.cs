using Nemesys.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesys.Models.Interfaces
{
    public interface INemesysRepository
    {
        IEnumerable<Person> GetAllUsers();
        Person GetUserById(int userId);

        IEnumerable<Reporter> GetAllReporters();
        Reporter GetReporterById(int userId);

        IEnumerable<Investigator> GetAllInvestigators();
        Investigator GetInvestigatorById(int userId);
    }
}