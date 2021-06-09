using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.DTOs.ViewModels;
using PashaInsuranceTest.Exceptions;
using PashaInsuranceTest.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PashaInsuranceTest.Services
{
    public class GroupService : IGroupService
    {
        private readonly IBaseRepository _baseRepo;
        private readonly IClientRepository _clientRepo;
        public GroupService(IBaseRepository baseRepo, IClientRepository clientRepo)
        {
            _baseRepo = baseRepo;
            _clientRepo = clientRepo;
        }

        public void Create(IGroupCreateData data) {
            var isGroupExists = _baseRepo.FindByName<Group>(data.Name) != null;
            if (isGroupExists)
            {
                throw new BadRequestException($"Group with name {data.Name} exists");
            }
            var group = new Group
            {
                Name = data.Name,
                Amount = data.Amount
            };

            var services = _baseRepo.ListByIds<Service, int>(data.Services);
            if (services.Any()) services.ForEach(s => group.Services.Add(s));

            var clients = _clientRepo.ListByIds(data.Clients);
            if (clients.Any()) clients.ForEach(c => group.Clients.Add(c));

            _baseRepo.Create(group);
            _baseRepo.Commit();
        }

        public void Delete(int id)
        {
            var group = GetGroup(id);
            _baseRepo.Delete(group);
            _baseRepo.Commit();
        }

        public List<GroupViewDto> List()
        {
            return _baseRepo.List<Group>("Services", "Clients")
                .Select(g => new GroupViewDto {
                    Id = g.Id,
                    Name = g.Name,
                    Amount = g.Amount,
                    Clients = g.Clients.Select(c => new InnerData<string> { Id = c.Id, Name = c.Name }).ToList(),
                    Services = g.Services.Select(s => new InnerData<int> { Id = s.Id, Name = s.Name }).ToList()
                })
                .ToList();
        }

        public void Update(IGroupUpdateData data) {
            var group = GetGroup(data.Id);
            var groupWithNewName = _baseRepo.FindByName<Group>(data.Name);
            if (groupWithNewName != null && groupWithNewName.Id != data.Id)
            {
                throw new BadRequestException($"Group with name {data.Name} exists");
            }
            group.Name = data.Name;
            group.Amount = data.Amount;

            _baseRepo.Update(group);
            _baseRepo.Commit();
        }
        private Group GetGroup(int id) {
            return _baseRepo.Find<Group, int>(id) ?? throw new NotFoundException($"Group by id {id} does not exists");
        }
    }
    ///////////////////////////////////////////////////////////////////// 
    public interface IGroupService {
        void Create(IGroupCreateData data);
        void Update(IGroupUpdateData data);
        void Delete(int id);
        List<GroupViewDto> List();
    }
}
