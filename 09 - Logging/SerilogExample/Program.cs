using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace SerilogExample
{
    // https://serilog.net/
    // https://github.com/serilog/serilog-aspnetcore
    // https://benfoster.io/blog/serilog-best-practices/

    public class Program
    {
        public static void Main(string[] args)
        {            
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Error)             
             .Enrich.FromLogContext()
             .Enrich.WithProperty("Environment", Environment.MachineName)
             .Enrich.With(new ThreadEnricher())
             .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
             {
                 AutoRegisterTemplate = true,
                 IndexFormat = $"serilog-example-app-{DateTime.UtcNow:yyyy-MM}"
             })
            // .WriteTo.Console(new RenderedCompactJsonFormatter()) // Console, Debug ve File için JSON formatında çıktılar üretilebilir.
            .WriteTo.Console()
             .CreateLogger();

            Log.Information($"Server started.");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
