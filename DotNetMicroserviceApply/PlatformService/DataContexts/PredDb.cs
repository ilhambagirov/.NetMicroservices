using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.DataContexts
{
    public static class PredDb
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetRequiredService<AppDbContext>());
            }
        }

        private async static void SeedData(AppDbContext db)
        {
            if (!db.Platforms.Any())
            {
                await db.Platforms.AddRangeAsync(
                    new Platform()
                    {
                        Name = "DotNet",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                     new Platform()
                     {
                         Name = "Sql Server",
                         Publisher = "Microsoft",
                         Cost = "Free"
                     },
                      new Platform()
                      {
                          Name = "Kubernetes",
                          Publisher = "Cloud",
                          Cost = "Free"
                      }
                    );
                await db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("We do have Data");
            }
        }
    }
}
