using Microsoft.AspNetCore.Mvc;
using PashaInsuranceTest.DTOs.CreateModels;
using PashaInsuranceTest.DTOs.UpdateModels;
using PashaInsuranceTest.Services;


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
            return Ok(_service.Create(model));
        }

        [HttpPost]
        [Route("[controller]/update")]
        public IActionResult Update(GroupUpdateDto model)
        {
            return Ok(_service.Update(model));
        }

        [HttpPost]
        [Route("[controller]/delete/{id}")]
        public IActionResult Delete(int id) {

            _service.Delete(id);

            return NoContent();
        }
    }
}
