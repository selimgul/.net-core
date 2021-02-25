using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace WebApi
{
    public class Startup
    {
        // * Formatters
        //   - Produces,Consumes
        //   - FormatFilter
        //
        // * Action return types => Specific type, IActionResult, ActionResult, ActionResult<T>
        //   - Status Code Results => StatusCode,OkResult,CreatedResult,NoContentResult,
        //                            BadRequestResult,UnauthorizedResult,NotFoundResult,UnsupportedMediaTypeResult
        //   - Status Code with Object Results => ObjectResult, OkObjectResult,CreateObjectResult,
        //                                        BadRequestObjectResult,NotFoundObjectRequest
        //   - Redirect Results => RedirectResult,LocalRedirectResult,RedirectToActionResult,RedirectToRouteResult
        //   - File Results => FileResult,FileContentResult,VirtualFileResult,PhysicalFileResult 
        //   - Content Results => ViewResult,PartialViewResult,JsonResult,ContentResult 
        //
        // * Binding sources => [FromBody], [FromForm], [FromHeader], [FromQuery], [FromRoute], [FromServices]
        // 
        // * CORS
        //
        // * Authentication
        //   - Middleware
        //   - Handler : AuthenticationHandler
        //   - Policy
        //   - JWT
        //
        // * In-memory Cache

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string DefaultPolicyName { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Accept header'ı ile kullanım için options kullanılır.
            // FormatFilter ile kullanım için AddFormatterMappings() kullanılır.
            services.AddMvc(options =>
            {                             
                // To add XmlSerializer based Input and Output formatters.
                options.InputFormatters.Add(new XmlSerializerInputFormatter());
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            }).AddFormatterMappings(map => { map.SetMediaTypeMappingForFormat("xml",
                                                                               new MediaTypeHeaderValue("application/xml")); });            
            services.AddCors(options => {                
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin();                    
                });
            });

            // Authentication 2
            //services.AddAuthentication("BasicAuth")
            //  .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuth", null);

            // Authentication 3
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AtLeast21", policy =>
            //        policy.Requirements.Add(new MinimumAgeRequirement(21)));
            //});

            //services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Authentication 1
            //app.UseMiddleware<AuthenticationMiddleware>();

            // Authentication 2
            //app.UseAuthentication();
            
            app.UseMvc();            
        }
    }
}
