using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    var value = "taskImpl";
                    var sectionProps = hostContext.Configuration.GetSection("WorkerConfig").GetChildren();
                    services.AddDbContext<DataSaverContext>((options) => { options.UseSqlServer("Server=YOGESHWAR\\SQLDEV;Database=Pomodoro;Trusted_Connection=true;"); });
                    services.AddScoped<IDataRepository,DataSaverRepository>();
                    services.AddScoped<IDataSaver,DataSaver>();
                    if (value == "normal")
                    {
                        services.AddHostedService<Worker>();
                    }
                    if (value == "taskImpl") {
                        services.AddHostedService<WorkerUsingTasks>();
                    }
                });
    }
}
