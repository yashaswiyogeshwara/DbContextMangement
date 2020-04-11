using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.Contracts
{
    public interface IDataRepoWithUow
    {

        void SaveData(List<Data> dataList);
        void SaveDataClone(List<Data> dataList);
    }
}
