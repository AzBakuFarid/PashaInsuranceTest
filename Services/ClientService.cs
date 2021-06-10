using Microsoft.AspNetCore.Identity;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.DTOs.ViewModels;
using PashaInsuranceTest.Exceptions;
using PashaInsuranceTest.Helpers;
using PashaInsuranceTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PashaInsuranceTest.Services
{
    public class ClientService : IClientService
    {
        private readonly IBaseRepository _baseRepo;
        private readonly IClientRepository _clientRepo;
        public ClientService(IBaseRepository baseRepo, IClientRepository clientRepo)
        {
            _baseRepo = baseRepo;
            _clientRepo = clientRepo;
        }

        public ClientViewDto Create(IClientCreateData data)
        {
            var client = new AppUser
            {
                UserName = data.Email,
                IsClient = true,
                Name = data.Name,
                Surname = data.Surname,
                Birthday = data.Birthday,
                Email = data.Email
            };
            if (data.Group.HasValue)
            {
                client.Group = _baseRepo.Find<Group, int>(data.Group.Value) ?? throw new BadRequestException($"Group by id {data.Group.Value} does not exists");
            }
            ExecuteFunction(() => _clientRepo.Create(client, data.Password));
            return Mapper.MapClientToViewModel(client);
        }

        public void Delete(string id)
        {
            var client = GetClient(id);
            ExecuteFunction(() => _clientRepo.Delete(client));
        }

        public ClientViewDto Update(ICLientUpdateData data)
        {
            var client = GetClient(data.Id);

            client.Name = data.Name;
            client.Surname = data.Surname;
            client.Birthday = data.Birthday;
            client.Email = data.Email;
            ExecuteFunction(() => _clientRepo.Update(client));
            return Mapper.MapClientToViewModel(client);
        }

        public void AddToGroup(IAddToGroupData<string> data) {
            var client = GetClient(data.TargetId);
            var group = _baseRepo.Find<Group, int>(data.GroupId, "Clients") ?? throw new BadRequestException($"Group by id {data.GroupId} does not exists");

            if (group.Clients.Contains(client))  // optimizasia ucun
            {
                return;
            }

            client.Group = group;
            ExecuteFunction(() => _clientRepo.Update(client));
        }

        public void RemoveFromGroup(IAddToGroupData<string> data)
        {
            var client = GetClient(data.TargetId);
            var group = _baseRepo.Find<Group, int>(data.GroupId, "Clients") ?? throw new BadRequestException($"Group by id {data.GroupId} does not exists");

            if (client.Group == null || client.Group.Id != data.GroupId)
            {
                return; // group-dan cixarmaq isteyirdi, group tapilmadisa, demek ki relation da yoxdu. test app-da bunu ok kimi qebul elemek olar
            }

            client.Group = null;
            client.GroupId = null;

            ExecuteFunction(() => _clientRepo.Update(client));

        }

        public List<ClientViewDto> List()
        {
            return _clientRepo.List().Select(c => Mapper.MapClientToViewModel(c)).ToList();
        }
        private AppUser GetClient(string clientId) {
            return _clientRepo.Find(clientId) ?? throw new NotFoundException($"Client by id {clientId} does not exists");
        }
        private void ExecuteFunction(Func<IdentityResult> method) {
            var result = method();
            if (!result.Succeeded)
            {
                throw new BadRequestException(String.Join(Environment.NewLine, result.Errors.Select(s => s.Description)));
            }
        }
    }
    ////////////////////////////////////////////////////////////////////////// 
    public interface IClientService
    {
        ClientViewDto Create(IClientCreateData data);
        ClientViewDto Update(ICLientUpdateData data);
        void Delete(string id);
        void AddToGroup(IAddToGroupData<string> data);
        void RemoveFromGroup(IAddToGroupData<string> data);
        List<ClientViewDto> List();
    }
}
