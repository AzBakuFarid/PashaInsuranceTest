using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.Interfaces;
using PashaInsuranceTest.Exceptions;
using PashaInsuranceTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var oldGroup = _baseRepo.FindByName<Group>(data.Name);
            if (oldGroup != null)
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
    }
    ///////////////////////////////////////////////////////////////////// 
    public interface IGroupService {
        void Create(IGroupCreateData data);
    }
}
