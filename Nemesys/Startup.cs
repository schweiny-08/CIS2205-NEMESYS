using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Nemesys.DAL;
using Nemesys.Models.Interfaces;
using Nemesys.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Nemesys.Models;

namespace Nemesys
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env ,IConfiguration configuration)
        {
            _env = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NemesysContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("NemesysDBConnection")));

            //services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddDistributedMemoryCache();
            //services.AddRazorPages();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                // User needs authorized account to sign in
                options.SignIn.RequireConfirmedAccount = true;

                // Password Requirements
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings - when there are a number of failed attempts in login
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings - constrains on username/email
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<NemesysContext>();

            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.SlidingExpiration = true;
            });

            services.AddTransient<INemesysRepository, NemesysRepository>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                /*endpoints.MapControllerRoute(
                    name: "root",
                    pattern: "{action}",
                    defaults: new { controller = "Home", action="Index"}
                    );
*/
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
