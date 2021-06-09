using Microsoft.AspNetCore.Mvc;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.CreateModels;
using PashaInsuranceTest.DTOs.UpdateModels;
using PashaInsuranceTest.Extensions;
using PashaInsuranceTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IBaseRepository _baseRepo;
        private readonly IClientRepository _clientRepo;

        public ClientController(IBaseRepository baseRepo, IClientRepository clientRepo)
        {
            _baseRepo = baseRepo;
            this._clientRepo = clientRepo;
        }


        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(ClientCreateDto model)
        {
            var client = new AppUser {
                UserName = model.Email,
                IsClient = true,
                Name = model.Name,
                Surname = model.Surname,
                Birthday = model.Birthday, 
                Email = model.Email
            };
            if (model.Group.HasValue)
            {
                var group = _baseRepo.Find<Group, int>(model.Group.Value);
                if (group == null)
                {
                    return BadRequest(new { error = $"The group by id {model.Group.Value} does not exists" });
                }
                client.Group = group;
            }

            var result = _clientRepo.Create(client, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors.Select(s => s.Description)});
            }

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/update/{id}")]
        public IActionResult Update(ClientUpdateDto model)
        {
            var client = _clientRepo.Find(model.Id);
            if (client == null)
            {
                return NotFound(new { error = $"The client by id {model.Id} does not exists" });
            }
            client.Name = model.Name;
            client.Surname = model.Name;
            client.Birthday = model.Birthday;
            client.Email = model.Email;
            var result = _clientRepo.Update(client);
            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors.Select(s => s.Description) });
            }
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/addToGroup")]
        public IActionResult AddClientToGroup(AddClientToGroupDto model)
        {
            var client = _clientRepo.Find(model.Client);
            if (client == null)
            {
                return NotFound(new { error = $"The client by id {model.Client} does not exists" });
            }
            var group = _baseRepo.Find<Group, int>(model.Group, "Clients");
            if (group == null)
            {
                return BadRequest(new { error = $"The group by id {model.Group} does not exists" });
            }
            if (group.Clients.Contains(client))  // optimizasia ucun
            {
                return Ok();
            }
            client.Group = group;
            _clientRepo.Update(client);
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/removeFromGroup")]
        public IActionResult RemoveFromGroup(AddClientToGroupDto model)
        {
            var client = _clientRepo.Find(model.Client);
            if (client == null)
            {
                return NotFound(new { error = $"The client by id {model.Client} does not exists" });
            }
            if (client.Group == null || client.Group.Id != model.Group)
            {
                return Ok(); // group-dan cixarmaq isteyirdi, group tapilmadisa, demek ki relation da yoxdu. test app-da bunu ok kimi qebul elemek olar
            }

            client.Group = null;
            _clientRepo.Update(client);

            return Ok();
        }

    }
}
