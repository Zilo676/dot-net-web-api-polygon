using System.Linq.Expressions;
using Core.Api.QueryFilter;
using Core.Utils.Pagination;
using Core.Utils.Sorting;
using Newtonsoft.Json;

namespace Core.Utils.QueryParameters;

public abstract class QueryParameters : IPaginationCreator, IQueryParametersSorting
{
    public QueryParameters()
    {
    }

    /// <summary>
    /// Текущая страница
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Количество элементов в одной странице
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Фильтр, по которому осуществляется фильтрация
    /// </summary>
    public string Filter { get; set; } = string.Empty;

    public string Sort { get; set; } = string.Empty;

    public bool DESC { get; set; } = false;

    public OrderParameters? Order
    {
        get { return !string.IsNullOrEmpty(Sort) ? new OrderParameters(Sort, !DESC) : null; }
    }

    public PaginationParameters CreatePagination()
    {
        var pagination = new PaginationParameters();

        if (PageNumber != null)
        {
            pagination.PageNumber = (int)PageNumber;
        }

        if (PageSize != null)
        {
            pagination.PageSize = (int)PageSize;
        }

        return pagination;
    }

    public (Expression<Func<T, bool>> expression, PaginationParameters pagination, OrderParameters? order) PrepareData<T>()
    {
        var filter = JsonConvert.DeserializeObject<QueryFilter>(Filter);
        var expression = QueryFilterCreator<T>.GetFilterExpression(filter);

        return (expression, pagination: CreatePagination(), order: Order);
    }
}