
using DAL.EF;
using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceContracts.Contracts;
using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary.Repository
{
    public class DataSaverRepository : IDataRepository
    {
        private DataSaverContext _context;

        private ILogger<DataSaverRepository> _logger;
        public DataSaverRepository(DataSaverContext context, ILogger<DataSaverRepository> logger) {
            _context = context;
            _logger = logger;
            int num = new Random().Next();
            _logger.LogInformation(String.Format("DataSaverRepository : , {0}", num));

        }
        //public void SaveData(List<Data> dataList)
        //{

        //    List<TextData> textData = dataList.ConvertAll<TextData>((data) => new TextData() { Key = data.Key, Value = data.Value });
        //    DbContextOptionsBuilder<DataSaverContext> builder = new DbContextOptionsBuilder<DataSaverContext>();
        //    builder.UseSqlServer("Server=YOGESHWAR\\SQLDEV;Database=Pomodoro;Trusted_Connection=true;");

        //    using (var context = new DataSaverContext(builder.Options))
        //    {
        //        context.TextData.AddRange(textData.ToArray());
        //        context.SaveChanges();
        //    }
        //}

        //public void SaveDataClone(List<Data> dataList)
        //{
        //    List<TextDataClone> textDataClone = dataList.ConvertAll<TextDataClone>((data) => new TextDataClone() { Key = data.Key, Value = data.Value });
        //    DbContextOptionsBuilder<DataSaverContext> builder = new DbContextOptionsBuilder<DataSaverContext>();
        //    builder.UseSqlServer("Server=YOGESHWAR\\SQLDEV;Database=Pomodoro;Trusted_Connection=true;");

        //    using (var context = new DataSaverContext(builder.Options))
        //    {
        //        context.TextDataClone.AddRange(textDataClone.ToArray());
        //        context.SaveChanges();
        //    }
        //}

        public void SaveData(List<Data> dataList)
        {

            List<TextData> textData = dataList.ConvertAll<TextData>((data) => new TextData() { Key = data.Key, Value = data.Value });

            _context.TextData.AddRange(textData.ToArray());
            _context.SaveChanges();

        }

        public void SaveDataClone(List<Data> dataList)
        {
            List<TextDataClone> textDataClone = dataList.ConvertAll<TextDataClone>((data) => new TextDataClone() { Key = data.Key, Value = data.Value });

            _context.TextDataClone.AddRange(textDataClone.ToArray());
            _context.SaveChanges();

        }
    }
}
