using DAL.EF;
using DAL.EF.Models;
using ServiceContracts.Contracts;
using ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary
{
    public class DataRepoWithUnitOfWork : IDataRepoWithUow
    {
        private IContext _context;
        public DataRepoWithUnitOfWork(IContext context) {
            _context = context;
        }
        public void SaveData(List<Data> dataList)
        {
            List<TextData> textData = dataList.ConvertAll<TextData>((data) => new TextData() { Key = data.Key, Value = data.Value });
            using (var uow = _context.Begin())
            {
                uow.AddRange<TextData>(textData.ToArray());
            }
        }

        

        public void SaveDataClone(List<Data> dataList)
        {
            throw new NotImplementedException();
        }

        
    }
}
