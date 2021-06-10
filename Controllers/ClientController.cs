using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("[controller]/list")]
        public IActionResult List()
        {
            return Ok(_service.List());
        }

        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(ClientCreateDto model)
        {
            return Ok(_service.Create(model));
        }

        [HttpPost]
        [Route("[controller]/update")]
        public IActionResult Update(ClientUpdateDto model)
        {
            return Ok(_service.Update(model));
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

            return NoContent();
        }

    }
}
