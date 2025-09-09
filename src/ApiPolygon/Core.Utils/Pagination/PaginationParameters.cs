using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Utils.Pagination;

public class PaginationParameters
{
    /// <summary>
    /// Текущая страница
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Количество элементов в одной странице
    /// </summary>
    public int PageSize { get; set; } = 500;

    /// <summary>
    /// Количество элементов которые пропускаем
    /// </summary>
    [BindNever]
    public int Skip => (PageNumber - 1) * PageSize;

    /// <summary>
    /// Количество элементов в выборке
    /// </summary>
    [BindNever]
    public int Total { get; set; }

    /// <summary>
    /// Количество страниц
    /// </summary>
    [BindNever]
    public int TotalPages { get; set; }

    /// <summary>
    /// Номер предыдущей страницы
    /// </summary>
    [BindNever]
    public int? PrevPage => PageNumber == 1 ? null : PageNumber - 1;

    /// <summary>
    /// Номер следующей страницы
    /// </summary>
    [BindNever]
    public int? NextPage => TotalPages > 1 && TotalPages > PageNumber ? PageNumber + 1 : null;
}