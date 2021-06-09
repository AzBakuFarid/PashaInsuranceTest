using PashaInsuranceTest.DTOs.Enums;
using PashaInsuranceTest.DTOs.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class ServiceUpdateDto : IServiceUpdateData
    {
        public int Id { get; set; }
        [Required] [MaxLength(255)] public string Name { get; set; }
        [DataType(DataType.Date)] public DateTime StartsAt { get; set; }
        [DataType(DataType.Date)] public DateTime ValidTill { get; set; }
        [EnumDataType(typeof(ServiceTypeRequestEnum))] public ServiceTypeRequestEnum Type { get; set; }
    }
}
