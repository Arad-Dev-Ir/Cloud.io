namespace Cloud.Web.Core.Contract;

using static Math;
using Cloud.Core.Models;

public record PagedData<T> where T : Record
{
    public List<T> Items { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public bool HasNextPage => (Page * PageSize) < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public int TotalPages => (int)Ceiling(TotalCount / (double)PageSize);
}