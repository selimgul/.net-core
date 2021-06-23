using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Basics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILog log)
        {
            _logger = logger;
            
            log.info("Constructor");            
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromServices] ILog log)
        {
            log.info("Get");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        
        [HttpGet]
        [Route("foo")]
        public String foo()
        {
            var services = this.HttpContext.RequestServices;
            var log = (ILog)services.GetService(typeof(ILog));

            log.info("foo");
            return "foo";           
        }

         [HttpGet]
        [Route("bar")]
        public String bar([FromServices] IEnumerable<ILog> logServices)
        {
            foreach (var log in logServices)
            {
                log.info("bar");    
            }
            
            return "bar";           
        }
    }
}
