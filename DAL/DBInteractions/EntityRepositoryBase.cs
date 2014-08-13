using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace DAL.DBInteractions
{
public abstract class EntityRepositoryBase<T> where T : class
{
    private MainContext _dataContext;
    private readonly IDbSet<T> dbset;
    protected EntityRepositoryBase(IDBFactory databaseFactory)
    {
        DatabaseFactory = databaseFactory;
        dbset = DataContext.Set<T>();
    }

    protected IDBFactory DatabaseFactory
    {
        get; private set;
    }

    protected MainContext DataContext
    {
        get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
    }
    public virtual void Add(T entity)
    {
        dbset.Add(entity);           
    }
    public virtual void Update(T entity)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
    }
    public virtual void Delete(T entity)
    {
        dbset.Remove(entity);           
    }
    public void Delete(Func<T, Boolean> where)
    {
        IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
        foreach (T obj in objects)
            dbset.Remove(obj);
    } 
    public virtual T GetById(long id)
    {
        return dbset.Find(id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return dbset.ToList();
    }
    public virtual IEnumerable<T> GetMany(Func<T, bool> where)
    {
        return dbset.Where(where).ToList();
    }
    public T Get(Func<T, Boolean> where)
    {
        return dbset.Where(where).FirstOrDefault<T>();
    } 
}
}
