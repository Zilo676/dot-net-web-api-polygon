using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Utils.Repository;

public interface IRepository<T> where T : class, new()
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null, int? take = null);

    Task<IQueryable<T>> GetAllAsync();
    Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<IQueryable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
    Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

    T GetSingle(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    Task<T> GetSingleAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    T Add(T toAdd);
    Task<T> AddAsync(T entity);
    T Update(T toUpdate);
    void Delete(T entity);
    void Delete(Expression<Func<T, bool>> predicate);

    int Count();
    int Count(Expression<Func<T, bool>> predicate);
}