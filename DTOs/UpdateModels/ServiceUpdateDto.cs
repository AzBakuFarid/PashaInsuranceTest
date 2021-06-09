using PashaInsuranceTest.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class ServiceUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime ValidTill { get; set; }
        public ServiceTypeRequestEnum Type { get; set; }
    }
}
