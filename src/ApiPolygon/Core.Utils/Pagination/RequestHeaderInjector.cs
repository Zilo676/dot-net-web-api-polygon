using Microsoft.AspNetCore.Http;

namespace Core.Utils.Pagination;

public static class RequestHeaderInjector
{
    public static void AddPaginationHeaders(HttpResponse response, PaginationParameters pagination)
    {
        response.Headers.Add("X-Total", pagination.Total.ToString());
        response.Headers.Add("X-Page", pagination.PageNumber.ToString());
        response.Headers.Add("X-Per-Page", pagination.PageSize.ToString());
        response.Headers.Add("X-Total-Pages", pagination.TotalPages.ToString());

        if (pagination.PrevPage != null)
        {
            response.Headers.Add("X-Prev-Page", pagination.PrevPage.ToString());
        }

        if (pagination.NextPage != null)
        {
            response.Headers.Add("X-Next-Page", pagination.NextPage.ToString());
        }
    }
}