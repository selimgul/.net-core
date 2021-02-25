using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Config
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigDirect();
            ConfigClass();
            ConfigMemory();
            ConfigCommandLine();

            Console.ReadLine();
        }

        private static void ConfigDirect()
        {
            IConfigurationRoot ConfigurationManager = GetConfig("services1.json");
            
            Console.WriteLine($"server: {ConfigurationManager["ServerName"]}");

            var services = ConfigurationManager.GetSection("Services").AsEnumerable();

            foreach (var service in services.Where(kv => kv.Value != null))
            {
                Console.WriteLine($"service: {service.Key} - {service.Value}");
            }
        }
        private static void ConfigClass()
        {
            IConfigurationRoot ConfigurationManager = GetConfig("services2.json");

            var serviceConfig = new ServiceConfig();
            ConfigurationManager.GetSection("ServiceConfig").Bind(serviceConfig);

            Console.WriteLine($"server name: {serviceConfig.ServerName}");

            foreach (var service in serviceConfig.Services)
            {
                Console.WriteLine($"service: {service.Name} - {service.StartupType}");
            }
        }
        private static void ConfigMemory()
        {
            IConfigurationRoot ConfigurationManager = GetConfig();

            IEnumerable<Service> services = new List<Service>();
            ConfigurationManager.GetSection("Services").Bind(services);

            foreach (var service in services)
            {
                Console.WriteLine($"service: {service.Name} - {service.StartupType}");
            }
        }
        private static void ConfigCommandLine()
        {
            string[] args = new string[] { "Services:0:StartupType=manual" };

            IConfigurationRoot ConfigurationManager = GetConfig(args);

            IEnumerable<Service> services = new List<Service>();
            ConfigurationManager.GetSection("Services").Bind(services);

            foreach (var service in services)
            {
                Console.WriteLine($"service: {service.Name} - {service.StartupType}");
            }
        }

        private static IConfigurationRoot GetConfig(string pConfigFile)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())               
               .AddJsonFile(pConfigFile);
            
            return builder.Build();
        }
        private static IConfigurationRoot GetConfig(string[] args = null)
        {
            var builder = new ConfigurationBuilder();

            var parameters = new Dictionary<string, string>{
                {"Services:0:Name","UniQue.EXE"},
                {"Services:0:StartupType","auto"},
                {"Services:1:Name","UniQue.HPES"},
                {"Services:1:StartupType","manual"},
                {"Services:2:Name","UniQue.WPES"},
                {"Services:2:StartupType","auto"}};

            builder.AddInMemoryCollection(parameters);

            if (args != null)
                builder.AddCommandLine(args);

            return builder.Build();
        }
    }

    public class ServiceConfig
    {
        public string ServerName { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }

    public class Service
    {
        public string Name { get; set; }
        public string StartupType { get; set; }
    }
}
