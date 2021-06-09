using Microsoft.AspNetCore.Mvc;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.CreateModels;
using PashaInsuranceTest.DTOs.UpdateModels;
using PashaInsuranceTest.Helpers;
using PashaInsuranceTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Controllers
{
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IBaseRepository _baseRepo;

        public ServiceController(IBaseRepository baseRepo)
        {
            _baseRepo = baseRepo;
        }

        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(ServiceCreateDto model) 
        {
            var isServiceExists = _baseRepo.FindByName<Service>(model.Name) != null;
            if (isServiceExists)
            {
                return BadRequest(new { error = $"Service with name {model.Name} exists" });
            }
            var service = new Service {
                Name = model.Name,
                StartsAt = model.StartsAt,
                ValidTill = model.ValidTill,
                Type = Mapper.ServiceRequestTypes[model.Type]
            };

            var groups = _baseRepo.ListByIds<Group, int>(model.Groups);
            if (groups.Any()) groups.ForEach(g => service.Groups.Add(g));

            var spesifications = _baseRepo.ListByIds<Spesification, int>(model.Spesifications);
            if (spesifications.Any()) spesifications.ForEach(s => service.Spesifications.Add(s));

            _baseRepo.Create(service);
            _baseRepo.Commit();

            return Ok(); 
        }

        [HttpPost]
        [Route("[controller]/update/{id}")]
        public IActionResult Update(ServiceUpdateDto model) 
        {
            var service = _baseRepo.Find<Service, int>(model.Id);
            var otherService = _baseRepo.FindByName<Service>(model.Name);
            if (otherService == null)
            {
                return NotFound(new { error = $"Service by id {model.Id} does not exists" });
            }
            if (otherService.Id != service.Id)
            {
                return BadRequest(new { error = $"Service with name {model.Name} exists" });
            }
            service.Name = model.Name;
            service.StartsAt = model.StartsAt;
            service.ValidTill = model.ValidTill;
            service.Type = Mapper.ServiceRequestTypes[model.Type];

            _baseRepo.Update(service);
            _baseRepo.Commit();

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/addToGroup")]
        public IActionResult AddServiceToGroup(AddServiceToGroupDto model)
        {
            var service = _baseRepo.Find<Service, int>(model.Service, "Groups");
            if (service == null)
            {
                return NotFound(new { error = $"Service by id {model.Service} does not exists" });
            }
            var group = _baseRepo.Find<Group, int>(model.Group);
            if (group == null)
            {
                return BadRequest(new { error = $"The group by id {model.Group} does not exists" });
            }
            if (service.Groups.Contains(group))  // optimizasia ucun
            {
                return Ok();
            }
            service.Groups.Add(group);
            _baseRepo.Update(service);
            _baseRepo.Commit();
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/removeFromGroup")]
        public IActionResult RemoveFromGroup(AddServiceToGroupDto model)
        {
            var service = _baseRepo.Find<Service, int>(model.Service, "Groups");
            if (service == null)
            {
                return NotFound(new { error = $"Service by id {model.Service} does not exists" });
            }
            var group = service.Groups.FirstOrDefault(w => w.Id == model.Group);
            if (group == null)
            {
                return Ok(); // group tapilmadisa, demek ki relation da yoxdu. test app-da bunu ok kimi qebul elemek olar
            }

            service.Groups.Remove(group);
            _baseRepo.Update(service);
            _baseRepo.Commit();

            return Ok();
        }
    }
}
