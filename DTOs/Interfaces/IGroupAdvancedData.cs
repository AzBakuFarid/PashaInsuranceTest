using System.Collections.Generic;

namespace PashaInsuranceTest.DTOs.Interfaces
{
    public interface IGroupAdvancedData
    {
        List<int> Services { get; set; }
        List<string> Clients { get; set; }
    }
}
