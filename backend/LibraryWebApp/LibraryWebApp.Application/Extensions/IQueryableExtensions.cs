using LibraryWebApp.Application.DTOs;

namespace LibraryWebApp.Application.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationViewModel paginationParams)
        {
            return query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize);
        }
    }
}
