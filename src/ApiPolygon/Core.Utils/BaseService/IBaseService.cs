using System.Linq.Expressions;
using Core.Utils.Pagination;
using Core.Utils.Sorting;

namespace Core.Utils.BaseService;

public interface IBaseService<TEntity, TDto>
{
    Task<TDto> AddAsync(TDto dto);
    Task DeleteAsync(Guid id);
    Task<TDto> UpdateAsync(TDto dtos);

    IEnumerable<TDto> GetAll(Expression<Func<TDto, bool>> expression = null, PaginationParameters? pagination = null,
        OrderParameters? order = null);

    Task<TDto> GetByIdAsync(Guid id);
    Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression);
}