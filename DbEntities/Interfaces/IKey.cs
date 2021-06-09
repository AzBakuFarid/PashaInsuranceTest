using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DbEntities.Interfaces
{
    public interface IKey<TKeyType>
    {
        TKeyType Id { get; set; }
    }
}
