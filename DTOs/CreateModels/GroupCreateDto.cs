using PashaInsuranceTest.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.CreateModels
{
    public class GroupCreateDto : IGroupCreateData
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public List<int> Services { get; set; }
        public List<string> Clients { get; set; }

        public GroupCreateDto()
        {
            Services = new List<int>();
            Clients = new List<string>();
        }
    }
}
