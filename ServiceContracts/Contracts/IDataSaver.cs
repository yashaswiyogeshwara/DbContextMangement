using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Contracts
{
    public interface IDataSaver
    {
        public Task SaveDataAsync();

        public void SaveDataAsync(bool useAdoNetTransactionScope);

        public void SaveData();
    }
}
