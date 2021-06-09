using PashaInsuranceTest.DTOs.Interfaces;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class GroupUpdateDto : IGroupUpdateData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
