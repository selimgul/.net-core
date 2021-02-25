using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var username = context.Request.Query["username"];

            if (!String.IsNullOrWhiteSpace(username) &&
                username == "selim")
                await  _next(context);
            else
                await context.Response.WriteAsync("Not authorized");
        }
    }
}
