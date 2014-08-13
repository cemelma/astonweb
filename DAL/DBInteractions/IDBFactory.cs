using DAL.Context;
using System;

namespace DAL.DBInteractions
{
    public interface IDBFactory : IDisposable
    {
        MainContext Get();
    }
}
