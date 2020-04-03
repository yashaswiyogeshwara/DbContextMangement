using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.Contracts
{
    public interface IDataRepository
    {
        public void SaveData(List<Data> dataList);
        public void SaveDataClone(List<Data> dataList);
    }
}
