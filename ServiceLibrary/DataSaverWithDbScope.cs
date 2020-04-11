using DAL.EF;
using ServiceContracts.Contracts;
using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public class DataSaverWithDbScope : IDataSaverWithDbScope
    {
        private IDbScopeFactory _dbScopeFactory;
        private IDataSaverRepositoryWithDbScope _dataRepository;
        public DataSaverWithDbScope(IDbScopeFactory dbScopeFactory, IDataSaverRepositoryWithDbScope dataRepository) {
            _dbScopeFactory = dbScopeFactory;
            _dataRepository = dataRepository;
        }
        public Task SaveDataAsync()
        {
            Task t = Task.Run(() => {
                //_logger.LogInformation("In Data Service");
                List<Data> dataList = new List<Data>();
                string[] lines = System.IO.File.ReadAllLines(@"E:\Personal_Projects\SampleDataSaver\sampleData.txt");
                foreach (string line in lines)
                {
                    string[] row = line.Split(':');
                    dataList.Add(new Data { Key = row[0], Value = row[1] });
                }
                using (var dbScope = _dbScopeFactory.Create())
                {
                    _dataRepository.SaveData(dataList);
                    //_dataRepository.SaveDataClone(dataList);
                    //_logger.LogInformation("Finished saving data");
                }
            });
            return t;
        }
    }
}
