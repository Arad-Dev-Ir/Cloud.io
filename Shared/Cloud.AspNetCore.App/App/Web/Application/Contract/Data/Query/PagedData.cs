namespace Cloud.Web.Core.Contract;

using static Math;
using Cloud.Core.Models;

public class PagedData<T> where T : Record, new()
{
    public List<T> Records { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => (Page * PageSize) < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public int TotalPages => (int)Ceiling(TotalCount / (double)PageSize);
}