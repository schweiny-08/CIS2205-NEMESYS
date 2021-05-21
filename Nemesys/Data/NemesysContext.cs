using Nemesys.Models.FormModels;
//using Nemesys.Models.UserModels;

using Microsoft.EntityFrameworkCore;
using Nemesys.ViewModels;
using Nemesys.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Nemesys.DAL
{
    public class NemesysContext : IdentityDbContext
    {
        public NemesysContext(DbContextOptions<NemesysContext> options) : base(options) // Setting the connection string
        {

        }

        //public DbSet<Reporter> Reporters { get; set; }
        //public DbSet<Investigator> Investigators { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Investigation> Investigations { get; set; }
        public DbSet<HazardType> HazardTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Reporter>().ToTable("Reporter");
            //modelBuilder.Entity<Investigator>().ToTable("Investigator");
            modelBuilder.Entity<Report>().ToTable("Report");
            modelBuilder.Entity<Investigation>().ToTable("Investigation");
            modelBuilder.Entity<HazardType>().ToTable("HazardType");
            
            base.OnModelCreating(modelBuilder);
        }

      /*  public DbSet<Nemesys.ViewModels.Reports.ReportViewModel> ReportViewModel { get; set; }

        public DbSet<Nemesys.ViewModels.Reports.CreateEditReportViewModel> CreateEditReportViewModel { get; set; }
*/
       /* public DbSet<Nemesys.Models.UserModels.Person> Person { get; set; }*/
    }
}