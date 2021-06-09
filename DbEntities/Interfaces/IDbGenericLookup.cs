using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DbEntities.Interfaces
{
    public interface IDbGenericLookup<TKeyType> : IKey<TKeyType>, IName
    {
    }
}
