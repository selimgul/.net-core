using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IHubContext<QuoteHub> _hubContext;

        public ValuesController(IHubContext<QuoteHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Hello from WebApi Controller", id.ToString()).Wait();
            return new string[] { "value1", "value2" };
        }      
    }
}
