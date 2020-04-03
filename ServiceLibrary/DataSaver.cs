
using Microsoft.Extensions.Logging;
using ServiceContracts.Contracts;
using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public class DataSaver : IDataSaver
    {
        private IDataRepository _dataRepository;
        private int _guid;
        private ILogger _logger;
        public DataSaver(IDataRepository dataRepository, ILogger<DataSaver> logger) {
            _dataRepository = dataRepository;
            _guid = new Random().Next(1000);
            _logger = logger;
            _logger.LogInformation(string.Format("DataSaver guid is {0}",_guid.ToString()));
        }
        public Task SaveDataAsync()
        {
            Task t = Task.Run(() => {
                _logger.LogInformation("In Data Service");
                List<Data> dataList = new List<Data>();
                string[] lines = System.IO.File.ReadAllLines(@"E:\Personal_Projects\SampleDataSaver\sampleData.txt");
                foreach (string line in lines)
                {
                    string[] row = line.Split(':');
                    dataList.Add(new Data { Key = row[0], Value = row[1] });
                }
                _dataRepository.SaveData(dataList);
                _dataRepository.SaveDataClone(dataList);
                _logger.LogInformation("Finished saving data");
            });
            return t;
        }

        public void SaveData()
        {
            
                _logger.LogInformation("In Data Service");
                List<Data> dataList = new List<Data>();
                string[] lines = System.IO.File.ReadAllLines(@"E:\Personal_Projects\SampleDataSaver\sampleData.txt");
                foreach (string line in lines)
                {
                    string[] row = line.Split(':');
                    dataList.Add(new Data { Key = row[0], Value = row[1] });
                }
                _dataRepository.SaveData(dataList);
                _dataRepository.SaveDataClone(dataList);
                _logger.LogInformation("Finished saving data");
            
        }

    }
}
