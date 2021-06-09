using PashaInsuranceTest.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.CreateModels
{
    public class ClientCreateDto : IClientCreateData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int? Group { get; set; }
    }
}
