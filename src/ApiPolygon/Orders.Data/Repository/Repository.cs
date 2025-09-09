using System.Linq.Expressions;
using Core.Utils.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Orders.Data.Repository;

public class Repository<T>(OrdersContext context) : IRepository<T>
    where T : class, new()
{
    protected readonly OrdersContext _context = context;

    public virtual T Add(T entity)
    {
        return _context.Set<T>().Add(entity).Entity;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        return (await _context.Set<T>().AddAsync(entity)).Entity;
    }

    public virtual int Count()
    {
        return _context.Set<T>().Count();
    }

    public virtual int Count(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Count(predicate);
    }

    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void Delete(Expression<Func<T, bool>> predicate)
    {
        var entity = GetSingle(predicate);
        _context.Set<T>().Remove(entity);
    }

    public virtual IQueryable<T> GetAll()
    {
        return GetAll(null, null, null, null, null);
    }

    public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return GetAll(predicate, null, null, null, null);
    }

    public virtual IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
    {
        return GetAll(null, include, null, null, null);
    }

    public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
    {
        return GetQuery(predicate, include);
    }

    public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null)
    {
        IQueryable<T> query = GetQuery(predicate, include);

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip != null && skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take != null && take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return query;
    }

    public virtual Task<IQueryable<T>> GetAllAsync()
    {
        return GetAllAsync(null, null, null, null, null);
    }

    public virtual Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return GetAllAsync(predicate, null, null, null, null);
    }

    public virtual Task<IQueryable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
    {
        return GetAllAsync(null, include, null, null, null);
    }

    public virtual Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
    {
        return GetAllAsync(predicate, include, null, null, null);
    }

    public virtual Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null, int? take = null)
    {
        IQueryable<T> query = GetQuery(predicate, include);

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip != null && skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take != null && take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return Task.FromResult<IQueryable<T>>(query);
    }

    public virtual T GetSingle(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = GetQuery(predicate, include);

        return query.AsNoTracking().FirstOrDefault();
    }

    public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = GetQuery(predicate, include);

        return query.AsNoTracking().FirstOrDefaultAsync();
    }

    public virtual T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    protected virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query;
    }
}