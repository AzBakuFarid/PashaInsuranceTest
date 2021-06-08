using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PashaInsuranceTest.Data;
using PashaInsuranceTest.Helpers;
using PashaInsuranceTest.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Extensions
{
    public static class SeedExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var repo = services.GetService<IBaseRepository>();
                var env = services.GetService<IWebHostEnvironment>();
                var filepath = Path.Combine(env.ContentRootPath, "MockData", "data.json");
                var data = JsonConvert.DeserializeObject<MockDataWrapper>(File.ReadAllText(filepath));
                DataSeeder.Seed(repo, data);
            }
            return host;
        }
    }

}
