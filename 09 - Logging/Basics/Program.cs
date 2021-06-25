using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Logging
{
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0

    // ILogger, ILoggerFactory

    public class Program
    {
        /*
        2. Loglama konfigurasyonu genellikle appsettings.{environment}.json dosyasında bulunur.
           "Logging" altında provider özelinde tanımlama yapılabilir. Özel bir tanım verilmediğinde tüm provider'lara uygulanır.
           "Default", "Microsoft" gibi ifadeler kategoriyi belirler. Daha spesifik tanım varsa ("Microsoft.Hosting.Lifetime" gibi) bu geçerli olur.
           Çağrılan log metodu seviyesi tanımlanan seviyeye eşit ya da büyükse loglama yapılır.
            
        "Logging": {    
            "LogLevel": {
                "Default": "Information",
                "Microsoft": "Warning",
                "Microsoft.Hosting.Lifetime": "Information"
            }
        }

        LogLevel:
            Trace       => 0
            Debug       => 1
            Information => 2
            Warning     => 3
            Error       => 4
            Critical    => 5
            None        => 6        

        3. Logging kırılımından sonra provider ismi verilerek LogLevel ve Kategori değerleri override edilebilir.

        {
        "Logging": {
            "LogLevel": { // All providers, LogLevel applies to all the enabled providers.
                "Default": "Error", // Default logging, Error and higher.
                "Microsoft": "Warning" // All Microsoft* categories, Warning and higher.
            },
            "Debug": { // Debug provider.
                "LogLevel": {
                    "Default": "Information", // Overrides preceding LogLevel:Default setting.
                    "Microsoft.Hosting": "Trace" // Debug:Microsoft.Hosting category.
                }       
            }
        }

        4. LogLevel çeşitli kanallardan set edilebilir.
           Environment variable => set Logging__LogLevel__Default=Error

        5. Logger'lar bir kategori ile ilişkilidirler ve genelde içinde bulundukları class adıdır.
           Eğer custom bir kategori verilmesi istenirse ILoggerFactory DI ile alınıp istenen isimle logger yaratılabilir.

            private readonly ILogger _logger;

            public ContactModel(ILoggerFactory logger)
            {
                _logger = logger.CreateLogger("MyCategory");
            } 

        7. Host olmayan uygulamalarda gerekli düzenlemeler LoggerFactory üzerinden yapılabilir.
                using var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                        .AddConsole()
                        .AddEventLog();
                });
                ILogger logger = loggerFactory.CreateLogger<Program>();
                logger.LogInformation("Example log message");
        
        */

        public static void Main(string[] args)
        {            
            CreateHostBuilder(args).Build().Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            // 1. ConfigureLogging ile loglama altyapısı üzerindeki ayarlamalar yapılabilir.
            // Default olarak eklenen provider'lar (Console, Debug, EventSource, EventLog) çıkarılabilir.
            // İstenen provider'lar eklenebilir.
            .ConfigureLogging(logging =>
            {
                // logging.ClearProviders();
                // logging.AddConsole();
                // logging.AddEventLog(options => { options.SourceName = "MyApp"; });

                // 6. Konfigurasyonla yapılan işlemi AddFilter ile de gerçeklenebilir.
                // Her loglama çağrımında filter devreye girer ve true dönülenler için loglama yapılır, false dönülenler için yapılmaz.
                // logging.AddFilter((provider, category, logLevel) => {

                //     Console.WriteLine($"provider: {provider}, category: {category}, logLevel: {logLevel}");
                //     Console.WriteLine("-----------------------------------------------------------------");
                //     return true;
                // });
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
