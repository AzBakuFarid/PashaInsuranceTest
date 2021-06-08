using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.Repository;

namespace PashaInsuranceTest.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBaseRepository repo;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IBaseRepository _repo)
        {
            repo = _repo;
        }

        [HttpGet]
        [Route("home/index")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var result = repo.List<Group>();
            return Ok(result.Select(s => s.Name));
        }
    }
}
