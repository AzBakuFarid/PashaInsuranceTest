using PashaInsuranceTest.DbEntities.Enums;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.Enums;
using PashaInsuranceTest.DTOs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Helpers
{
    public class Mapper
    {
        public static Dictionary<ServiceTypeRequestEnum, ServiceTypeEnum> ServiceRequestTypes;

        static Mapper()
        {
            ServiceRequestTypes = new Dictionary<ServiceTypeRequestEnum, ServiceTypeEnum> {
                { ServiceTypeRequestEnum.Type1, ServiceTypeEnum.Type1 },
                { ServiceTypeRequestEnum.Type2, ServiceTypeEnum.Type2 },
                { ServiceTypeRequestEnum.Type3, ServiceTypeEnum.Type3 }
            };
        }

        public static GroupViewDto MapGroupToViewModel(Group model)
        {
            return new GroupViewDto {
                Id = model.Id,
                Amount = model.Amount,
                Name = model.Name,
                Clients = model?.Clients.Select(c => new InnerData<string> { Id = c.Id, Name = c.Email }).ToList(),
                Services = model?.Services.Select(s => new InnerData<int> { Id = s.Id, Name = s.Name }).ToList()
            };
        }
        public static ServiceViewDto MapServiceToViewModel(Service model)
        {
            return new ServiceViewDto
            {
                Id = model.Id,
                StartsAt = model.StartsAt.ToString("dd.MM.yyyy"),
                ValidTill = model.ValidTill.ToString("dd.MM.yyyy"),
                Name = model.Name,
                Type = model.Type.ToString(),
                Groups = model?.Groups.Select(c => new InnerData<int> { Id = c.Id, Name = c.Name }).ToList(),
                Spesifications = model?.Spesifications.Select(s => new InnerData<int> { Id = s.Id, Name = s.Name }).ToList()
            };
        }
        public static ClientViewDto MapClientToViewModel(AppUser model)
        {
            return new ClientViewDto
            {
                Id = model.Id,
                Birthday = model.Birthday?.ToString("dd.MM.yyyy") ?? "qeyd olunmayib",
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email, 
                Group = model.Group == null ? null : new InnerData<int> { Id = model.Group.Id, Name = model.Group.Name }
            };
        }
    }
}
