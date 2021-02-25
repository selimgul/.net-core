using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Logging.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILogger _logger2;

        public ValuesController(ILogger<ValuesController> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _logger2 = loggerFactory.CreateLogger(typeof(ValuesController));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {            
            _logger.LogTrace("Trace = 0");
            _logger.LogDebug("Debug = 1");
            _logger.LogInformation("Information = 2");
            _logger.LogWarning("Warning = 3");
            _logger.LogError("Error = 4");
            _logger.LogCritical("Critical = 5");

            //_logger2.LogTrace("Trace = 0");
            //_logger2.LogDebug("Debug = 1");
            //_logger2.LogInformation("Information = 2");
            //_logger2.LogWarning("Warning = 3");
            //_logger2.LogError("Error = 4");
            //_logger2.LogCritical("Critical = 5");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
