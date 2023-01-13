using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (System.Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(@"Error Occured " + ex);
                }
            }

            // host.Services.AddCors(options =>
            // {
            //     options.AddPolicy(name: "MyPolicy",
            //             policy =>
            //             {
            //                 policy.WithOrigins("http://example.com",
            //                     "http://www.contoso.com",
            //                     "https://cors1.azurewebsites.net",
            //                     "https://cors3.azurewebsites.net",
            //                     "https://localhost:44398",
            //                     "https://localhost:5001")
            //                         .WithMethods("PUT", "DELETE", "GET");
            //             });
            // });

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
