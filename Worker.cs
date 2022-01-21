using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using DataExchangeWorkerService.Services;

namespace DataExchangeWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly EtlProcessor _etlProcessor;

        public Worker(ILogger<Worker> logger, EtlProcessor etlProcessor)
        {
            _logger = logger;
            _etlProcessor = etlProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);

                try
                {
                    _etlProcessor.DoWork();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Something went wrong, Exception message: {e.Message}");
                    Console.WriteLine(e);
                }
                await Task.Delay(1000 * 60 * 5, stoppingToken);
            }
        }
    }
}
