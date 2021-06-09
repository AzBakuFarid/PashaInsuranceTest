using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDbEntity> AddInclude<TDbEntity>(this IQueryable<TDbEntity> query, params string[] includes) where TDbEntity : class
        {
            foreach (var inclue in includes)
            {
                query = query.Include(inclue);
            }
            return query;
        }
    }
}
