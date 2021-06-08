using Microsoft.Extensions.Configuration;
using PashaInsuranceTest.Data;
using PashaInsuranceTest.DbEntities.Enums;
using PashaInsuranceTest.DbEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Helpers
{
    public class DataSeeder
    {

        public static void Seed(AppDbContext dbContext)
        {
            CreateSpesifications(dbContext);
            CreateServices(dbContext);
            CreateGroups(dbContext);
            dbContext.SaveChanges();
        }
        static void CreateSpesifications(AppDbContext dbContext) {
            if (dbContext.spesifications.Any()) return;
            dbContext.spesifications.AddRange(
                new Spesification { Name = "spesification1" },
                new Spesification { Name = "spesification2" },
                new Spesification { Name = "spesification3" }
                );
        }
        static void CreateServices(AppDbContext dbContext)
        {
            if (dbContext.services.Any()) return;
            dbContext.services.AddRange(
                new Service
                { 
                    Name = "service1",
                    StartsAt = new DateTime(2021, 05,16), 
                    ValidTill = new DateTime(2038, 05, 16), Type = ServiceTypeEnum.Type1
                },
                new Service
                {
                    Name = "service2",
                    StartsAt = new DateTime(2021, 04, 16),
                    ValidTill = new DateTime(2028, 05, 16),
                    Type = ServiceTypeEnum.Type2
                },
                new Service
                {
                    Name = "service3",
                    StartsAt = new DateTime(2021, 10, 16),
                    ValidTill = new DateTime(2028, 05, 16),
                    Type = ServiceTypeEnum.Type3
                }
            );

        }
        static void CreateGroups(AppDbContext dbContext)
        {
            if (dbContext.groups.Any()) return;
            dbContext.groups.AddRange( 
                new Group { Amount = 15M, Name = "Gold" },
                new Group { Amount = 9M, Name = "Silver" },
                new Group { Amount = 123M, Name = "Platinum" }
                );
        }
    }
}
