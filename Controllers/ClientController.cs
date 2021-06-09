﻿using Microsoft.AspNetCore.Mvc;
using PashaInsuranceTest.DTOs.CreateModels;
using PashaInsuranceTest.DTOs.UpdateModels;
using PashaInsuranceTest.Services;

namespace PashaInsuranceTest.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            this._service = service;
        }


        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(ClientCreateDto model)
        {
            _service.Create(model);

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/update/{id}")]
        public IActionResult Update(ClientUpdateDto model)
        {
            _service.Update(model);
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/addToGroup")]
        public IActionResult AddToGroup(AddClientToGroupDto model)
        {
            _service.AddToGroup(model);
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/removeFromGroup")]
        public IActionResult RemoveFromGroup(AddClientToGroupDto model)
        {
            _service.RemoveFromGroup(model);

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/delete/{id}")]
        public IActionResult Delete(string id)
        {
            _service.Delete(id);

            return Ok();
        }

    }
}
