using System.Collections.Generic;

namespace PashaInsuranceTest.DTOs.Interfaces
{
    public interface IServiceAdvancedData
    {
        List<int> Groups { get; set; }
        List<int> Spesifications { get; set; }
    }
}
