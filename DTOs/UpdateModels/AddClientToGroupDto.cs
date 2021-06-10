using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class AddClientToGroupDto : IAddToGroupData<string>
    {
        [Required] public string TargetId { get; set; } 
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.AttributeError.INVALID_PRIMARY_KEY)] public int GroupId { get; set; }
    }
}
