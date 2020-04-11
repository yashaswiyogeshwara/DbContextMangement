using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.Contracts
{
    public interface IDataSaverRepositoryWithDbScope
    {
         void SaveData(List<Data> dataList);
        void SaveDataClone(List<Data> dataList);

    }
}
