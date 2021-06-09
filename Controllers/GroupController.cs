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
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("[controller]/list")]
        public IActionResult List()
        {
            return Ok(_service.List());
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
            _service.Update(model);

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/delete/{id}")]
        public IActionResult Delete(int id) {

            _service.Delete(id);

            return Ok();
        }
    }
}
