using PashaInsuranceTest.Extensions;
using PashaInsuranceTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Helpers
{
    public class DataSeeder
    {

        public static void Seed(IBaseRepository repo, MockDataWrapper data)
        {
            Create(repo, data.Spesifications);
            Create(repo, data.Services);
            Create(repo, data.Groups);
            data.Services.ForEach(service =>
                data.Spesifications
                    .Where(w => data.ServiceSpesifications[service.Name]
                    .Contains(w.Name))
                    .ForEach(spec => service.Spesifications.Add(spec))
            );
            data.Groups.ForEach(group =>
                data.Services
                    .Where(w => data.GroupServices[group.Name]
                    .Contains(w.Name))
                    .ForEach(service => group.Services.Add(service))
            );
            repo.Commit();
        }
        static IEnumerable<TData> Create<TData>(IBaseRepository repo, IEnumerable<TData> data) where TData : class
        {
            var dbEntities = repo.List<TData>(); ;
            if (dbEntities.Any() || !data.Any()) return dbEntities;
            data.ForEach(d => repo.Create(d));
            return data;
        }
    }
}
