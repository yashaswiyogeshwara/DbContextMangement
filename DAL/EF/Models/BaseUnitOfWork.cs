using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Models
{
    public class BaseUnitOfWork : IContext
    {
        private IContext _parentUnitOfWork;
        private DbContext _context;
        private IDbContextTransaction _transaction;
        public BaseUnitOfWork(DbContext context, IContext parentUnitOfWork = null)
        {
            if (parentUnitOfWork == null) {
                //_transaction = context.Database.BeginTransaction();
            }
            _context = context;
            _parentUnitOfWork = parentUnitOfWork;
        }

        public void AddRange<TEntity>(TEntity[] entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Dispose()
        {
            if (_parentUnitOfWork == null)
            {
               // _transaction.Commit();
                _context.SaveChanges();
                _transaction = null;
            }
        }
    }
}
