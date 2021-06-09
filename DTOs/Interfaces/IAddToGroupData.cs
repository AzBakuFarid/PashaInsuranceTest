using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.Interfaces
{
    public interface IAddToGroupData<TKey>
    {
        int GroupId { get; set; }
        TKey TargetId { get; set; }

    }
}
