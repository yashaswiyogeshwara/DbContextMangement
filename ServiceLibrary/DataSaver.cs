
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ServiceContracts.Contracts;
using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
            //_logger.LogInformation(string.Format("DataSaver guid is {0}",_guid.ToString()));
        }

        public Task SaveDataAsync() {
            Task t = Task.Run(() => {
                //_logger.LogInformation("In Data Service");
                List<Data> dataList = new List<Data>();
                string[] lines = System.IO.File.ReadAllLines(@"E:\Personal_Projects\SampleDataSaver\sampleData.txt");
                foreach (string line in lines)
                {
                    string[] row = line.Split(':');
                    dataList.Add(new Data { Key = row[0], Value = row[1] });
                }
                using (var conn = new SqlConnection())
                {
                    _dataRepository.SaveData(dataList);
                    _dataRepository.SaveDataClone(dataList);
                    //_logger.LogInformation("Finished saving data");
                }
            });
            return t;
        }
        public void SaveDataAsync(bool useAdoNetTransactionScope)
        {
            //Task t = Task.Run(() => {
                //_logger.LogInformation("In Data Service");
                List<Data> dataList = new List<Data>();
                string[] lines = System.IO.File.ReadAllLines(@"E:\Personal_Projects\SampleDataSaver\sampleData.txt");
                foreach (string line in lines)
                {
                    string[] row = line.Split(':');
                    dataList.Add(new Data { Key = row[0], Value = row[1] });
                }
            using (var conn = new SqlConnection("Server=YOGESHWAR\\SQLDEV;Database=Pomodoro;Trusted_Connection=true;")) {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {


                        // we are doing something similar in our Execute() this may not work for dbContext we should check this!!.
                        _dataRepository.SaveData(dataList, transaction);
                        throw new Exception("something went wrong");
                        _dataRepository.SaveDataClone(dataList, transaction);
                        //_logger.LogInformation("Finished saving data");

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        conn.Close();
                    }


                }

            }
            
            //});
            //return t;
        }

        public void SaveData()
        {
            
                //_logger.LogInformation("In Data Service");
                List<Data> dataList = new List<Data>();
                string[] lines = System.IO.File.ReadAllLines(@"E:\Personal_Projects\SampleDataSaver\sampleData.txt");
                foreach (string line in lines)
                {
                    string[] row = line.Split(':');
                    dataList.Add(new Data { Key = row[0], Value = row[1] });
                }
            
                _dataRepository.SaveDataUsingInjectedContext(dataList);
                _dataRepository.SaveDataCloneUsingInjectedContext(dataList);
                
                
                //_logger.LogInformation("Finished saving data");
            
        }

        
    }
}
