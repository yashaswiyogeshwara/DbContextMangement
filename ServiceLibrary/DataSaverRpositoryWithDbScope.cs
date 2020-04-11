using DAL.EF;
using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Contracts;
using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary
{
    public class DataSaverRpositoryWithDbScope : IDataSaverRepositoryWithDbScope
    {
        private IDbScopeFactory _factory;

        public DataSaverRpositoryWithDbScope(IDbScopeFactory factory) {
            _factory = factory;
        }
        public void SaveData(List<Data> dataList)
        {
            List<TextData> textData = dataList.ConvertAll<TextData>((data) => new TextData() { Key = data.Key, Value = data.Value });
            using (var dbScope = _factory.Create()) {
                DataSaverContext context = dbScope.Get<DataSaverContext>();
                context.TextData.AddRange(textData.ToArray());
                context.SaveChanges();
            }    
        }

        public void SaveDataClone(List<Data> dataList)
        {
            throw new NotImplementedException();
        }
    }
}
