using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nemesys.DAL;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Nemesys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<NemesysContext>();
                    //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    DbInitializer.Initialize(context);
                }
                catch (Exception e){
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured while creating DB");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => 
                {
                    logging.ClearProviders();
                    logging.AddEventLog();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
