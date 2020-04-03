using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceContracts.Contracts;

namespace SampleDataSaver
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IServiceProvider _serviceProvider;

        // This throws error Worker with singleton scope cannot consume DataSaver of scoped lifetime
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Created a service in scope");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = (IDataSaver)scope.ServiceProvider.GetRequiredService(typeof(IDataSaver));
                    await service.SaveData();
                }
                _logger.LogInformation("Finshed task");
            }
        }


    }
}
