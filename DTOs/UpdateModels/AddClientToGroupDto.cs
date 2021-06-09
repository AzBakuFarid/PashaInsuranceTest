using PashaInsuranceTest.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class AddClientToGroupDto : IAddToGroupData<string>
    {
        [Required] public string TargetId { get; set; } 
        [Range(1, int.MaxValue)] public int GroupId { get; set; }
    }
}
