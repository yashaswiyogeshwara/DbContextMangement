using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceContracts.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDataSaver
{
    public class WorkerUsingTasks : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IDataSaver _dataSaverService;
        private IServiceProvider _serviceProvider;

        public WorkerUsingTasks(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Created a service in scope");
                
                    Task.WaitAll(SaveDataInMultipleThreads().ToArray());
            }
                //_logger.LogInformation("Finshed task");
        }

        private List<Task> SaveDataInMultipleThreads()
        {
            
            List<Task> tasks = new List<Task>();
            
                for (int i = 0; i < 20; i++)
                {
                
                    Task t = new Task(() =>
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var service = (IDataSaver)scope.ServiceProvider.GetRequiredService(typeof(IDataSaver));
                            service.SaveData();
                        }
                    });
                    t.Start();
                    tasks.Add(t);
                }
            
            return tasks;
                
        }
    }
}

