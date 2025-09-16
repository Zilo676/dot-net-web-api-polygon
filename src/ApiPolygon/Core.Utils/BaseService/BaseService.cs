using System.Linq.Expressions;
using Core.Utils.Pagination;
using Core.Utils.Repository;
using Core.Utils.Sorting;
using Core.Data.DTO;
using Core.Data.Models;
using Core.Utils.Unit;
using AutoMapper;

namespace Core.Utils.BaseService;

public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : BaseEntity, new() where TDto :  BaseDto
{
    protected readonly IRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unit;

    public BaseService(IRepository<TEntity> repository, IMapper mapper, IUnitOfWork unit)
    {
        _repository = repository;
        _mapper = mapper;
        _unit = unit;
    }

    public virtual async Task<TDto> AddAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity = _repository.Add(entity);
        var createdDto = _mapper.Map<TDto>(entity);
        await PostAddAsync(createdDto);
        await _unit.SaveChangesAsync();
        return createdDto;
    }

    protected virtual Task PostAddAsync(TDto createdDto) => Task.CompletedTask;

    public virtual async Task<TDto> UpdateAsync(TDto dto)
    {
        if (dto?.Id == null || dto.Id == Guid.Empty)
        {
            throw new Exception("Dto Id is null or empty");
        }

        var entity = _mapper.Map<TEntity>(dto);
        entity = _repository.Update(entity);
        var resultDto = _mapper.Map<TDto>(entity);
        await PostUpdateAsync(resultDto);
        await _unit.SaveChangesAsync();
        return resultDto;
    }

    protected virtual Task PostUpdateAsync(TDto updatedDto) => Task.CompletedTask;

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = _repository.GetSingle(x => x.Id == id);
        if (entity != null)
        {
            _repository.Delete(entity);
            await PostDeleteAsync(entity);
            await _unit.SaveChangesAsync();
        }
    }

    protected virtual Task PostDeleteAsync(TEntity entity) => Task.CompletedTask;

    public virtual IEnumerable<TDto> GetAll(Expression<Func<TDto, bool>> expression = null,
        PaginationParameters? pagination = null, OrderParameters? order = null)
    {
        var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
        CalculatePagination(ref pagination, predicate);

        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByExpression = OrderBy;
        if (order != null)
        {
            orderByExpression = (IQueryable<TEntity> entities) =>
            {
                return entities.OrderBy(order.ColumnName, order.IsAscending);
            };
        }

        return _repository.GetAll(predicate,
            orderBy: orderByExpression,
            skip: pagination.Skip,
            take: pagination.PageSize).Select(_mapper.Map<TDto>).ToHashSet();
    }

    protected virtual IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> entities)
    {
        return entities.OrderBy(entity => entity.Id);
    }

    public virtual async Task<TDto> GetByIdAsync(Guid id)
    {
        var entity = _repository.GetSingle(x => x.Id == id);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression)
    {
        var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
        var entity = _repository.GetSingle(predicate);
        return _mapper.Map<TDto>(entity);
    }

    protected virtual void CalculatePagination(ref PaginationParameters? pagination,
        Expression<Func<TEntity, bool>>? expression = null)
    {
        pagination ??= new PaginationParameters();

        pagination.Total = expression is not null ? _repository.Count(expression) : _repository.Count();
        pagination.TotalPages = (int)Math.Ceiling((double)pagination.Total / pagination.PageSize);

        if (pagination.PageNumber > pagination.TotalPages && pagination.Total != 0)
        {
            throw new Exception(
                $"Не получаетя взять страницу {pagination.PageNumber} всего {pagination.TotalPages} страниц");
        }
    }
}