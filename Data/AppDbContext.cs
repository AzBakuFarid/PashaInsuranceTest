using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PashaInsuranceTest.DbEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> ctx) : base(ctx)
        {
        }

        public DbSet<Group> groups { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Spesification> spesifications { get; set; } 
    }
}
