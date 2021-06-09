using PashaInsuranceTest.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class AddServiceToGroupDto : IAddToGroupData<int>
    {
        [Range(1, int.MaxValue)] public int TargetId { get; set; }
        [Range(1, int.MaxValue)] public int GroupId { get; set; }
    }
}
