using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using Minifutbol.DAL.Context;
using Task = System.Threading.Tasks.Task;

namespace Minifutbol.DAL.Repository
{
  public class Repository<T> : IRepository<T>, IDisposable where T : class
  {
    #region Properties

    // ReSharper disable once StaticMemberInGenericType
    private static MinifutbolContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public MinifutbolContext DbContext
    {
      get => _dbContext ?? (_dbContext = new MinifutbolContext());
      set => _dbContext = value;
    }

    // private readonly MinifutbolContext DbContext;

    #endregion

    #region Constructor

    public Repository(MinifutbolContext context)
    {
      _dbContext = context;
      _dbSet = _dbContext.Set<T>();

    }

    //public Repository(MinifutbolContext dbContext)
    //{
    //    DbContext = dbContext;
    //    _dbSet = DbContext.Set<T>();
    //}

    #endregion

    #region Dispose
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing) return;
      if (_dbContext == null) return;
      _dbContext.Dispose();
      _dbContext = null;
    }

    #endregion


    #region Repository Get

    public IQueryable<T> GetAll(params string[] includes)
    {
      if (includes.Length <= 0) return _dbSet;

      foreach (var include in includes)
      {
        _dbSet.Include(include).Load();
      }
      return _dbSet;
    }

    public T GetById(int id, params string[] includes)
    {
      if (includes.Length <= 0) return _dbSet.Find(id);

      foreach (var include in includes)
      {
        _dbSet.Include(include).Load();
      }
      var at = _dbSet.Find(id);
      return at;
    }

    #endregion

    #region Repository Find

    public T Find(Expression<Func<T, bool>> predicate, params string[] includes)
    {
      if (includes.Length <= 0) return _dbSet.SingleOrDefault(predicate);

      foreach (var include in includes)
      {
        _dbSet.Include(include).Load();
      }
      var data = _dbSet.SingleOrDefault(predicate);
      return data;
    }

    public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params string[] includes)
    {

      if (includes.Length <= 0) return _dbSet.AsExpandable().Where(predicate);

      foreach (var include in includes)
      {
        _dbSet.Include(include).Load();
      }
      return _dbSet.AsExpandable().Where(predicate);
    }

    #endregion

    #region  Repository Add

    public T Add(T entity, params string[] includes)
    {
      if (includes.Length <= 0)
      {
        _dbSet.Add(entity);
        DbContext.SaveChanges();
        DbContext = new MinifutbolContext();
        return entity;
      }
      foreach (var include in includes)
      {
        _dbSet.Include(include).Load();
      }
      _dbSet.Add(entity);
      return entity;
    }

    public List<T> AddRange(List<T> entities, params string[] includes)
    {
      _dbSet.AddRange(entities);
      DbContext.SaveChanges();
      DbContext = new MinifutbolContext();
      return entities;
    }

    #endregion

    #region Repository Update

    public T Update(T entity)
    {
      if (entity == null) return null;
      DbContext.Entry(entity).CurrentValues.SetValues(entity);
      DbContext.SaveChanges();
      DbContext = new MinifutbolContext();
      return entity;
    }
    public List<T> UpdateRange(List<T> entity)
    {
      if (entity == null) return null;
      for (int i = 0; i < entity.Count; i++)
      {
        DbContext.Entry(entity[i]).CurrentValues.SetValues(entity[i]);
      }
      DbContext.SaveChanges();
      DbContext = new MinifutbolContext();
      return entity;
    }

    #endregion

    #region Repository Remove

    public void Remove(T entity)
    {
      _dbSet.Remove(entity);
      DbContext.SaveChanges();
      DbContext = new MinifutbolContext();
      DbContext.SaveChanges();

    }

    #endregion
  }
}
