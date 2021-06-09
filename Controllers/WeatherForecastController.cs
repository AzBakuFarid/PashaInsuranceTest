using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PashaInsuranceTest.Data;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.Repository;

namespace PashaInsuranceTest.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBaseRepository _repo;
        private readonly IClientRepository _clientrepo; 
        private readonly AppDbContext _dbContext;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IBaseRepository repo, AppDbContext dbContext, IClientRepository clientrepo)
        {
            _repo = repo;
            _dbContext = dbContext;
            _clientrepo = clientrepo;
        }

        [HttpGet]
        [Route("home/index")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var users = _clientrepo.List();
            var groups = _dbContext.groups.Include("Services.Spesifications").Include("Clients").ToList();
            var services = _dbContext.services.Include("Spesifications").ToList();
            var spesi = _dbContext.spesifications.ToList();
            return Ok(users.Select(s => new {user = s.Id, group = s.Group?.Name ?? "bosdur" }));
        }
    }
}
