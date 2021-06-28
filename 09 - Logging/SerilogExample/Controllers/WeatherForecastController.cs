﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;

namespace SerilogExample.Controllers
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
        private readonly IDiagnosticContext _diagnosticContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("WeatherForecastController::Get 1");
            
            var rng = new Random();
            IEnumerable<WeatherForecast> resp = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            // UseSerilogRequestLogging ile eklenen middleware'de yapılacak loglamaya
            // diagnosticContext ile istenen değerler eklenebilir.
            _diagnosticContext.Set("WeatherForecastController::Get::Response", resp);

            LogContext.PushProperty("CorrelationId", HttpContext.GetCorrelationId());

            _logger.LogInformation("WeatherForecastController::Get 2");

            return resp.ToArray();
        }
    }
}
