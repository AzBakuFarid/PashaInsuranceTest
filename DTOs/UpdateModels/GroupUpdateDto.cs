using PashaInsuranceTest.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class GroupUpdateDto : IGroupUpdateData
    {
        public int Id { get; set; }
        [Required] [MaxLength(255)] public string Name { get; set; }
        [Range(1, 1_000_000)] public decimal Amount { get; set; }
    }
}
