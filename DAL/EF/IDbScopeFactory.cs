using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public interface IDbScopeFactory
    {
        DbContextScope Create();
    }
}
