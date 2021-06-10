using PashaInsuranceTest.DTOs.Enums;
using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class ServiceUpdateDto : IServiceUpdateData
    {
        public int Id { get; set; }
        [Required] [MaxLength(255)] public string Name { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime ValidTill { get; set; }
        [EnumDataType(typeof(ServiceTypeRequestEnum), ErrorMessage = ErrorMessage.AttributeError.INCORRECT_CHOISE)] public ServiceTypeRequestEnum Type { get; set; }
    }
}
