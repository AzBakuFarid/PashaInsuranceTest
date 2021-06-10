using PashaInsuranceTest.DbEntities.Enums;
using PashaInsuranceTest.DTOs.Enums;
using PashaInsuranceTest.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using PashaInsuranceTest.Helpers;

namespace PashaInsuranceTest.DTOs.CreateModels
{
    public class ServiceCreateDto : IServiceCreateData
    {
        [Required] [MaxLength(255)] public string Name { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime ValidTill { get; set; }
        [EnumDataType(typeof(ServiceTypeRequestEnum), ErrorMessage = ErrorMessage.AttributeError.INCORRECT_CHOISE)] public ServiceTypeRequestEnum Type { get; set; }

        public List<int> Groups { get; set; }
        public List<int> Spesifications { get; set; }

        public ServiceCreateDto()
        {
            Groups = new List<int>();
            Spesifications = new List<int>();
        }
    }
}
