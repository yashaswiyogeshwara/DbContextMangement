using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public interface IContext : IDisposable
    {
        void AddRange<TEntity>(TEntity[] entities) where TEntity : class;
    }
    public interface IContext
    {
        IContext Begin();
        int SaveChanges();
        void Commit();
    }
}
