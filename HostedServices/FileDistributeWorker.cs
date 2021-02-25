using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DaemonSample
{
    public class FileDistributeWorker : BackgroundService
    {
        private readonly ILogger<FileDistributeWorker> _logger;
        private Task _executingTask;
        private CancellationTokenSource _cts;

        public FileDistributeWorker(ILogger<FileDistributeWorker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {                       
            _logger.LogInformation("FileDistributeWorker started.");

            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = ExecuteAsync(_cts.Token);

            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }


        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return Task.CompletedTask;
            }

            _logger.LogInformation("FileDistributeWorker stopping.");

            _cts.Cancel();

            Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken)).ConfigureAwait(true);

            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogInformation("FileDistributeWorker stopped.");

            return Task.CompletedTask;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("FileDistributeWorker: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
