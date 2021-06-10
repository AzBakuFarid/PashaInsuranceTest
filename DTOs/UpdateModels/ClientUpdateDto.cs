using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class ClientUpdateDto : ICLientUpdateData
    {
        public string Id { get; set; }
        [Required] [MaxLength(255)] public string Name { get; set; }
        [Required] [MaxLength(255)] public string Surname { get; set; }
        [Required] public string Password { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
