using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledJobs
{
    public class HeartBeatService : HostedService
    {
        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {                
                Debug.WriteLine($"HeartBeatService: {DateTime.Now.ToString()}");
                
                await Task.Delay(TimeSpan.FromSeconds(3), cToken);
            }
        }
    }
}
