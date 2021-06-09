using PashaInsuranceTest.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace PashaInsuranceTest.DTOs.CreateModels
{
    public class GroupCreateDto : IGroupCreateData
    {
        [Required] [MaxLength(255)] public string Name { get; set; }
        [Range(1, 1_000_000)] public decimal Amount { get; set; }
        public List<int> Services { get; set; }
        public List<string> Clients { get; set; }

        public GroupCreateDto()
        {
            Services = new List<int>();
            Clients = new List<string>();
        }
    }
}
