using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.Exceptions;
using PashaInsuranceTest.Helpers;
using PashaInsuranceTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Services
{
    public class ServiceService : IServiceService // burda adlandirma cox da ugurlu olmadi, amma ne ise 
    {
        private readonly IBaseRepository _baseRepo;
        public ServiceService (IBaseRepository baseRepo)
        {
            _baseRepo = baseRepo;
        }

        public void AddToGroup(IAddToGroupData<int> data)
        {
            var service = GetService(data.TargetId);
            var group = service.Groups.FirstOrDefault(w => w.Id == data.GroupId);
            if (group == null)
            {
                throw new BadRequestException($"Group by id {data.GroupId} does not exists");
            }
            if (service.Groups.Contains(group))  // optimizasia ucun
            {
                return;
            }
            service.Groups.Add(group);
            _baseRepo.Update(service);
            _baseRepo.Commit();
        }

        public void Create(IServiceCreateData data)
        {
            var isServiceExists = _baseRepo.FindByName<Service>(data.Name) != null;
            if (isServiceExists)
            {
                throw new BadRequestException($"Service with name {data.Name} exists");
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
        }

        public void Delete(int id)
        {
            var service = GetService(id);
            _baseRepo.Delete(service);
            _baseRepo.Commit();
        }

        public void RemoveFromGroup(IAddToGroupData<int> data)
        {
            var service = GetService(data.TargetId);
            var group = service.Groups.FirstOrDefault(w => w.Id == data.GroupId);
            if (group == null)
            {
                return; // group tapilmadisa, demek ki relation da yoxdu. test app-da bunu ok kimi qebul elemek olar
            }

            service.Groups.Remove(group);
            _baseRepo.Update(service);
            _baseRepo.Commit();
        }

        public void Update(IServiceUpdateData data)
        {
            var service = GetService(data.Id);
            var otherService = _baseRepo.FindByName<Service>(data.Name);

            if (otherService.Id != service.Id)
            {
                throw new BadRequestException($"Service with name {data.Name} exists");
            }
            service.Name = data.Name;
            service.StartsAt = data.StartsAt;
            service.ValidTill = data.ValidTill;
            service.Type = Mapper.ServiceRequestTypes[data.Type];

            _baseRepo.Update(service);
            _baseRepo.Commit();
        }
        private Service GetService(int id)
        {
            return _baseRepo.Find<Service, int>(id, "Groups") ?? throw new NotFoundException($"Service by id {id} does not exists");
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////// 
    public interface IServiceService
    {
        void Create(IServiceCreateData data);
        void Update(IServiceUpdateData data);
        void Delete(int id);
        void AddToGroup(IAddToGroupData<int> data);
        void RemoveFromGroup(IAddToGroupData<int> data);
    }
}
