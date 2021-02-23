using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();       // get the IHost object to configure passing the Seed data. 
            using IServiceScope scope = host.Services.CreateScope();      // create a scope for the services you are about to create and use.
            IServiceProvider services = scope.ServiceProvider;  // you don't have access to the exception handler in the program bc it hasn't been created yet.
            try
            {
                var context = services.GetRequiredService<DataContext>();//  get the Db context
                await context.Database.MigrateAsync();          // automatically creates the Db, and migrates the current state of the models.
                                                                // If Db already exists, it will migrate the changes to the Db.
                await Seed.SeedUsers(context);                  // use the static Seed Class to replace whatever is in the b
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();               // get a temporary Log Service bc the programs log service isn't created yet.
                logger.LogError(ex, "FROM MARK - An error occurred during Db migration of seed data."); // log the error.
            }

            await host.RunAsync();// NOW run the Program.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
