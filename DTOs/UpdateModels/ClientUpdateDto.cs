using PashaInsuranceTest.DTOs.Interfaces;
using System;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class ClientUpdateDto : ICLientUpdateData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
