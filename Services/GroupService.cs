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
    public class GroupService : IGroupService
    {
        private readonly IBaseRepository _baseRepo;
        private readonly IClientRepository _clientRepo;
        public GroupService(IBaseRepository baseRepo, IClientRepository clientRepo)
        {
            _baseRepo = baseRepo;
            _clientRepo = clientRepo;
        }

        public GroupViewDto Create(IGroupCreateData data) {
            var isGroupExists = _baseRepo.FindByName<Group>(data.Name) != null;
            if (isGroupExists)
            {
                throw new BadRequestException(string.Format(ErrorMessage.DbLookup.EXISTS_WITH_NAME, nameof(Group), data.Name));
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
            return Mapper.MapGroupToViewModel(group);
        }

        public void Delete(int id)
        {
            var group = GetGroup(id);
            _baseRepo.Delete(group);
            _baseRepo.Commit();
        }

        public List<GroupViewDto> List()
        {
            return _baseRepo.List<Group>("Services", "Clients").Select(c => Mapper.MapGroupToViewModel(c)).ToList();
        }

        public GroupViewDto Update(IGroupUpdateData data) {
            var group = GetGroup(data.Id);
            var groupWithNewName = _baseRepo.FindByName<Group>(data.Name);
            if (groupWithNewName != null && groupWithNewName.Id != data.Id)
            {
                throw new BadRequestException(string.Format(ErrorMessage.DbLookup.EXISTS_WITH_NAME, nameof(Group), data.Name));
            }
            group.Name = data.Name;
            group.Amount = data.Amount;

            _baseRepo.Update(group);
            _baseRepo.Commit();
            return Mapper.MapGroupToViewModel(group);

        }
        private Group GetGroup(int id) {
            return _baseRepo.Find<Group, int>(id) ?? throw new NotFoundException(string.Format(ErrorMessage.DbLookup.DOES_NOT_EXIST_FOR_ID, nameof(Group), id));
        }
    }
    ///////////////////////////////////////////////////////////////////// 
    public interface IGroupService {
        GroupViewDto Create(IGroupCreateData data);
        GroupViewDto Update(IGroupUpdateData data);
        void Delete(int id);
        List<GroupViewDto> List();
    }
}
