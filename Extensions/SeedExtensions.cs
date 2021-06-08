using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PashaInsuranceTest.Data;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.Helpers;
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
                var context = services.GetService<AppDbContext>();
                var env = services.GetService<IWebHostEnvironment>();
                var filepath = Path.Combine(env.ContentRootPath, "MockData", "data.json");
                var data = JsonConvert.DeserializeObject<MockDataWrapper>(File.ReadAllText(filepath));
                DataSeeder.Seed(context);
            }
            return host;
        }
    }
    public class MockDataWrapper
    {
        public List<Group> Groups { get; set; }
        public List<Service> Services { get; set; }
        public List<Spesification> Spesifications { get; set; }
        public Dictionary<string, string[]> ServiceSpesifications { get; set; }
        public Dictionary<string, string[]> GroupServices { get; set; }


    }
}
