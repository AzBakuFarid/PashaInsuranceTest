using PashaInsuranceTest.DTOs.Enums;
using PashaInsuranceTest.DTOs.Interfaces;
using System;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class ServiceUpdateDto : IServiceUpdateData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime ValidTill { get; set; }
        public ServiceTypeRequestEnum Type { get; set; }
    }
}
