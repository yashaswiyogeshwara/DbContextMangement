using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public class DataSaverContext : DbContext, IContext
    {
        public DataSaverContext() { 
        
        }
        public DataSaverContext(DbContextOptions<DataSaverContext> options) : base(options)
        {
           
        }
        public DataSaverContext(DbContextOptions<DataSaverContext> options, ILogger<DataSaverContext> logger ) : base(options) {
            logger.LogInformation(String.Format("Context id is : {0}", new Random().Next(1000)));
        }
        public DbSet<TextData> TextData { get; set; }
        public DbSet<TextDataClone> TextDataClone { get; set; }

        private IContext _rootUnitOfWork;
        public IContext Begin()
        {
            if (_rootUnitOfWork == null) {
                _rootUnitOfWork = new BaseUnitOfWork(this);
                return _rootUnitOfWork;
            }
            return new BaseUnitOfWork(this,_rootUnitOfWork);
        }

        public void Commit()
        {
            this.Database.CommitTransaction();
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
          optionsBuilder.UseSqlServer("Server=YOGESHWAR\\SQLDEV;Database=Pomodoro;Trusted_Connection=true;");      
        }
    }
}
