using Nemesys.Models.FormModels;
//using Nemesys.Models.UserModels;

using Microsoft.EntityFrameworkCore;
using Nemesys.ViewModels;
using Nemesys.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Nemesys.DAL
{
    public class NemesysContext : IdentityDbContext<ApplicationUser>
    {
        public NemesysContext(DbContextOptions<NemesysContext> options) : base(options) // Setting the connection string
        {

        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Investigation> Investigations { get; set; }
        public DbSet<HazardType> HazardTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable("Report");
            modelBuilder.Entity<Investigation>().ToTable("Investigation");
            modelBuilder.Entity<HazardType>().ToTable("HazardType");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}