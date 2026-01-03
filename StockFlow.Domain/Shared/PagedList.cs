using Microsoft.EntityFrameworkCore;

namespace StockFlow.Domain.Shared;

public record PagedList<T>(List<T> Items, int Page, int PageSize, int TotalCount)
{
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        int totalCount = await query.CountAsync();
        List<T> items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new(items, page, pageSize, totalCount);
    }

    public static PagedList<T> CreateFromEnumerable(IEnumerable<T> source, int page, int pageSize)
    {
        var list = source.ToList(); 
        int totalCount = list.Count;
        var items = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new(items, page, pageSize, totalCount);
    }
}
