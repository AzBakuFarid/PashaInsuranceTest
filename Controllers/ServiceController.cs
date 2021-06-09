using Microsoft.AspNetCore.Mvc;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.DTOs.CreateModels;
using PashaInsuranceTest.DTOs.UpdateModels;
using PashaInsuranceTest.Helpers;
using PashaInsuranceTest.Repository;
using PashaInsuranceTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Controllers
{
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
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
        public IActionResult Create(ServiceCreateDto model) 
        {
            _service.Create(model);

            return Ok(); 
        }

        [HttpPost]
        [Route("[controller]/update/{id}")]
        public IActionResult Update(ServiceUpdateDto model) 
        {
            _service.Update(model);

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/addToGroup")]
        public IActionResult AddServiceToGroup(AddServiceToGroupDto model)
        {
            _service.AddToGroup(model);
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/removeFromGroup")]
        public IActionResult RemoveFromGroup(AddServiceToGroupDto model)
        {
            _service.RemoveFromGroup(model);

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/delete/{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);

            return Ok();
        }
    }
}
