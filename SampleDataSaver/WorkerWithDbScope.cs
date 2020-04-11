using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDataSaver
{
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
        public class WorkerWithDbScope : BackgroundService
        {
            private readonly ILogger<WorkerWithDbScope> _logger;
            private IServiceProvider _serviceProvider;

            // This throws error WorkerWithDbScope with singleton scope cannot consume DataSaver of scoped lifetime
            public WorkerWithDbScope(ILogger<WorkerWithDbScope> logger, IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
                _logger = logger;
            }



            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                if (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Created a service in scope");
                    
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var service = (IDataSaverWithDbScope)scope.ServiceProvider.GetRequiredService(typeof(IDataSaverWithDbScope));
                            await service.SaveDataAsync();
                        }
                        _logger.LogInformation("Finshed task");
                    
                    
                }
            }


        }
    }
}
