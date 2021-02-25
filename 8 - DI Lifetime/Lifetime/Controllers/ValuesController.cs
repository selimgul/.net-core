using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lifetime.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly OperationService _service;

        public ValuesController(OperationService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return String.Format("TransientOperation: {0}\nScopedOperation: {1}\nSingletonOperation: {2}\nSingletonInstanceOperation: {3}",
                _service.TransientOperation.OperationId.ToString(),
                _service.ScopedOperation.OperationId.ToString(),
                _service.SingletonOperation.OperationId.ToString(),
                _service.SingletonInstanceOperation.OperationId.ToString()
                );
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
