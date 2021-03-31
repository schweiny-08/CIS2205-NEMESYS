using Nemesys.Models.FormModels;
using Nemesys.Models.UserModels;

using Microsoft.EntityFrameworkCore;

namespace Nemesys.DAL
{
    public class NemesysContext : DbContext
    {
        public NemesysContext(DbContextOptions<NemesysContext> options) : base(options)//"NemesysContext") // Setting the connection string
        {
            
        }

        public DbSet<Reporter> Reporters { get; set; }
        public DbSet<Investigator> Investigators { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Investigation> Investigations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Reporter>().ToTable("Reporter");
            modelBuilder.Entity<Investigator>().ToTable("Investigator");
            modelBuilder.Entity<Report>().ToTable("Report");
            modelBuilder.Entity<Investigation>().ToTable("Investigation");
        }
    }
}