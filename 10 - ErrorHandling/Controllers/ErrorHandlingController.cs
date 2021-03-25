using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics;

namespace ErrorHandling.Controllers
{
    [ApiController]    
    public class ErrorHandlingController : ControllerBase
    {
        [Route("/error-development")]
        public IActionResult ErrorDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {            
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error-non-development")]
        public IActionResult ErrorNonDevelopment()
        {            
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(title: context.Error.Message);
        }
    }
}