using Nemesys.Models.FormModels;
using Nemesys.Models.UserModels;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Nemesys.DAL
{
    public class NemesysContext : DbContext
    {
        public NemesysContext() : base("NemesysContext") // Setting the connection string
        {
            
        }

        public DbSet<Reporter> Reporters { get; set; }
        public DbSet<Investigator> Investigators { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Investigation> Investigations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}