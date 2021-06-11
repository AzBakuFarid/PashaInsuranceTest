using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.DTOs.ViewModels;
using PashaInsuranceTest.Exceptions;
using PashaInsuranceTest.Helpers;
using PashaInsuranceTest.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PashaInsuranceTest.Services
{
    public class ServiceService : IServiceService // burda adlandirma cox da ugurlu olmadi, amma ne ise 
    {
        private readonly IBaseRepository _baseRepo;
        public ServiceService (IBaseRepository baseRepo)
        {
            _baseRepo = baseRepo;
        }
        public List<ServiceViewDto> List()
        {
            return _baseRepo.List<Service>("Groups", "Spesifications").Select(s => Mapper.MapServiceToViewModel(s)).ToList();
        }
        public void AddToGroup(IAddToGroupData<int> data)
        {
            var service = GetService(data.TargetId);
            var group = _baseRepo.Find<Group, int>(data.GroupId);
            if (group == null)
            {
                throw new BadRequestException(string.Format(ErrorMessage.DbLookup.DOES_NOT_EXIST_FOR_ID, nameof(Group), data.GroupId));
            }
            if (service.Groups.Contains(group))  // optimizasia ucun
            {
                return;
            }
            service.Groups.Add(group);
            _baseRepo.Update(service);
            _baseRepo.Commit();
        }

        public ServiceViewDto Create(IServiceCreateData data)
        {
            var isServiceExists = _baseRepo.FindByName<Service>(data.Name) != null;
            if (isServiceExists)
            {
                throw new BadRequestException(string.Format(ErrorMessage.DbLookup.EXISTS_WITH_NAME, nameof(Service), data.Name));
            }
            var service = new Service
            {
                Name = data.Name,
                StartsAt = data.StartsAt,
                ValidTill = data.ValidTill,
                Type = Mapper.ServiceRequestTypes[data.Type]
            };

            var groups = _baseRepo.ListByIds<Group, int>(data.Groups);
            if (groups.Any()) groups.ForEach(g => service.Groups.Add(g));

            var spesifications = _baseRepo.ListByIds<Spesification, int>(data.Spesifications);
            if (spesifications.Any()) spesifications.ForEach(s => service.Spesifications.Add(s));

            _baseRepo.Create(service);
            _baseRepo.Commit();
            return Mapper.MapServiceToViewModel(service);

        }

        public void Delete(int id)
        {
            var service = GetService(id);
            _baseRepo.Delete(service);
            _baseRepo.Commit();
        }

        public void RemoveFromGroup(IAddToGroupData<int> data)
        {
            var service = GetService(data.TargetId, "Groups");
            var group = service.Groups.FirstOrDefault(w => w.Id == data.GroupId);
            if (group == null)
            {
                return; // group tapilmadisa, demek ki relation da yoxdu. test app-da bunu ok kimi qebul elemek olar
            }

            service.Groups.Remove(group);
            _baseRepo.Update(service);
            _baseRepo.Commit();

        }

        public ServiceViewDto Update(IServiceUpdateData data)
        {
            var service = GetService(data.Id);
            var serviceWithNewName = _baseRepo.FindByName<Service>(data.Name);

            if (serviceWithNewName != null && serviceWithNewName.Id != service.Id)
            {
                throw new BadRequestException(string.Format(ErrorMessage.DbLookup.EXISTS_WITH_NAME, nameof(Service), data.Name));
            }
            service.Name = data.Name;
            service.StartsAt = data.StartsAt;
            service.ValidTill = data.ValidTill;
            service.Type = Mapper.ServiceRequestTypes[data.Type];

            _baseRepo.Update(service);
            _baseRepo.Commit();

            return Mapper.MapServiceToViewModel(GetService(service.Id, "Groups", "Spesifications"));

        }
        private Service GetService(int id, params string[] includes)
        {
            return _baseRepo.Find<Service, int>(id, includes) ?? throw new NotFoundException(string.Format(ErrorMessage.DbLookup.DOES_NOT_EXIST_FOR_ID, nameof(Service), id));
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////// 
    public interface IServiceService
    {
        ServiceViewDto Create(IServiceCreateData data);
        ServiceViewDto Update(IServiceUpdateData data);
        void Delete(int id);
        void AddToGroup(IAddToGroupData<int> data);
        void RemoveFromGroup(IAddToGroupData<int> data);
        List<ServiceViewDto> List();
    }
}
