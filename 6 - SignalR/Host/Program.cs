using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Host
{
    class Program
    {
        // https://www.buraksenyurt.com/post/net-core-tarafinda-signalr-kullanimi

        static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .Build()
                   .Run();
        }
    }
}
