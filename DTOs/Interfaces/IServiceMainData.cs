using PashaInsuranceTest.DTOs.Enums;
using System;

namespace PashaInsuranceTest.DTOs.Interfaces
{
    public interface IServiceMainData
    {
        string Name { get; set; }
        DateTime StartsAt { get; set; }
        DateTime ValidTill { get; set; }
        ServiceTypeRequestEnum Type { get; set; }
    }
}
