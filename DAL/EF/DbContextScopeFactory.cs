using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public class DbContextScopeFactory : IDbScopeFactory
    {
        public DbContextScope Create()
        {
            DbContextScope scope = new DbContextScope();
            return scope;
        }
    }
}
