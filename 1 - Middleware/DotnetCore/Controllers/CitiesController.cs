using DotnetCore.Models;
using DotnetCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotnetCore.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ILogger _logger;

        public CitiesController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult GetCities()
        {
            return Ok(CityStore.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            _logger.Log("GetCity: " + id);
            return Ok(CityStore.Cities.FirstOrDefault(c => c.ID == id));
        }

        [HttpGet("hello")]
        public IActionResult Hello()
        {            
            return Ok(Startup.Configuration["greeting:msg"]);
        }
    }
}
