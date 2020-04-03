using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public class DataSaverContext : DbContext
    {
        public DataSaverContext(DbContextOptions<DataSaverContext> options) : base(options) { 
        
        }
        public DbSet<TextData> TextData { get; set; }
        public DbSet<TextDataClone> TextDataClone { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //    optionsBuilder.UseSqlServer("Server=YOGESHWAR\\SQLDEV;Database=Pomodoro;Trusted_Connection=true;");      
        }
    }
}
