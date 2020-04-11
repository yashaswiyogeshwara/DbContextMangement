using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Transactions;

namespace ServiceContracts.Contracts
{
    public interface IDataRepository
    {
        public void SaveData(List<Data> dataList);
        public void SaveDataClone(List<Data> dataList);

        public void SaveData(List<Data> dataList, DbTransaction txn);
        public void SaveDataClone(List<Data> dataList, DbTransaction txn);


        public void SaveDataUsingInjectedContext(List<Data> dataList);
        public void SaveDataCloneUsingInjectedContext(List<Data> dataList);
    }
}
