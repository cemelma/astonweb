
using DAL.Context;
namespace DAL.DBInteractions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDBFactory _databaseFactory;
        private MainContext _dataContext;

        public UnitOfWork(IDBFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        protected MainContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
