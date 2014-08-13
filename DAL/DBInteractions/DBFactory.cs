
using DAL.Context;
using System.Data.Entity;

namespace DAL.DBInteractions
{
public class DBFactory : Disposable, IDBFactory
{

    public DBFactory()
    {
        Database.SetInitializer<MainContext>(null);
    }
    private MainContext dataContext;
    public MainContext Get()
    {
        return dataContext ?? (dataContext = new MainContext());
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
