using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using Infrastructure.DI.StructureMap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleDataSaver.SampleDataSaver;
using ServiceContracts.Contracts;
using ServiceLibrary;
using ServiceLibrary.Repository;

namespace SampleDataSaver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //var value = hostContext.Configuration.GetSection("WorkerConfig").GetChildren();
                    var value = "uow";
                    var sectionProps = hostContext.Configuration.GetSection("WorkerConfig").GetChildren();
                    
                    services.AddDbContext<DataSaverContext>((options) => { options.UseSqlServer("Server=YOGESHWAR\\SQLDEV;Database=Pomodoro;Trusted_Connection=true;"); });
                    services.AddScoped<IDataRepository,DataSaverRepository>();
                    services.AddScoped<IDataSaver,DataSaver>();
                    services.AddScoped<IDataSaverWithDbScope, DataSaverWithDbScope>();
                    services.AddScoped<IDbScopeFactory, DbContextScopeFactory>();
                    services.AddScoped<IDataSaverRepositoryWithDbScope, DataSaverRpositoryWithDbScope>();
                    services.AddScoped<IContext, DataSaverContext>();
                    services.AddScoped<IDataSaverWithUow, DataSaverWithUow>();
                    services.AddScoped<IDataRepoWithUow, DataRepoWithUnitOfWork>();

                    if (value == "normal")
                    {
                        services.AddHostedService<Worker>();
                    }
                    if (value == "taskImpl") {
                        services.AddHostedService<WorkerUsingTasks>();
                    }
                    if (value == "usingDbScope") {
                        services.AddHostedService<WorkerWithDbScope>();
                    }
                    if (value == "uow")
                    {
                        services.AddHostedService<WorkerWithUow>();
                    }
                });
    }
}
