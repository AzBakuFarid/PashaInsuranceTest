using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.CreateModels;
using PashaInsuranceTest.DTOs.UpdateModels;
using PashaInsuranceTest.Extensions;
using PashaInsuranceTest.Repository;
using PashaInsuranceTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Controllers
{
    [ApiController]
    public class GroupController: ControllerBase
    {
        private readonly IBaseRepository _repo;
        private readonly IClientRepository _clientRepo;
        private readonly IGroupService _service;

        public GroupController(IBaseRepository repo, IClientRepository clientRepo, IGroupService service)
        {
            _repo = repo;
            this._clientRepo = clientRepo;
            _service = service;
        }

        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(GroupCreateDto model)
        {
            _service.Create(model);

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/update/{id}")]
        public IActionResult Update(GroupUpdateDto model)
        {
            var group = _repo.Find<Group, int>(model.Id, "Clients", "Services.Spesifications");
            var otherGroup = _repo.FindByName<Group>(model.Name);
            if (group == null)
            {
                return NotFound(new { error = $"The group by id {model.Id} does not exists" });
            }
            if (otherGroup != null && otherGroup.Id != model.Id )
            {
                return BadRequest(new { error = $"The group with name {model.Name} exists" });
            }
            group.Name = model.Name;
            group.Amount = model.Amount;

            _repo.Update(group);
            _repo.Commit();

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/delete/{id}")]
        public IActionResult Delete(int id) {
            var group = _repo.Find<Group, int>(id);
            if (group == null)
            {
                return NotFound(new { error = $"The group by id {id} does not exists" });
            }
            _repo.Delete(group);
            _repo.Commit();
            return Ok();
        }
    }
}
