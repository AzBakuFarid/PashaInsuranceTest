﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.CreateModels;
using PashaInsuranceTest.Extensions;
using PashaInsuranceTest.Repository;
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
        private readonly UserManager<AppUser> _userManager;

        public GroupController(IBaseRepository repo, UserManager<AppUser> userManager)
        {
            _repo = repo;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(GroupCM model)
        {
            var oldGroup = _repo.FindByName<Group>(model.Name);
            if (oldGroup != null) {
                return BadRequest(new { error = $"The group with name {model.Name} exists" });
            }
            var group = new Group {
                Name = model.Name,
                Amount = model.Amount
            };
            var services = _repo.ListByIds<Service, int>(model.Services);
            var clients = _userManager.Users.Where(w => w.IsClient && model.Clients.Contains(w.Id));
            if (services.Any()) services.ForEach(s => group.Services.Add(s));
            if (clients.Any()) clients.ForEach(c => group.Clients.Add(c));
            _repo.Create(group);
            _repo.Commit();
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/update/{id}")]
        public IActionResult Update(int id, GroupCM model)
        {
            var group = _repo.Find<Group, int>(id, "Clients", "Services.Spesifications");
            var otherGroup = _repo.FindByName<Group>(model.Name);
            if (group == null)
            {
                return NotFound(new { error = $"The group by id {id} does not exists" });
            }
            if (otherGroup != null && otherGroup.Id != id )
            {
                return BadRequest(new { error = $"The group with name {model.Name} exists" });
            }
            group.Name = model.Name;
            group.Amount = model.Amount;

            group.Services.Clear();
            group.Clients.Clear();

            var services = _repo.ListByIds<Service, int>(model.Services);
            var clients = _userManager.Users.Where(w => w.IsClient && model.Clients.Contains(w.Id));

            if (services.Any()) services.ForEach(s => group.Services.Add(s));
            if (clients.Any()) clients.ForEach(c => group.Clients.Add(c));

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
