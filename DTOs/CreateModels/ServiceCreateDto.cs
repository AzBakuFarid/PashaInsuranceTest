using PashaInsuranceTest.DbEntities.Enums;
using PashaInsuranceTest.DTOs.Enums;
using PashaInsuranceTest.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.CreateModels
{
    public class ServiceCreateDto : IServiceCreateData
    {
        public string Name { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime ValidTill { get; set; }
        public ServiceTypeRequestEnum Type { get; set; }

        public List<int> Groups { get; set; }
        public List<int> Spesifications { get; set; }

        public ServiceCreateDto()
        {
            Groups = new List<int>();
            Spesifications = new List<int>();
        }
    }
}
